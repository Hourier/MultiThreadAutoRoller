using System.Collections.Generic;
using System.Text;

namespace DiceRollExperimentModel
{
    public class PlayerDescription
    {
        private readonly IReadOnlyDictionary<PersonalityType, string> personalityMap;

        public PlayerDescription(PlayerPersonality playerPersonality) => this.personalityMap = playerPersonality.PersonalityMap;

        public string GetDescription(PersonalityType personalityType)
        {
            var builder = new StringBuilder();
            builder.Append("あなたは");
            builder.Append("人間"); // TODO: 後で変数化する.
            builder.Append("の");
            builder.Append("女性"); // TODO: 後で変数化する.
            builder.Append("で");
            builder.Append(this.personalityMap[personalityType]);
            if (personalityType != PersonalityType.Nimble)
            {
                builder.Append("の");
            }

            builder.Append("戦士"); // TODO: 後で変数化する.
            builder.Append("です");
            return builder.ToString();
        }
    }
}
