using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiceRollExperimentModel
{
    // TODO: その内拡張メソッドにしたいのでDisplay属性を取り敢えず付けておく
    public enum PersonalityType
    {
        [Display(Name = "ふつう")]
        Ordinary = 0,
        [Display(Name = "ちからじまん")]
        Mighty = 1,
        [Display(Name = "きれもの")]
        Shrewd = 2,
        [Display(Name = "しあわせもの")]
        Pious = 3,
        [Display(Name = "すばしっこい")]
        Nimble = 4,
        [Display(Name = "いのちしらず")]
        Fearless = 5,
        [Display(Name = "コンバット")]
        Combat = 6,
        [Display(Name = "なまけもの")]
        Lazy = 7,
        [Display(Name = "セクシーギャル")]
        Sexy = 8,
        [Display(Name = "ラッキーマン")]
        Lucky = 9,
        [Display(Name = "がまんづよい")]
        Patient = 10,
        [Display(Name = "いかさま")]
        Munchikin = 11,
        [Display(Name = "チャージマン")]
        Chargeman = 12,
    }

    public class PlayerPersonality
    {
        private readonly Dictionary<PersonalityType, string> personalityMap = new Dictionary<PersonalityType, string>();

        public PlayerPersonality()
        {
            this.personalityMap.Add(PersonalityType.Ordinary, "ふつう");
            this.personalityMap.Add(PersonalityType.Mighty, "ちからじまん");
            this.personalityMap.Add(PersonalityType.Shrewd, "きれもの");
            this.personalityMap.Add(PersonalityType.Pious, "しあわせもの");
            this.personalityMap.Add(PersonalityType.Nimble, "すばしっこい");
            this.personalityMap.Add(PersonalityType.Fearless, "いのちしらず");
            this.personalityMap.Add(PersonalityType.Combat, "コンバット");
            this.personalityMap.Add(PersonalityType.Lazy, "なまけもの");
            this.personalityMap.Add(PersonalityType.Sexy, "セクシーギャル");
            this.personalityMap.Add(PersonalityType.Lucky, "ラッキーマン");
            this.personalityMap.Add(PersonalityType.Patient, "がまんづよい");
            this.personalityMap.Add(PersonalityType.Munchikin, "いかさま");
            this.personalityMap.Add(PersonalityType.Chargeman, "チャージマン");
        }

        public IReadOnlyDictionary<PersonalityType, string> PersonalityMap => this.personalityMap;
    }
}
