using System.Collections.Generic;
using System.Text;

namespace DiceRollExperimentModel
{
    public class PlayerDescription
    {
        private readonly IReadOnlyDictionary<PersonalityType, string> personalityMap;
        private readonly IReadOnlyDictionary<ClassType, string> classMap;

        public PlayerDescription(PlayerPersonality playerPersonality, PlayerClass playerClass)
        {
            this.personalityMap = playerPersonality.PersonalityMap;
            this.classMap = playerClass.ClassMap;
        }

        public string GetDescription(PersonalityType personalityType, ClassType classType)
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

            builder.Append(this.classMap[classType]);
            builder.Append("です");
            return builder.ToString();
        }
    }
}
