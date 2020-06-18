using DiceRollExperimentModel.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiceRollExperimentModel
{
    // TODO: その内拡張メソッドにしたいのでDisplay属性を取り敢えず付けておく
    public enum ClassType
    {
        [Display(Name = "戦士")]
        Warrior = 0,
        [Display(Name = "メイジ")]
        Mage = 1,
        [Display(Name = "プリースト")]
        Priest = 2,
        [Display(Name = "盗賊")]
        Rogue = 3,
        [Display(Name = "レンジャー")]
        Ranger = 4,
        [Display(Name = "パラディン")]
        Paladin = 5,
        [Display(Name = "魔法戦士")]
        WarriorMage = 6,
        [Display(Name = "混沌の戦士")]
        ChaosWarrior = 7,
        [Display(Name = "修行僧")]
        Monk = 8,
        [Display(Name = "超能力者")]
        MindCrafter = 9,
        [Display(Name = "ハイ＝メイジ")]
        HighMage = 10,
        [Display(Name = "観光客")]
        Tourist = 11,
        [Display(Name = "ものまね師")]
        Imitator = 12,
        [Display(Name = "魔獣使い")]
        BeastMaster = 13,
        [Display(Name = "スペルマスター")]
        Sorcerer = 14,
        [Display(Name = "アーチャー")]
        Archer = 15,
        [Display(Name = "魔道具術師")]
        MagicEater = 16,
        [Display(Name = "吟遊詩人")]
        Bard = 17,
        [Display(Name = "赤魔導師")]
        RedMage = 18,
        [Display(Name = "剣術家")]
        Samurai = 19,
        [Display(Name = "練気術師")]
        ForceTrainer = 20,
        [Display(Name = "青魔導師")]
        BlueMage = 21,
        [Display(Name = "騎兵")]
        Cavalry = 22,
        [Display(Name = "狂戦士")]
        Berserker = 23,
        [Display(Name = "鍛冶師")]
        WeaponSmith = 24,
        [Display(Name = "鏡使い")]
        MirrorMaster = 25,
        [Display(Name = "忍者")]
        Ninja = 26,
        [Display(Name = "スナイパー")]
        Sniper = 27,
    }

    public class PlayerClass
    {
        private readonly Dictionary<ClassType, string> classMap = new Dictionary<ClassType, string>();

        public PlayerClass()
        {
            this.classMap.Add(ClassType.Warrior, "戦士");
            this.classMap.Add(ClassType.Mage, "メイジ");
            this.classMap.Add(ClassType.Priest, "プリースト");
            this.classMap.Add(ClassType.Rogue, "盗賊");
            this.classMap.Add(ClassType.Ranger, "レンジャー");
            this.classMap.Add(ClassType.Paladin, "パラディン");
            this.classMap.Add(ClassType.WarriorMage, "魔法戦士");
            this.classMap.Add(ClassType.ChaosWarrior, "混沌の戦士");
            this.classMap.Add(ClassType.Monk, "修行僧");
            this.classMap.Add(ClassType.MindCrafter, "超能力者");
            this.classMap.Add(ClassType.HighMage, "ハイ＝メイジ");
            this.classMap.Add(ClassType.Tourist, "観光客");
            this.classMap.Add(ClassType.Imitator, "ものまね師");
            this.classMap.Add(ClassType.BeastMaster, "魔獣使い");
            this.classMap.Add(ClassType.Sorcerer, "スペルマスター");
            this.classMap.Add(ClassType.Archer, "アーチャー");
            this.classMap.Add(ClassType.MagicEater, "魔道具術師");
            this.classMap.Add(ClassType.Bard, "吟遊詩人");
            this.classMap.Add(ClassType.RedMage, "赤魔導師");
            this.classMap.Add(ClassType.Samurai, "剣術家");
            this.classMap.Add(ClassType.ForceTrainer, "練気術師");
            this.classMap.Add(ClassType.BlueMage, "青魔導師");
            this.classMap.Add(ClassType.Cavalry, "騎兵");
            this.classMap.Add(ClassType.Berserker, "狂戦士");
            this.classMap.Add(ClassType.WeaponSmith, "鍛冶師");
            this.classMap.Add(ClassType.MirrorMaster, "鏡使い");
            this.classMap.Add(ClassType.Ninja, "忍者");
            this.classMap.Add(ClassType.Sniper, "スナイパー");
        }

        public IReadOnlyDictionary<ClassType, string> ClassMap => this.classMap;

        public ClassType GetPlayerClass(string value)
        {
            if (!int.TryParse(value, out var classValue))
            {
                throw new ArgumentException(Resources.M_InvalidValue);
            }

            if (!Enum.IsDefined(typeof(ClassType), classValue))
            {
                throw new ArgumentException(Resources.M_UndefinedValue);
            }

            return (ClassType)classValue;
        }
    }
}
