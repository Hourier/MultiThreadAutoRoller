using System;
using System.ComponentModel;
using System.Text;

namespace DiceRollExperimentModel
{
    public class DiceRoller : IDiceRoller
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private const int MaxDiceNumber = 100000000;
        private Random random = new Random();

        public DiceRoller()
        {
        }

        public int DiceRollCount { get; private set; }

        public int DiceRollResult { get; private set; }

        public TimeSpan ElapsedTime { get; private set; }

        public string GetName()
        {
            var builder = new StringBuilder();
            builder.Append("あなたは");
            builder.Append("人間"); // TODO: 後で変数化する.
            builder.Append("の");
            builder.Append("女性"); // TODO: 後で変数化する.
            builder.Append("で");
            builder.Append("ふつう"); // TODO: 後で変数化する.
            builder.Append("の");
            builder.Append("戦士"); // TODO: 後で変数化する.
            builder.Append("です");
            return builder.ToString();
        }

        public void StartRoll()
        {
            this.ResetResult();
            var startTime = DateTime.Now;
            var timeCount = 0;
            while(true)
            {
                var tempResult = this.random.Next(0, MaxDiceNumber);
                this.DiceRollCount++;
                if (tempResult == 0)
                {
                    this.DiceRollResult = tempResult;
                    break;
                }

                this.ElapsedTime = DateTime.Now - startTime;
                if (this.ElapsedTime.TotalMilliseconds / 100 > timeCount)
                {
                    timeCount++;
                    this.DiceRollResult = tempResult;
                    this.OnTimerElapsed();
                }
            }

            this.ElapsedTime = DateTime.Now - startTime;
            this.OnTimerElapsed();
        }

        private void ResetResult()
        {
            this.DiceRollCount = 0;
            this.DiceRollResult = 0;
            this.ElapsedTime = TimeSpan.Zero;
        }

        private void OnTimerElapsed() => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.DiceRollResult)));
    }
}
