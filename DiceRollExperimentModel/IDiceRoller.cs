using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DiceRollExperimentModel
{
    public interface IDiceRoller : INotifyPropertyChanged
    {
        public int DiceRollResult { get; }

        public Task<ulong> StartRoll();

        public (int threadNumber, ulong diceRollCount, int diceRollResult, TimeSpan elapsedTime, ulong rollsPerSecond) GetResult(string message);

        public (ulong diceRollCount, int diceRollResult, TimeSpan elapsedTime, ulong rollsPerSecond) GetFinalResult();
    }
}
