using System.Collections.Generic;
using System.Text;

namespace DiceRollExperimentModel
{
    public class PlayerDescription
    {
        private readonly IReadOnlyDictionary<SexType, string> sexMap;
        private readonly IReadOnlyDictionary<RaceType, string> raceMap;
        private readonly IReadOnlyDictionary<PersonalityType, string> personalityMap;
        private readonly IReadOnlyDictionary<ClassType, string> classMap;

        public PlayerDescription(PlayerSex playerSex, PlayerRace playerRace, PlayerPersonality playerPersonality, PlayerClass playerClass)
        {
            this.sexMap = playerSex.SexMap;
            this.raceMap = playerRace.RaceMap;
            this.personalityMap = playerPersonality.PersonalityMap;
            this.classMap = playerClass.ClassMap;
        }

        public string GetDescription(SexType sexType, RaceType raceType, PersonalityType personalityType, ClassType classType)
        {
            var builder = new StringBuilder();
            builder.Append("あなたは");
            builder.Append(this.raceMap[raceType]);
            builder.Append("の");
            builder.Append(this.sexMap[sexType]); // TODO: 後で変数化する.
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
