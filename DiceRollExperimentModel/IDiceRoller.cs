using System;
using System.Threading.Tasks;

namespace DiceRollExperimentModel
{
    public interface IDiceRoller
    {
        public event EventHandler<string>? OnCalculationFinished;

        // TODO 使われていない
        public int DiceRollResult { get; }

        public Task<ulong> StartRoll();

        public (int threadNumber, ulong diceRollCount, int diceRollResult, TimeSpan elapsedTime) GetResult(string message);

        public (ulong diceRollCount, int diceRollResult, TimeSpan elapsedTime, ulong rollsPerSecond) GetFinalResult();
    }
}
