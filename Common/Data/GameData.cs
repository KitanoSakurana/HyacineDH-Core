using HyacineCore.Server.Data.Config;
using HyacineCore.Server.Data.Config.AdventureAbility;
using HyacineCore.Server.Data.Config.Character;
using HyacineCore.Server.Data.Config.Scene;
using HyacineCore.Server.Data.Custom;
using HyacineCore.Server.Data.Excel;
using HyacineCore.Server.Util;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using HyacineCore.Server.Enums.Avatar;
using HyacineCore.Server.Enums.Item;

namespace HyacineCore.Server.Data;

public static class GameData
{
    #region Banners

    public static BannersConfig BannersConfig { get; set; } = new();

    #endregion

    #region VideoKeys

    public static VideoKeysConfig VideoKeysConfig { get; set; } = new();

    #endregion

    #region Pam

    public static Dictionary<int, PamSkinConfigExcel> PamSkinConfigData { get; private set; } = [];

    #endregion

    #region Activity

    public static ActivityConfig ActivityConfig { get; set; } = new();

    #region Marble

    public static Dictionary<int, MarbleMatchInfoExcel> MarbleMatchInfoData { get; private set; } = [];
    public static Dictionary<int, MarbleSealExcel> MarbleSealData { get; private set; } = [];

    #endregion

    #endregion

    #region Avatar

    public static Dictionary<int, AvatarConfigExcel> AvatarConfigData { get; private set; } = [];
    public static Dictionary<uint, AvatarRelicRecommendExcel> AvatarRelicRecommendData { get; private set; } = [];
    public static Dictionary<int, AvatarGlobalBuffConfigExcel> AvatarGlobalBuffConfigData { get; private set; } = [];

    public static Dictionary<int, AdventureAbilityConfigListInfo> AdventureAbilityConfigListData { get; private set; } =
        [];

    public static Dictionary<int, AvatarPromotionConfigExcel> AvatarPromotionConfigData { get; private set; } = [];
    public static Dictionary<int, AvatarExpItemConfigExcel> AvatarExpItemConfigData { get; private set; } = [];
    public static Dictionary<int, AvatarSkillTreeConfigExcel> AvatarSkillTreeConfigData { get; private set; } = [];
    public static Dictionary<int, MazeSkillExcel> MazeSkillData { get; private set; } = [];
    public static Dictionary<int, AvatarSkinExcel> AvatarSkinData { get; private set; } = [];
    public static Dictionary<int, AvatarDemoConfigExcel> AvatarDemoConfigData { get; private set; } = [];
    public static Dictionary<int, ExpTypeExcel> ExpTypeData { get; } = [];

    public static Dictionary<int, MultiplePathAvatarConfigExcel> MultiplePathAvatarConfigData { get; private set; } =
        [];

    public static Dictionary<int, AdventurePlayerExcel> AdventurePlayerData { get; private set; } = [];
    public static Dictionary<int, SummonUnitDataExcel> SummonUnitDataData { get; private set; } = [];
    public static Dictionary<int, DecideAvatarOrderExcel> DecideAvatarOrderData { get; private set; } = [];
    public static ConcurrentDictionary<int, CharacterConfigInfo> CharacterConfigInfoData { get; private set; } = [];
    public static Dictionary<AvatarBaseTypeEnum, UpgradeAvatarEquipmentExcel> UpgradeAvatarEquipmentData { get; private set; } =
        [];
    public static Dictionary<uint, UpgradeAvatarSubTypeExcel> UpgradeAvatarSubTypeData { get; private set; } = [];

    public static
        Dictionary<UpgradeAvatarSubRelicTypeEnum, Dictionary<RarityEnum,
            Dictionary<uint, Dictionary<RelicTypeEnum, UpgradeAvatarSubRelicExcel>>>> UpgradeAvatarSubRelicData
    {
        get;
        private set;
    } = [];

    #endregion

    #region Challenge

