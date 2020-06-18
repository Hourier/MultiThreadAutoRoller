using DiceRollExperimentModel.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiceRollExperimentModel
{
    // TODO: その内拡張メソッドにしたいのでDisplay属性を取り敢えず付けておく
    public enum RaceType
    {
        [Display(Name = "人間")]
        Human = 0,
        [Display(Name = "ハーフエルフ")]
        HalfElf = 1,
        [Display(Name = "エルフ")]
        Elf = 2,
        [Display(Name = "ホビット")]
        Hobbit = 3,
        [Display(Name = "ノーム")]
        Gnome = 4,
        [Display(Name = "ドワーフ")]
        Dwarf = 5,
        [Display(Name = "ハーフオーク")]
        HalfOrc = 6,
        [Display(Name = "ハーフトロル")]
        HalfTroll = 7,
        [Display(Name = "アンバライト")]
        Amberite = 8,
        [Display(Name = "ハイエルフ")]
        HighElf = 9,
        [Display(Name = "野蛮人")]
        Barbariann = 10,
        [Display(Name = "ハーフオーガ")]
        HalfOgre = 11,
        [Display(Name = "半巨人")]
        HalfGiant = 12,
        [Display(Name = "半タイタン")]
        HalfTitan = 13,
        [Display(Name = "サイクロプス")]
        Cyclops = 14,
        [Display(Name = "イーク")]
        Yeek = 15,
        [Display(Name = "クラッコン")]
        Klackons = 16,
        [Display(Name = "コボルド")]
        Kobold = 17,
        [Display(Name = "ニーベルング")]
        Nibelung = 18,
        [Display(Name = "ダークエルフ")]
        DarkElf = 19,
        [Display(Name = "ドラコニアン")]
        Doraconian = 20,
        [Display(Name = "マインドフレア")]
        MindFlayer = 21,
        [Display(Name = "インプ")]
        Imp = 22,
        [Display(Name = "ゴーレム")]
        Golem = 23,
        [Display(Name = "骸骨")]
        Skeleton = 24,
        [Display(Name = "ゾンビ")]
        Zombie = 25,
        [Display(Name = "吸血鬼")]
        Vampire = 26,
        [Display(Name = "幽霊")]
        Spectre = 27,
        [Display(Name = "妖精")]
        Fairy = 28,
        [Display(Name = "獣人")]
        Beastman = 29,
        [Display(Name = "エント")]
        Ent = 30,
        [Display(Name = "アルコン")]
        Archon = 31,
        [Display(Name = "バルログ")]
        Barlog = 32,
        [Display(Name = "ドゥナダン")]
        Dunedan = 33,
        [Display(Name = "影フェアリー")]
        ShadowFairy = 34,
        [Display(Name = "クター")]
        Kutar = 35,
        [Display(Name = "アンドロイド")]
        Android = 36,
        [Display(Name = "マーフォーク")]
        Merflok = 37,
    }

    public class PlayerRace
    {
        private readonly Dictionary<RaceType, string> raceMap = new Dictionary<RaceType, string>();

        public PlayerRace()
        {
            this.raceMap.Add(RaceType.Human, "人間");
            this.raceMap.Add(RaceType.HalfElf, "ハーフエルフ");
            this.raceMap.Add(RaceType.Elf, "エルフ");
            this.raceMap.Add(RaceType.Hobbit, "ホビット");
            this.raceMap.Add(RaceType.Gnome, "ノーム");
            this.raceMap.Add(RaceType.Dwarf, "ドワーフ");
            this.raceMap.Add(RaceType.HalfOrc, "ハーフオーク");
            this.raceMap.Add(RaceType.HalfTroll, "ハーフトロル");
            this.raceMap.Add(RaceType.Amberite, "アンバライト");
            this.raceMap.Add(RaceType.HighElf, "ハイ＝エルフ");
            this.raceMap.Add(RaceType.Barbariann, "野蛮人");
            this.raceMap.Add(RaceType.HalfOgre, "ハーフオーガ");
            this.raceMap.Add(RaceType.HalfGiant, "半巨人");
            this.raceMap.Add(RaceType.HalfTitan, "半タイタン");
            this.raceMap.Add(RaceType.Cyclops, "サイクロプス");
            this.raceMap.Add(RaceType.Yeek, "イーク");
            this.raceMap.Add(RaceType.Klackons, "クラッコン");
            this.raceMap.Add(RaceType.Kobold, "コボルド");
            this.raceMap.Add(RaceType.Nibelung, "ニーベルング");
            this.raceMap.Add(RaceType.DarkElf, "ダークエルフ");
            this.raceMap.Add(RaceType.Doraconian, "ドラコニアン");
            this.raceMap.Add(RaceType.MindFlayer, "マインドフレア");
            this.raceMap.Add(RaceType.Imp, "インプ");
            this.raceMap.Add(RaceType.Golem, "ゴーレム");
            this.raceMap.Add(RaceType.Skeleton, "骸骨");
            this.raceMap.Add(RaceType.Zombie, "ゾンビ");
            this.raceMap.Add(RaceType.Vampire, "吸血鬼");
            this.raceMap.Add(RaceType.Spectre, "幽霊");
            this.raceMap.Add(RaceType.Fairy, "妖精");
            this.raceMap.Add(RaceType.Beastman, "獣人");
            this.raceMap.Add(RaceType.Ent, "エント");
            this.raceMap.Add(RaceType.Archon, "アルコン");
            this.raceMap.Add(RaceType.Barlog, "バルログ");
            this.raceMap.Add(RaceType.Dunedan, "ドゥナダン");
            this.raceMap.Add(RaceType.ShadowFairy, "影フェアリー");
            this.raceMap.Add(RaceType.Kutar, "クター");
            this.raceMap.Add(RaceType.Android, "アンドロイド");
            this.raceMap.Add(RaceType.Merflok, "マーフォーク");
        }

        public IReadOnlyDictionary<RaceType, string> RaceMap => this.raceMap;

        public RaceType GetPlayerRace(string value)
        {
            if (!int.TryParse(value, out var raceValue))
            {
                throw new ArgumentException(Resources.M_InvalidValue);
            }

            if (!Enum.IsDefined(typeof(RaceType), raceValue))
            {
                throw new ArgumentException(Resources.M_UndefinedValue);
            }

            return (RaceType)raceValue;
        }

    }
}
