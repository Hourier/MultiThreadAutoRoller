using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiceRollExperimentModel
{
    public enum RealmType
    {
        [Display(Name = "なし")]
        None = 0,
        [Display(Name = "生命")]
        Life = 1,
        [Display(Name = "仙術")]
        Sorcery = 2,
        [Display(Name = "自然")]
        Nature = 3,
        [Display(Name = "カオス")]
        Chaos = 4,
        [Display(Name = "暗黒")]
        Death = 5,
        [Display(Name = "トランプ")]
        Trump = 6,
        [Display(Name = "秘術")]
        Arcane = 7,
        [Display(Name = "匠")]
        Craft = 8,
        [Display(Name = "悪魔")]
        Demon = 9,
        [Display(Name = "破邪")]
        Crusade = 10,
        [Display(Name = "歌集")]
        Song = 11,
        [Display(Name = "武芸")]
        Kenjutsu = 12,
        [Display(Name = "呪術")]
        Hex = 13,
    }

    public class PlayerRealm
    {
        private const string realmNone = "なし";
        private const string realmLife = "生命";
        private const string realmSorcery = "仙術";
        private const string realmNature = "自然";
        private const string realmChaos = "カオス";
        private const string realmDeath = "暗黒";
        private const string realmTrump = "トランプ";
        private const string realmArcane = "秘術";
        private const string realmCraft = "匠";
        private const string realmDemon = "悪魔";
        private const string realmCrusade = "破邪";
        private const string realmSong = "歌集";
        private const string realmKenjutsu = "武芸";
        private const string realmHex = "呪術";
        private readonly Dictionary<RealmType, string> mageRealmMap = new Dictionary<RealmType, string>();
        private readonly Dictionary<RealmType, string> priestRealmMapFirst = new Dictionary<RealmType, string>();
        private readonly Dictionary<RealmType, string> priestRealmMapSecond = new Dictionary<RealmType, string>();
        private readonly Dictionary<RealmType, string> rogueRealmMap = new Dictionary<RealmType, string>();
        private readonly Dictionary<RealmType, string> rangerRealmMapFirst = new Dictionary<RealmType, string>();
        private readonly Dictionary<RealmType, string> rangerRealmMapSecond = new Dictionary<RealmType, string>();
        private readonly Dictionary<RealmType, string> paladinRealmMap = new Dictionary<RealmType, string>();
        private readonly Dictionary<RealmType, string> warriorMageRealmMapFirst = new Dictionary<RealmType, string>();
        private readonly Dictionary<RealmType, string> warriorMageRealmMapSecond = new Dictionary<RealmType, string>();
        private readonly Dictionary<RealmType, string> chaosWarriorMageRealmMap = new Dictionary<RealmType, string>();
        private readonly Dictionary<RealmType, string> monkRealmMap = new Dictionary<RealmType, string>();
        private readonly Dictionary<RealmType, string> highMageRealmMap = new Dictionary<RealmType, string>();
        private readonly Dictionary<RealmType, string> touristRealmMap = new Dictionary<RealmType, string>();
        private readonly Dictionary<RealmType, string> beastMasterRealmMap = new Dictionary<RealmType, string>();
        private readonly Dictionary<RealmType, string> forceTrainerRealmMap = new Dictionary<RealmType, string>();
        private readonly Dictionary<RealmType, string> bardRealmMap = new Dictionary<RealmType, string>();
        private readonly Dictionary<RealmType, string> samuraiRealmMap = new Dictionary<RealmType, string>();
        private readonly Dictionary<RealmType, string> warriorRealmMap = new Dictionary<RealmType, string>();

        public PlayerRealm()
        {
            this.mageRealmMap.Add(RealmType.Life, realmLife);
            this.mageRealmMap.Add(RealmType.Sorcery, realmSorcery);
            this.mageRealmMap.Add(RealmType.Nature, realmNature);
            this.mageRealmMap.Add(RealmType.Chaos, realmChaos);
            this.mageRealmMap.Add(RealmType.Death, realmDeath);
            this.mageRealmMap.Add(RealmType.Trump, realmTrump);
            this.mageRealmMap.Add(RealmType.Arcane, realmArcane);
            this.mageRealmMap.Add(RealmType.Craft, realmCraft);
            this.mageRealmMap.Add(RealmType.Demon, realmDemon);
            this.mageRealmMap.Add(RealmType.Crusade, realmCrusade);

            this.priestRealmMapFirst.Add(RealmType.Life, realmLife);
            this.priestRealmMapFirst.Add(RealmType.Death, realmDeath);
            this.priestRealmMapFirst.Add(RealmType.Demon, realmDemon);
            this.priestRealmMapFirst.Add(RealmType.Crusade, realmCrusade);
            this.priestRealmMapSecond.Add(RealmType.Life, realmLife);
            this.priestRealmMapSecond.Add(RealmType.Sorcery, realmSorcery);
            this.priestRealmMapSecond.Add(RealmType.Nature, realmNature);
            this.priestRealmMapSecond.Add(RealmType.Chaos, realmChaos);
            this.priestRealmMapSecond.Add(RealmType.Death, realmDeath);
            this.priestRealmMapSecond.Add(RealmType.Trump, realmTrump);
            this.priestRealmMapSecond.Add(RealmType.Arcane, realmArcane);
            this.priestRealmMapSecond.Add(RealmType.Craft, realmCraft);
            this.priestRealmMapSecond.Add(RealmType.Demon, realmDemon);
            this.priestRealmMapSecond.Add(RealmType.Crusade, realmCrusade);

            this.rogueRealmMap.Add(RealmType.Sorcery, realmSorcery);
            this.rogueRealmMap.Add(RealmType.Death, realmDeath);
            this.rogueRealmMap.Add(RealmType.Trump, realmTrump);
            this.rogueRealmMap.Add(RealmType.Arcane, realmArcane);
            this.rogueRealmMap.Add(RealmType.Craft, realmCraft);

            this.rangerRealmMapFirst.Add(RealmType.Nature, realmNature);
            this.rangerRealmMapSecond.Add(RealmType.Sorcery, realmSorcery);
            this.rangerRealmMapSecond.Add(RealmType.Chaos, realmChaos);
            this.rangerRealmMapSecond.Add(RealmType.Death, realmDeath);
            this.rangerRealmMapSecond.Add(RealmType.Trump, realmTrump);
            this.rangerRealmMapSecond.Add(RealmType.Arcane, realmArcane);
            this.rangerRealmMapSecond.Add(RealmType.Demon, realmDemon);

            this.paladinRealmMap.Add(RealmType.Crusade, realmCrusade);
            this.paladinRealmMap.Add(RealmType.Death, realmDeath);

            this.warriorMageRealmMapFirst.Add(RealmType.Arcane, realmArcane);
            this.warriorMageRealmMapSecond.Add(RealmType.Life, realmLife);
            this.warriorMageRealmMapSecond.Add(RealmType.Sorcery, realmSorcery);
            this.warriorMageRealmMapSecond.Add(RealmType.Nature, realmNature);
            this.warriorMageRealmMapSecond.Add(RealmType.Chaos, realmChaos);
            this.warriorMageRealmMapSecond.Add(RealmType.Death, realmDeath);
            this.warriorMageRealmMapSecond.Add(RealmType.Trump, realmTrump);
            this.warriorMageRealmMapSecond.Add(RealmType.Craft, realmCraft);
            this.warriorMageRealmMapSecond.Add(RealmType.Demon, realmDemon);
            this.warriorMageRealmMapSecond.Add(RealmType.Crusade, realmCrusade);

            this.chaosWarriorMageRealmMap.Add(RealmType.Chaos, realmChaos);
            this.chaosWarriorMageRealmMap.Add(RealmType.Demon, realmDemon);

            this.monkRealmMap.Add(RealmType.Life, realmLife);
            this.monkRealmMap.Add(RealmType.Nature, realmNature);
            this.monkRealmMap.Add(RealmType.Death, realmDeath);
            this.monkRealmMap.Add(RealmType.Craft, realmCraft);

            this.highMageRealmMap.Add(RealmType.Life, realmLife);
            this.highMageRealmMap.Add(RealmType.Sorcery, realmSorcery);
            this.highMageRealmMap.Add(RealmType.Nature, realmNature);
            this.highMageRealmMap.Add(RealmType.Chaos, realmChaos);
            this.highMageRealmMap.Add(RealmType.Death, realmDeath);
            this.highMageRealmMap.Add(RealmType.Trump, realmTrump);
            this.highMageRealmMap.Add(RealmType.Arcane, realmArcane);
            this.highMageRealmMap.Add(RealmType.Craft, realmCraft);
            this.highMageRealmMap.Add(RealmType.Demon, realmDemon);
            this.highMageRealmMap.Add(RealmType.Crusade, realmCrusade);
            this.highMageRealmMap.Add(RealmType.Hex, realmHex);

            this.touristRealmMap.Add(RealmType.Arcane, realmArcane);

            this.beastMasterRealmMap.Add(RealmType.Trump, realmTrump);

            this.forceTrainerRealmMap.Add(RealmType.Life, realmLife);
            this.forceTrainerRealmMap.Add(RealmType.Nature, realmNature);
            this.forceTrainerRealmMap.Add(RealmType.Death, realmDeath);
            this.forceTrainerRealmMap.Add(RealmType.Craft, realmCraft);

            this.bardRealmMap.Add(RealmType.Song, realmSong);

            this.samuraiRealmMap.Add(RealmType.Kenjutsu, realmKenjutsu);

            this.warriorRealmMap.Add(RealmType.None, realmNone);
        }

        public bool HasFirstRealm(ClassType playerClass)
        {
            switch (playerClass)
            {
                case ClassType.Mage:
                case ClassType.Priest:
                case ClassType.Rogue:
                case ClassType.Ranger:
                case ClassType.Paladin:
                case ClassType.WarriorMage:
                case ClassType.ChaosWarrior:
                case ClassType.Monk:
                case ClassType.HighMage:
                case ClassType.Tourist:
                case ClassType.BeastMaster:
                case ClassType.ForceTrainer:
                case ClassType.Bard:
                case ClassType.Samurai:
                    return true;
                default:
                    return false;
            }
        }

        public bool HasSecondRealm(ClassType playerClass)
        {
            switch (playerClass)
            {
                case ClassType.Mage:
                case ClassType.Priest:
                case ClassType.Ranger:
                case ClassType.WarriorMage:
                    return true;
                default:
                    return false;
            }
        }

        public IReadOnlyDictionary<RealmType, string> GetRealm(ClassType playerClass, bool isFirstRealm)
        {
            return playerClass switch
            {
                ClassType.Mage => this.mageRealmMap,
                ClassType.Priest => isFirstRealm ? this.priestRealmMapFirst : this.priestRealmMapSecond,
                ClassType.Rogue => this.rogueRealmMap,
                ClassType.Ranger => isFirstRealm ? this.rangerRealmMapFirst : this.rangerRealmMapSecond,
                ClassType.Paladin => this.paladinRealmMap,
                ClassType.WarriorMage => isFirstRealm ? this.warriorMageRealmMapFirst : this.warriorMageRealmMapSecond,
                ClassType.ChaosWarrior => this.chaosWarriorMageRealmMap,
                ClassType.Monk => this.monkRealmMap,
                ClassType.HighMage => this.highMageRealmMap,
                ClassType.Tourist => this.touristRealmMap,
                ClassType.BeastMaster => this.beastMasterRealmMap,
                ClassType.ForceTrainer => this.forceTrainerRealmMap,
                ClassType.Bard => this.bardRealmMap,
                ClassType.Samurai => this.samuraiRealmMap,
                _ => this.warriorRealmMap,
            };
        }
    }
}
