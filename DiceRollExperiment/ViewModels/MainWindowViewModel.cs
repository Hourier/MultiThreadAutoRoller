using DiceRollExperimentModel;
using Prism.Events;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Unity;

namespace DiceRollExperiment.ViewModels
{
    public class MainWindowViewModel : BindableBase, IDisposable
    {
        private string title = "Dice Roll Experiment";
        private readonly IEventAggregator eventAggregator; // TODO: まだ使わない、不必要であることが確定したら消す.
        private readonly PlayerDescription playerDescription;
        private readonly PlayerSex playerSex = new PlayerSex();
        private readonly PlayerRace playerRace = new PlayerRace();
        private readonly PlayerPersonality playerPersonality = new PlayerPersonality();
        private readonly PlayerClass playerClass = new PlayerClass();
        private readonly PlayerRealm playerRealm = new PlayerRealm();
        private bool isButtonPushedFirst;
        private bool hasFinalResultGotten;
        private CompositeDisposable disposable = new CompositeDisposable();

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.AddCompositeDisposable();
            this.DiceRollCommand.Subscribe(() => this.ExecuteDiceRoll());

            this.playerDescription = new PlayerDescription(this.playerSex, this.playerRace, this.playerPersonality, this.playerClass);
            this.SexesComboBox = this.playerSex.SexMap;
            this.SelectedPlayerSex.Subscribe(x => this.UpdateSex(x));
            this.RacesComboBox = this.playerRace.RaceMap;
            this.SelectedPlayerRace.Subscribe(x => this.UpdateRace(x));
            this.PersonalitiesComboBox = this.playerPersonality.PersonalityMap;
            this.SelectedPlayerPersonality.Subscribe(x => this.UpdatePersonality(x));
            this.ClassesComboBox = this.playerClass.ClassMap;
            this.SelectedPlayerClass.Subscribe(x => this.UpdateClass(x));
            this.FirstRealmsComboBox.Subscribe(_ => this.UpdateRealmsComboBoxFirst());
            this.SecondRealmsComboBox.Subscribe(_ => this.UpdateRealmssComboBoxSecond());
            this.SelectedPlayerRealmFirst.Subscribe(_ => this.UpdateRealmFirst());
            this.SelectedPlayerRealmSecond.Subscribe(_ => this.UpdateRealmSecond());
        }

        public string Title
        {
            get { return title; }
            set { this.SetProperty(ref this.title, value); }
        }

        // コンストラクタ内ではnull.
        [Dependency]
        public IDiceRoller DiceRoller { get; set; }

        public IReadOnlyDictionary<SexType, string> SexesComboBox { get; set; }

        public IReadOnlyDictionary<RaceType, string> RacesComboBox { get; set; }

        // まだ男女でセクシーギャル/ラッキーマンを除外する機能は持っていない.
        public IReadOnlyDictionary<PersonalityType, string> PersonalitiesComboBox { get; set; }

        public IReadOnlyDictionary<ClassType, string> ClassesComboBox { get; set; }

        public ReactiveProperty<IReadOnlyDictionary<RealmType, string>> FirstRealmsComboBox { get; set; } = new ReactiveProperty<IReadOnlyDictionary<RealmType, string>>();

        public ReactiveProperty<IReadOnlyDictionary<RealmType, string>> SecondRealmsComboBox { get; set; } = new ReactiveProperty<IReadOnlyDictionary<RealmType, string>>();

        // 本当はenumとの相互変換をしたいが、Prism7.2+ReactivePropertyのXAML環境下で適切に動作するConverterをどうしても作り込めなかった.
        public ReactiveProperty<string> SelectedPlayerSex { get; } = new ReactiveProperty<string>(((int)SexType.Female).ToString());

        public ReactiveProperty<string> SelectedPlayerRace { get; } = new ReactiveProperty<string>(((int)RaceType.Human).ToString());

        public ReactiveProperty<string> SelectedPlayerPersonality { get; } = new ReactiveProperty<string>(((int)PersonalityType.Ordinary).ToString());

