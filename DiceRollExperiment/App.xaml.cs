using Prism.Ioc;
using DiceRollExperiment.Views;
using DiceRollExperimentModel;
using System.Windows;

namespace DiceRollExperiment
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell() => this.Container.Resolve<MainWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry) => containerRegistry.Register<IDiceRoller, DiceRoller>();
    }
}
