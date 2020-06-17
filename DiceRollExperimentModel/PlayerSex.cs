using DiceRollExperimentModel.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiceRollExperimentModel
{
    // TODO: その内拡張メソッドにしたいのでDisplay属性を取り敢えず付けておく
    public enum SexType
    {
        [Display(Name = "女性")]
        Female = 0,
        [Display(Name = "男性")]
        Male = 1,
    }

    public class PlayerSex
    {
        private readonly Dictionary<SexType, string> sexMap = new Dictionary<SexType, string>();

        public PlayerSex()
        {
            this.sexMap.Add(SexType.Female, "女性");
            this.sexMap.Add(SexType.Male, "男性");
        }

        public IReadOnlyDictionary<SexType, string> SexMap => this.sexMap;

        public SexType GetPlayerSex(string value)
        {
            if (!int.TryParse(value, out var sexValue))
            {
                throw new ArgumentException(Resources.M_InvalidValue);
            }

            if (!Enum.IsDefined(typeof(ClassType), sexValue))
            {
                throw new ArgumentException(Resources.M_UndefinedValue);
            }

            return (SexType)sexValue;
        }
    }
}