        // ふさわしい職業かどうかを調べる機能は未実装.
        public ReactiveProperty<string> SelectedPlayerClass { get; } = new ReactiveProperty<string>(((int)ClassType.Warrior).ToString());

        public ReactiveProperty<bool> HasFirstRealm { get; } = new ReactiveProperty<bool>();

        public ReactiveProperty<bool> HasSecondRealm { get; } = new ReactiveProperty<bool>();

        public ReactiveProperty<string> SelectedPlayerRealmFirst { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<string> SelectedPlayerRealmSecond { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<string> PlayerDescriptionLabel { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<string> RealmDescriptionLabel { get; } = new ReactiveProperty<string>();

        public ReactiveCommand DisplayDescription { get; } = new ReactiveCommand();

        public ReactiveCommand DiceRollCommand { get; } = new ReactiveCommand();

        public ReactiveProperty<string> DiceRollCount { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<string> DiceRollResult { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<string> ElapsedTime { get; } = new ReactiveProperty<string>();

        public void Dispose() => this.disposable.Dispose();

        public void AddCompositeDisposable()
        {
            this.disposable.Add(this.SelectedPlayerRace);
            this.disposable.Add(this.SelectedPlayerPersonality);
            this.disposable.Add(this.SelectedPlayerClass);
            this.disposable.Add(this.FirstRealmsComboBox);
            this.disposable.Add(this.SecondRealmsComboBox);
            this.disposable.Add(this.HasFirstRealm);
            this.disposable.Add(this.HasSecondRealm);
            this.disposable.Add(this.SelectedPlayerRealmFirst);
            this.disposable.Add(this.PlayerDescriptionLabel);
            this.disposable.Add(this.RealmDescriptionLabel);
            this.disposable.Add(this.DisplayDescription);
            this.disposable.Add(this.DiceRollCommand);
            this.disposable.Add(this.DiceRollCount);
            this.disposable.Add(this.ElapsedTime);
        }

        private void UpdateSex(string x)
        {
            var sexType = this.playerSex.GetPlayerSex(x);
            var raceType = this.playerRace.GetPlayerRace(x);
            var personalityType = this.playerPersonality.GetPlayerPersonality(this.SelectedPlayerPersonality.Value);
            var classType = this.playerClass.GetPlayerClass(this.SelectedPlayerClass.Value);
            this.PlayerDescriptionLabel.Value = this.playerDescription.GetDescription(sexType, raceType, personalityType, classType);
        }

        private void UpdateRace(string x)
        {
            var sexType = this.playerSex.GetPlayerSex(this.SelectedPlayerSex.Value);
            var raceType = this.playerRace.GetPlayerRace(x);
            var personalityType = this.playerPersonality.GetPlayerPersonality(this.SelectedPlayerPersonality.Value);
            var classType = this.playerClass.GetPlayerClass(this.SelectedPlayerClass.Value);
            this.PlayerDescriptionLabel.Value = this.playerDescription.GetDescription(sexType, raceType, personalityType, classType);
        }

        private void UpdatePersonality(string x)
        {
            var sexType = this.playerSex.GetPlayerSex(this.SelectedPlayerSex.Value);
            var raceType = this.playerRace.GetPlayerRace(this.SelectedPlayerRace.Value);
            var personalityType = this.playerPersonality.GetPlayerPersonality(x);
            var classType = this.playerClass.GetPlayerClass(this.SelectedPlayerClass.Value);
            this.PlayerDescriptionLabel.Value = this.playerDescription.GetDescription(sexType, raceType, personalityType, classType);
        }

        private void UpdateClass(string x)
        {
            var sexType = this.playerSex.GetPlayerSex(this.SelectedPlayerSex.Value);
            var raceType = this.playerRace.GetPlayerRace(this.SelectedPlayerRace.Value);
            var personalityType = this.playerPersonality.GetPlayerPersonality(this.SelectedPlayerPersonality.Value);
            var classType = this.playerClass.GetPlayerClass(x);
            this.UpdateRealms();
            this.PlayerDescriptionLabel.Value = this.playerDescription.GetDescription(sexType, raceType, personalityType, classType);
            this.UpdateRealmDescription();
        }

        private void UpdateRealms()
        {
            var classType = this.playerClass.GetPlayerClass(this.SelectedPlayerClass.Value);
            var firstRealms = this.playerRealm.GetRealms(classType, true);
            this.FirstRealmsComboBox.Value = firstRealms;
            this.SelectedPlayerRealmFirst.Value = "0";
            var hasFirstRealm = this.playerRealm.HasFirstRealm(classType);
            var isFirstRealmFixed = this.playerRealm.IsFirstRealmFixed(classType);
            this.HasFirstRealm.Value = hasFirstRealm && !isFirstRealmFixed;

            var firstRealm = firstRealms.Select(x => x.Key).First();
            this.SecondRealmsComboBox.Value = this.playerRealm.GetRealms(classType, false, firstRealm);
            this.SelectedPlayerRealmSecond.Value = "0";
            this.HasSecondRealm.Value = this.playerRealm.HasSecondRealm(classType);
        }

        private void UpdateRealmsComboBoxFirst()
        {
            var classType = this.playerClass.GetPlayerClass(this.SelectedPlayerClass.Value);
            var firstRealms = this.playerRealm.GetRealms(classType, true);
            this.FirstRealmsComboBox.Value = firstRealms;
            this.SelectedPlayerRealmFirst.Value = "0";
            var hasFirstRealm = this.playerRealm.HasFirstRealm(classType);
            var isFirstRealmFixed = this.playerRealm.IsFirstRealmFixed(classType);
            this.HasFirstRealm.Value = hasFirstRealm && !isFirstRealmFixed;

            var firstRealm = firstRealms.Select(x => x.Key).First();
            this.SecondRealmsComboBox.Value = this.playerRealm.GetRealms(classType, false, firstRealm);
            this.SelectedPlayerRealmSecond.Value = "0";
            this.HasSecondRealm.Value = this.playerRealm.HasSecondRealm(classType);
        }

        private void UpdateRealmssComboBoxSecond()
        {
            var selectedFirstRealm = this.SelectedPlayerRealmFirst.Value;
            if ("-1".Equals(selectedFirstRealm))
            {
                return;
            }

            var classType = this.playerClass.GetPlayerClass(this.SelectedPlayerClass.Value);
            var firstRealm = this.playerRealm.GetRealms(classType, true).Select(x => x.Key).ToList()[int.Parse(selectedFirstRealm)];
            if (this.UpdateMageRealm(classType, firstRealm) || this.UpdatePriestRealm(classType, firstRealm))
            {
                return;
            }

            this.SelectedPlayerRealmSecond.Value = "0";
        }

        private void UpdateRealmFirst()
        {
            var selectedFirstRealm = this.SelectedPlayerRealmFirst.Value;
            if ("-1".Equals(selectedFirstRealm))
            {
                return;
            }

            var classType = this.playerClass.GetPlayerClass(this.SelectedPlayerClass.Value);
            var firstRealm = this.playerRealm.GetRealms(classType, true).Select(x => x.Key).ToList()[int.Parse(selectedFirstRealm)];
            this.SecondRealmsComboBox.Value = this.playerRealm.GetRealms(classType, false, firstRealm);
            if (this.UpdateMageRealm(classType, firstRealm) || this.UpdatePriestRealm(classType, firstRealm))
            {
                return;
            }

            this.SelectedPlayerRealmSecond.Value = "0";
            this.UpdateRealmDescription();
        }

        private void UpdateRealmSecond() => this.UpdateRealmDescription();

        private bool UpdateMageRealm(ClassType classType, RealmType firstRealm)
        {
            if (classType != ClassType.Mage)
            {
                return false;
            }

            // 生命の時は仙術を強制的に選択させる.
            if (firstRealm == RealmType.Life)
            {
                this.SelectedPlayerRealmSecond.Value = "1";
                return true;
            }

            // 仙術の時は生命にしたいのだが、どうしてもコンボボックスが消滅するバグが起きたので仕方なくカオスにする.
            if (firstRealm == RealmType.Sorcery)
            {
                this.SelectedPlayerRealmSecond.Value = "2";
                return true;
            }

            // それ以外は第2領域のデフォルト値を生命にする.
            this.SelectedPlayerRealmSecond.Value = "0";
            return true;
        }

        private bool UpdatePriestRealm(ClassType classType, RealmType firstRealm)
        {
            if (classType != ClassType.Priest)
            {
                return false;
            }

            // 生命の時は仙術を強制的に選択させる.
            // 暗黒の時も仙術になってしまうがどうしようもないので放置する.
            // メイジで生命以外を選択し、その後プリーストを選択するとコンボボックスが空になる……
            // 逆は問題なし。一旦暗黒などを選択して生命に戻しても問題なし.
            // 意味不明なので一旦放置.
            if (firstRealm == RealmType.Life)
            {
                this.SelectedPlayerRealmSecond.Value = "1";
                return true;
            }

            // それ以外は第2領域のデフォルト値を生命にする.
            this.SelectedPlayerRealmSecond.Value = "0";
            return true;
        }

        private void UpdateRealmDescription()
        {
            var classType = this.playerClass.GetPlayerClass(this.SelectedPlayerClass.Value);
            var selectedFirstRealm = this.SelectedPlayerRealmFirst.Value;
            if ("-1".Equals(selectedFirstRealm))
            {
                return;
            }

            var firstRealm = this.playerRealm.GetRealms(classType, true).Select(x => x.Key).ToList()[int.Parse(selectedFirstRealm)];
            var selectedSecondRealm = this.SelectedPlayerRealmSecond.Value;
            if ("-1".Equals(selectedSecondRealm))
            {
                return;
            }

            var secondRealm = this.playerRealm.GetRealms(classType, false, firstRealm).Select(x => x.Key).ToList()[int.Parse(selectedSecondRealm)];
            this.RealmDescriptionLabel.Value = this.playerRealm.GetRealmDescription(classType, firstRealm, secondRealm);
        }

        private void ExecuteDiceRoll()
        {
            if (!this.isButtonPushedFirst)
            {
                this.DiceRoller.PropertyChanged += this.UpdateDiceRollResult;
                this.isButtonPushedFirst = true;
            }

            this.hasFinalResultGotten = false;
            _ = Task.Run(() => this.DiceRoller.StartRoll());
        }

        // 敢えてToPropertyAsSynchronizedにしていない。UIの描画速度を抑えるため
        private void UpdateDiceRollResult(object sender, PropertyChangedEventArgs e)
        {
            if ("Finished".Equals(e.PropertyName))
            {
                this.ShowFinalDiceRollResult();
                return;
            }

            if (this.hasFinalResultGotten || !e.PropertyName.Contains(','))
            {
                return;
            }

            var (threadNumber, diceRollCount, diceRollResult, elapsedTime, rollsPerSecond) = this.DiceRoller.GetResult(e.PropertyName);
            if (threadNumber != 0)
            {
                return;
            }

            this.DiceRollCount.Value = diceRollCount.ToString("N0");
            this.DiceRollResult.Value =diceRollResult.ToString("N0");
            this.ElapsedTime.Value = elapsedTime.ToString(@"mm\:ss\.fff");
        }

        private void ShowFinalDiceRollResult()
        {
            this.hasFinalResultGotten = true;
            var (diceRollCount, diceRollResult, elapsedTime, rollsPerSecond) = this.DiceRoller.GetFinalResult();
            this.DiceRollCount.Value = diceRollCount.ToString("N0");
            this.DiceRollResult.Value = diceRollResult.ToString("N0");
            this.ElapsedTime.Value = elapsedTime.ToString(@"mm\:ss\.fff");
        }
    }
}
