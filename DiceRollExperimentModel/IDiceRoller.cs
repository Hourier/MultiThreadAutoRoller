using System;
using System.ComponentModel;

namespace DiceRollExperimentModel
{
    public interface IDiceRoller : INotifyPropertyChanged
    {
        public ulong DiceRollCount { get; }

        public int DiceRollResult { get; }

        public TimeSpan ElapsedTime { get; }

        public void StartRoll();
    }
}