    public static Dictionary<int, ChallengeConfigExcel> ChallengeConfigData { get; private set; } = [];
    public static Dictionary<int, ChallengeTargetExcel> ChallengeTargetData { get; private set; } = [];
    public static Dictionary<int, ChallengeGroupExcel> ChallengeGroupData { get; private set; } = [];
    public static ChallengePeakOverrideConfig ChallengePeakOverrideConfig { get; set; } = new();

    public static Dictionary<int, ChallengePeakGroupConfigExcel> ChallengePeakGroupConfigData { get; private set; } =
        [];

    public static Dictionary<int, ChallengePeakConfigExcel> ChallengePeakConfigData { get; private set; } = [];
    public static Dictionary<int, ChallengePeakBossConfigExcel> ChallengePeakBossConfigData { get; private set; } = [];
    public static Dictionary<int, List<ChallengeRewardExcel>> ChallengeRewardData { get; private set; } = [];

    #endregion

    #region Battle

    public static Dictionary<int, CocoonConfigExcel> CocoonConfigData { get; private set; } = [];
    public static Dictionary<int, StageConfigExcel> StageConfigData { get; private set; } = [];
    public static Dictionary<int, RaidConfigExcel> RaidConfigData { get; private set; } = [];
    public static Dictionary<int, MazeBuffExcel> MazeBuffData { get; private set; } = [];
    public static Dictionary<int, InteractConfigExcel> InteractConfigData { get; private set; } = [];
    public static Dictionary<int, NPCMonsterDataExcel> NpcMonsterDataData { get; private set; } = [];
    public static Dictionary<int, MonsterConfigExcel> MonsterConfigData { get; private set; } = [];
    public static Dictionary<int, MonsterTemplateConfigExcel> MonsterTemplateConfigData { get; private set; } = [];
    public static Dictionary<int, MonsterDropExcel> MonsterDropData { get; private set; } = [];
    public static Dictionary<int, BattleCollegeConfigExcel> BattleCollegeConfigData { get; private set; } = [];
    public static Dictionary<int, BattleTargetConfigExcel> BattleTargetConfigData { get; private set; } = [];

    #endregion
    #region Player

    public static Dictionary<int, AchievementDataExcel> AchievementDataData { get; private set; } = [];
    public static Dictionary<int, QuestDataExcel> QuestDataData { get; private set; } = [];
    public static Dictionary<int, FinishWayExcel> FinishWayData { get; private set; } = [];
    public static Dictionary<int, PlayerLevelConfigExcel> PlayerLevelConfigData { get; } = [];
    public static Dictionary<int, BackGroundMusicExcel> BackGroundMusicData { get; private set; } = [];
    public static Dictionary<int, ChatBubbleConfigExcel> ChatBubbleConfigData { get; private set; } = [];
    public static Dictionary<string, RechargeConfigExcel> RechargeConfigData { get; private set; } = [];
    public static Dictionary<int, RechargeGiftConfigExcel> RechargeGiftConfigData { get; private set; } = [];

    #endregion

    #region Offering

    public static Dictionary<int, OfferingTypeConfigExcel> OfferingTypeConfigData { get; private set; } = [];

    public static Dictionary<int, Dictionary<int, OfferingLevelConfigExcel>> OfferingLevelConfigData
    {
        get;
        private set;
    } = [];

    #endregion

    #region Maze

    [JsonConverter(typeof(ConcurrentDictionaryConverter<string, FloorInfo>))]
    public static ConcurrentDictionary<string, FloorInfo> FloorInfoData { get; } = [];

