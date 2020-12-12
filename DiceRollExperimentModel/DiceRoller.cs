using DiceRollExperimentModel.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiceRollExperimentModel
{
    public class DiceRoller : IDiceRoller
    {
        public event EventHandler<string>? OnCalculationFinished;

        private readonly List<DiceRollerThread> diceRollerThreads = new List<DiceRollerThread>();

        public DiceRoller()
        {
            var cpuThreads = Environment.ProcessorCount;
            for (var i = 0; i < cpuThreads; i++)
            {
                var diceRollerThread = new DiceRollerThread(i);
                this.diceRollerThreads.Add(diceRollerThread);
                if (i == 0)
                {
                    diceRollerThread.OnCalculationFinished += this.OnTimerElapsed;
                }
            }
        }

        public int DiceRollResult { get; private set; }

        public async Task<ulong> StartRoll()
        {
            var tokenSource = new CancellationTokenSource();
            var cancellationToken = tokenSource.Token;
            var tasks = new List<Task<ulong>>();
            var startTime = DateTime.Now;
            foreach (var diceRollerThread in this.diceRollerThreads)
            {
                var task = Task.Run(() => diceRollerThread.RunDiceRoll(startTime, cancellationToken));
                tasks.Add(task);
            }

            var endTask = await Task.WhenAny(tasks);
            tokenSource.Cancel();
            this.OnCalculationFinished?.Invoke(this, Resources.M_Finished);
            return endTask.Result;
        }

        public (int threadNumber, ulong diceRollCount, int diceRollResult, TimeSpan elapsedTime) GetResult(string message)
        {
            var threadNumber = int.Parse(message.Split(',').First());
            var (diceRollCount, diceRollResult, elapsedTime) = this.diceRollerThreads[threadNumber].GetResult(message);
            return (threadNumber, diceRollCount, diceRollResult, elapsedTime);
        }

        public (ulong diceRollCount, int diceRollResult, TimeSpan elapsedTime, ulong rollsPerSecond) GetFinalResult()
        {
            ulong diceRollCount = 0;
            foreach(var diceRollerThread in this.diceRollerThreads)
            {
                diceRollCount += diceRollerThread.DiceRollCount;
            }

            var diceRollResult = this.diceRollerThreads.Where(x => x.DiceRollResult == 0).FirstOrDefault();
            if ((diceRollResult == null) || (diceRollResult == default))
            {
                throw new Exception(Resources.M_UnknownError);
            }

            var elapsedTime = this.diceRollerThreads.First().ElapsedTime;
            var rollsPerSecond = elapsedTime.TotalMilliseconds < 1 ? 0 : diceRollCount / (ulong)(elapsedTime.TotalMilliseconds / 1000);
            return (diceRollCount, diceRollResult.DiceRollResult, elapsedTime, rollsPerSecond);
        }

        private void OnTimerElapsed(object? sender, string e) => this.OnCalculationFinished?.Invoke(this, e);
    }
}
