using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace DiceRollExperimentModel
{
    internal class DiceRollerThread
    {
        private const int MaxDiceNumber = int.MaxValue;
        private readonly Random random;
        private readonly int threadNumber;

        internal DiceRollerThread(int threadNumber)
        {
            this.threadNumber = threadNumber;
            using var rngProvider = new RNGCryptoServiceProvider();
            var randomizedNumbers = new byte[4];
            rngProvider.GetBytes(randomizedNumbers);
            var seed = BitConverter.ToInt32(randomizedNumbers, 0);
            this.random = new Random(seed);
        }

        public event EventHandler<string>? OnCalculationFinished;

        internal ulong DiceRollCount { get; private set; }

        internal int DiceRollResult { get; private set; }

        internal TimeSpan ElapsedTime { get; private set; }

        internal ulong RunDiceRoll(DateTime startTime, CancellationToken cancellationToken)
        {
            this.ResetResult();
            var timeCount = (ulong)0;
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                var tempResult = this.random.Next(0, MaxDiceNumber);
                this.DiceRollCount++;
                if (tempResult == 0)
                {
                    this.DiceRollResult = tempResult;
                    break;
                }

                if (this.threadNumber != 0)
                {
                    continue;
                }

                this.ElapsedTime = DateTime.Now - startTime;
                if (this.ElapsedTime.TotalMilliseconds / 100 <= timeCount)
                {
                    continue;
                }

                timeCount++;
                this.DiceRollResult = tempResult;
                this.OnTimerElapsed();
            }

            this.ElapsedTime = DateTime.Now - startTime;
            this.OnTimerElapsed();
            return timeCount;
        }

        internal (ulong diceRollCount, int diceRollResult, TimeSpan elapsedTime) GetResult(string message)
        {
            var messages = message.Split(',').ToList();
            if (messages.Count() != 5)
            {
                return (0, 0, TimeSpan.Zero);
            }

            return (ulong.Parse(messages[1]), int.Parse(messages[2]), TimeSpan.Parse(messages[3]));
        }

        private void ResetResult()
        {
            this.DiceRollCount = 0;
            this.DiceRollResult = 0;
            this.ElapsedTime = TimeSpan.Zero;
        }

        private void OnTimerElapsed()
        {
            var builder = new StringBuilder();
            builder.Append(this.threadNumber);
            builder.Append(',');
            builder.Append(this.DiceRollCount);
            builder.Append(',');
            builder.Append(this.DiceRollResult);
            builder.Append(',');
            builder.Append(this.ElapsedTime);
            builder.Append(",0");
            this.OnCalculationFinished?.Invoke(this, builder.ToString());
        }
    }
}
