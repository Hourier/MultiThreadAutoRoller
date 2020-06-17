using DiceRollExperimentModel;
using Prism.Events;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace DiceRollExperiment.ViewModels
{
    public class MainWindowViewModel : BindableBase, IDisposable
    {
        private string title = "Dice Roll Experiment";
        private readonly IEventAggregator eventAggregator; // TODO: まだ使わない、不必要であることが確定したら消す.
        private readonly PlayerDescription playerDescription;
        private bool isButtonPushedFirst;
        private CompositeDisposable disposable = new CompositeDisposable();

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.AddCompositeDisposable();
            this.PlayerDescriptionLabel.Value = "あなたは……";
            this.DiceRollCommand.Subscribe(() => this.ExecuteDiceRoll());

            var playerPersonality = new PlayerPersonality();
            this.playerDescription = new PlayerDescription(playerPersonality);
            this.PersonalitiesComboBox = playerPersonality.PersonalityMap;
            this.SelectedPlayerPersonality.Subscribe(x => this.UpdatePersonality(x));
        }

        public string Title
        {
            get { return title; }
            set { this.SetProperty(ref this.title, value); }
        }

        // コンストラクタ内ではnull.
        [Dependency]
        public IDiceRoller DiceRoller { get; set; }

        // まだ男女でセクシーギャル/ラッキーマンを除外する機能は持っていない.
        public IReadOnlyDictionary<PersonalityType, string> PersonalitiesComboBox { get; set; }

        // 本当はenumとの相互変換をしたいが、Prism7.2+ReactivePropertyのXAML環境下で適切に動作するConverterをどうしても作り込めなかった.
        public ReactiveProperty<string> SelectedPlayerPersonality { get; } = new ReactiveProperty<string>(((int)PersonalityType.Ordinary).ToString());

        public ReactiveProperty<string> PlayerDescriptionLabel { get; } = new ReactiveProperty<string>();

        public ReactiveCommand DisplayDescription { get; } = new ReactiveCommand();

        public ReactiveCommand DiceRollCommand { get; } = new ReactiveCommand();

        public ReactiveProperty<string> DiceRollCount { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<string> DiceRollResult { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<string> ElapsedTime { get; } = new ReactiveProperty<string>();

        public void Dispose() => this.disposable.Dispose();

        public void AddCompositeDisposable()
        {
            this.disposable.Add(this.SelectedPlayerPersonality);
            this.disposable.Add(this.PlayerDescriptionLabel);
            this.disposable.Add(this.DisplayDescription);
            this.disposable.Add(this.DiceRollCommand);
            this.disposable.Add(this.DiceRollCount);
            this.disposable.Add(this.ElapsedTime);
        }

        private void UpdatePersonality(string x)
        {
            if (!int.TryParse(x, out var intValue))
            {
                MessageBox.Show("Invalid value!");
                return;
            }

            if (!Enum.IsDefined(typeof(PersonalityType), intValue))
            {
                MessageBox.Show("Undefined value!");
                return;
            }

            this.PlayerDescriptionLabel.Value = this.playerDescription.GetDescription((PersonalityType)intValue);
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
