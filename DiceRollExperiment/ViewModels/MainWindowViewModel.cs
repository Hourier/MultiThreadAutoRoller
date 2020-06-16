using DiceRollExperimentModel;
using Prism.Events;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
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
        private bool isButtonPushedFirst;
        private CompositeDisposable disposable = new CompositeDisposable();

        public string Title
        {
            get { return title; }
            set { this.SetProperty(ref this.title, value); }
        }

        // コンストラクタ内ではnull.
        [Dependency]
        public IDiceRoller DiceRoller { get; set; }

        public ReactiveProperty<string> Name { get; } = new ReactiveProperty<string>();

        public ReactiveCommand DisplayName { get; } = new ReactiveCommand();

        public ReactiveCommand DiceRollCommand { get; } = new ReactiveCommand();

        public ReactiveProperty<string> DiceRollCount { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<string> DiceRollResult { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<string> ElapsedTime { get; } = new ReactiveProperty<string>();

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.disposable.Add(this.DisplayName);
            this.disposable.Add(this.DiceRollCommand);
            this.Name.Value = "あなたは……";
            this.DisplayName.Subscribe(() => this.UpdateName());
            this.DiceRollCommand.Subscribe(() => this.ExecuteDiceRoll());
        }

        public void Dispose() => this.disposable.Dispose();

        private void UpdateName() => this.Name.Value = this.DiceRoller.GetName();

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
