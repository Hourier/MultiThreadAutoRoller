using DiceRollExperimentModel;
using Prism.Events;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Disposables;
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
        private bool isButtonPushedFirst;
        private CompositeDisposable disposable = new CompositeDisposable();

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.AddCompositeDisposable();
            this.PlayerDescriptionLabel.Value = "あなたは……";
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

        // 魔法領域については未実装.
        public IReadOnlyDictionary<ClassType, string> ClassesComboBox { get; set; }

        // 本当はenumとの相互変換をしたいが、Prism7.2+ReactivePropertyのXAML環境下で適切に動作するConverterをどうしても作り込めなかった.
        public ReactiveProperty<string> SelectedPlayerSex { get; } = new ReactiveProperty<string>(((int)SexType.Female).ToString());

        public ReactiveProperty<string> SelectedPlayerRace { get; } = new ReactiveProperty<string>(((int)RaceType.Human).ToString());

        public ReactiveProperty<string> SelectedPlayerPersonality { get; } = new ReactiveProperty<string>(((int)PersonalityType.Ordinary).ToString());

        // ふさわしい職業かどうかを調べる機能は未実装.
        public ReactiveProperty<string> SelectedPlayerClass { get; } = new ReactiveProperty<string>(((int)ClassType.Warrior).ToString());

        public ReactiveProperty<string> PlayerDescriptionLabel { get; } = new ReactiveProperty<string>();

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
            this.disposable.Add(this.PlayerDescriptionLabel);
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
            this.PlayerDescriptionLabel.Value = this.playerDescription.GetDescription(sexType, raceType, personalityType, classType);
        }


        private void ExecuteDiceRoll()
        {
            if (!this.isButtonPushedFirst)
            {
                this.DiceRoller.PropertyChanged += this.UpdateDiceRollResult;
                this.isButtonPushedFirst = true;
            }

            _ = Task.Run(() => this.DiceRoller.StartRoll());
        }

        // 敢えてToPropertyAsSynchronizedにしていない。UIの描画速度を抑えるため
        private void UpdateDiceRollResult(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(this.DiceRoller.DiceRollResult))
            {
                return;
            }

            this.DiceRollCount.Value = this.DiceRoller.DiceRollCount.ToString("N0");
            this.DiceRollResult.Value = this.DiceRoller.DiceRollResult.ToString("N0");
            this.ElapsedTime.Value = this.DiceRoller.ElapsedTime.ToString(@"mm\:ss\.fff");
        }
    }
}