    public static Dictionary<int, NPCDataExcel> NpcDataData { get; private set; } = [];
    public static Dictionary<int, MapEntranceExcel> MapEntranceData { get; } = [];
    public static Dictionary<int, MazePlaneExcel> MazePlaneData { get; private set; } = [];
    public static Dictionary<int, MazePuzzleSwitchHandExcel> MazePuzzleSwitchHandData { get; private set; } = [];
    public static Dictionary<int, MazeChestExcel> MazeChestData { get; private set; } = [];
    public static Dictionary<int, MazePropExcel> MazePropData { get; private set; } = [];
    public static Dictionary<int, PlaneEventExcel> PlaneEventData { get; private set; } = [];
    public static Dictionary<int, ContentPackageConfigExcel> ContentPackageConfigData { get; private set; } = [];
    public static Dictionary<int, GroupSystemUnlockDataExcel> GroupSystemUnlockDataData { get; private set; } = [];
    public static Dictionary<int, FuncUnlockDataExcel> FuncUnlockDataData { get; private set; } = [];
    public static Dictionary<int, MusicRhythmLevelExcel> MusicRhythmLevelData { get; private set; } = [];
    public static Dictionary<int, MusicRhythmGroupExcel> MusicRhythmGroupData { get; private set; } = [];
    public static Dictionary<int, MusicRhythmPhaseExcel> MusicRhythmPhaseData { get; private set; } = [];
    public static Dictionary<int, MusicRhythmSongExcel> MusicRhythmSongData { get; private set; } = [];
    public static Dictionary<int, MusicRhythmSoundEffectExcel> MusicRhythmSoundEffectData { get; private set; } = [];
    public static Dictionary<int, MusicRhythmTrackExcel> MusicRhythmTrackData { get; private set; } = [];

    public static Dictionary<string, AdventureModifierConfig> AdventureModifierData { get; set; } = [];
    public static SceneRainbowGroupPropertyConfig SceneRainbowGroupPropertyData { get; set; } = new();

    #endregion
    #region Items

    public static Dictionary<int, MappingInfoExcel> MappingInfoData { get; private set; } = [];
    public static Dictionary<int, ItemConfigExcel> ItemConfigData { get; private set; } = [];
    public static Dictionary<int, ItemUseBuffDataExcel> ItemUseBuffDataData { get; private set; } = [];
    public static Dictionary<int, ItemUseDataExcel> ItemUseDataData { get; private set; } = [];
    public static Dictionary<int, EquipmentConfigExcel> EquipmentConfigData { get; private set; } = [];
    public static Dictionary<int, EquipmentExpTypeExcel> EquipmentExpTypeData { get; } = [];
    public static Dictionary<int, EquipmentExpItemConfigExcel> EquipmentExpItemConfigData { get; private set; } = [];

    public static Dictionary<int, EquipmentPromotionConfigExcel> EquipmentPromotionConfigData { get; private set; } =
        [];

    public static Dictionary<int, Dictionary<int, RelicMainAffixConfigExcel>> RelicMainAffixData { get; private set; } =
        []; // groupId, affixId

    public static Dictionary<int, Dictionary<int, RelicSubAffixConfigExcel>> RelicSubAffixData { get; private set; } =
        []; // groupId, affixId

    public static Dictionary<int, RelicConfigExcel> RelicConfigData { get; private set; } = [];
    public static Dictionary<int, RelicExpItemExcel> RelicExpItemData { get; private set; } = [];
    public static Dictionary<int, RelicExpTypeExcel> RelicExpTypeData { get; private set; } = [];
    public static Dictionary<int, PetExcel> PetData { get; private set; } = [];

    #endregion

    #region Special Avatar

    public static Dictionary<int, SpecialAvatarExcel> SpecialAvatarData { get; private set; } = [];
    public static Dictionary<int, SpecialAvatarRelicExcel> SpecialAvatarRelicData { get; private set; } = [];

    #endregion

