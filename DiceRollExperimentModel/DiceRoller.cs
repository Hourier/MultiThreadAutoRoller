using System.Text;

namespace DiceRollExperimentModel
{
    public class DiceRoller : IDiceRoller
    {
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
    }
}
