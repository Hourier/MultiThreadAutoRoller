using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiceRollExperimentModel
{
    public class DiceRoller : IDiceRoller
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly List<DiceRollerThread> diceRollerThreads = new List<DiceRollerThread>();

        public DiceRoller()
        {
        }

        public int DiceRollResult { get; private set; }

        public async Task<ulong> StartRoll()
        {
            this.diceRollerThreads.Clear();
            var cpuThreads = Environment.ProcessorCount;
            for (var i = 0; i < cpuThreads; i++)
            {
                var diceRollerThread = new DiceRollerThread(i);
                this.diceRollerThreads.Add(diceRollerThread);
                if (i == 0)
                {
                    diceRollerThread.PropertyChanged += this.OnTimerElapsed;
                }
            }

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
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Finished"));
            return endTask.Result;
        }

        public (int threadNumber, ulong diceRollCount, int diceRollResult, TimeSpan elapsedTime, ulong rollsPerSecond) GetResult(string message)
        {
            var threadNumber = int.Parse(message.Split(',').First());
            var (diceRollCount, diceRollResult, elapsedTime, rollsPerSecond) = this.diceRollerThreads[threadNumber].GetResult(message);
            return (threadNumber, diceRollCount, diceRollResult, elapsedTime, rollsPerSecond);
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
                throw new Exception("Unknown error!");
            }

            var elapsedTime = this.diceRollerThreads.First().ElapsedTime;
            var rollsPerSecond = diceRollCount / (ulong)Environment.ProcessorCount;
            return (diceRollCount, diceRollResult.DiceRollResult, elapsedTime, rollsPerSecond);
        }

        private void OnTimerElapsed(object sender, PropertyChangedEventArgs e)
        {
            this.PropertyChanged?.Invoke(this, e);
        }
    }
}