    #region Mission
    // 在 GameData.cs 的 #region Mission 部分添加
    public static Dictionary<int, DailyQuestConfigExcel> DailyQuestConfigData { get; private set; } = [];
    public static Dictionary<int, MainMissionExcel> MainMissionData { get; private set; } = [];
    public static Dictionary<int, SubMissionExcel> SubMissionData { get; private set; } = [];
    public static ConcurrentDictionary<int, SubMissionData> SubMissionInfoData { get; private set; } = [];
    public static Dictionary<int, RewardDataExcel> RewardDataData { get; private set; } = [];
    public static Dictionary<int, MessageGroupConfigExcel> MessageGroupConfigData { get; private set; } = [];
    public static Dictionary<int, MessageSectionConfigExcel> MessageSectionConfigData { get; private set; } = [];
    public static Dictionary<int, MessageContactsConfigExcel> MessageContactsConfigData { get; private set; } = [];
    public static Dictionary<int, MessageItemConfigExcel> MessageItemConfigData { get; private set; } = [];
    public static Dictionary<int, PerformanceDExcel> PerformanceDData { get; private set; } = [];
    public static Dictionary<int, PerformanceEExcel> PerformanceEData { get; private set; } = [];
    public static Dictionary<int, StoryLineExcel> StoryLineData { get; private set; } = [];

    public static Dictionary<int, Dictionary<int, StoryLineFloorDataExcel>>
        StoryLineFloorDataData { get; private set; } = [];

    public static Dictionary<int, StroyLineTrialAvatarDataExcel> StroyLineTrialAvatarDataData { get; private set; } =
        [];

    public static Dictionary<int, HeartDialScriptExcel> HeartDialScriptData { get; private set; } = [];
    public static Dictionary<int, HeartDialDialogueExcel> HeartDialDialogueData { get; private set; } = [];

    #endregion

    #region Item Exchange

    public static Dictionary<int, ShopConfigExcel> ShopConfigData { get; private set; } = [];
    public static Dictionary<int, RollShopConfigExcel> RollShopConfigData { get; private set; } = [];
    public static Dictionary<int, RollShopRewardExcel> RollShopRewardData { get; private set; } = [];
    public static Dictionary<int, ItemComposeConfigExcel> ItemComposeConfigData { get; private set; } = [];

    #endregion
    #region MatchThree

    public static Dictionary<int, MatchThreeLevelExcel> MatchThreeLevelData { get; private set; } = [];
    public static Dictionary<int, MatchThreeBirdExcel> MatchThreeBirdData { get; private set; } = [];

    #endregion

    #region Tutorial

    public static Dictionary<int, TutorialDataExcel> TutorialDataData { get; private set; } = [];
    public static Dictionary<int, TutorialGuideDataExcel> TutorialGuideDataData { get; private set; } = [];

    #endregion

    #region Actions

    public static void GetFloorInfo(int planeId, int floorId, out FloorInfo outer)
    {
        FloorInfoData.TryGetValue("P" + planeId + "_F" + floorId, out outer!);
    }

    public static FloorInfo? GetFloorInfo(int floorId)
    {
        var entrance = MapEntranceData.FirstOrDefault(x => x.Value.FloorID == floorId);
        if (entrance.Value == null) return null;

        GetFloorInfo(entrance.Value.PlaneID, floorId, out var floorInfo);
        return floorInfo;
    }

    public static int GetPlayerExpRequired(int level)
    {
        var excel = PlayerLevelConfigData[level];
        var prevExcel = PlayerLevelConfigData[level - 1];
        return excel != null && prevExcel != null ? excel.PlayerExp - prevExcel.PlayerExp : 0;
    }

    public static int GetAvatarExpRequired(int group, int level)
    {
        ExpTypeData.TryGetValue(group * 100 + level, out var expType);
        return expType?.Exp ?? 0;
    }

    public static int GetEquipmentExpRequired(int group, int level)
    {
        EquipmentExpTypeData.TryGetValue(group * 100 + level, out var expType);
        return expType?.Exp ?? 0;
    }

    public static int GetMinPromotionForLevel(int level)
    {
        return Math.Max(Math.Min((int)((level - 11) / 10D), 6), 0);
    }

    #endregion
}
