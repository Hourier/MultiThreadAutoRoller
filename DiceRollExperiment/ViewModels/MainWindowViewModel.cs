using DiceRollExperimentModel;
using Prism.Events;
using Prism.Mvvm;
using Reactive.Bindings;
using Unity;

namespace DiceRollExperiment.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Dice Roll Experiment";
        private readonly IEventAggregator eventAggregator; // TODO: まだ使わない、不必要であることが確定したら消す.

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

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.Name.Value = "あなたは……";
            this.DisplayName.Subscribe(() => this.UpdateName());
        }

        private void UpdateName() => this.Name.Value = this.DiceRoller.GetName();
    }
}
