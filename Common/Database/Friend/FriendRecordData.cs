using HyacineCore.Server.Proto;
using HyacineCore.Server.Util;
using SqlSugar;

namespace HyacineCore.Server.Database.Friend;

[SugarTable("friend_record_data")]
public class FriendRecordData : BaseDatabaseDataHelper
{
    [SugarColumn(IsJson = true)]
    public List<FriendDevelopmentInfoPb> DevelopmentInfos { get; set; } = []; // max 20 entries

    [SugarColumn(IsJson = true)]
    public Dictionary<uint, ChallengeGroupStatisticsPb> ChallengeGroupStatistics { get; set; } =
        []; // cur group statistics

    public uint NextRecordId { get; set; }

    public void AddAndRemoveOld(FriendDevelopmentInfoPb info)
    {
        // get same type
        var same = DevelopmentInfos.Where(x => x.DevelopmentType == info.DevelopmentType);

        // if param equal remove
        foreach (var infoPb in same.ToArray())
            // ReSharper disable once UsageOfDefaultStructEquality
            if (infoPb.Params.SequenceEqual(info.Params))
                // remove
                DevelopmentInfos.Remove(infoPb);

        DevelopmentInfos.Add(info);
    }
}

public class FriendDevelopmentInfoPb
{
    public DevelopmentType DevelopmentType { get; set; }
    public long Time { get; set; } = Extensions.GetUnixSec();
    public Dictionary<string, uint> Params { get; set; } = [];

    public JJPBEKAPFCF ToProto()
    {
        var proto = new JJPBEKAPFCF
        {
            Time = Time,
            OKJNGOKPAIJ = DevelopmentType
        };

        switch (DevelopmentType)
        {
            case DevelopmentType.LhjmkmeiklkPmfjffapajo: // DevelopmentNone
            case DevelopmentType.LhjmkmeiklkOanphkgagoa: // DevelopmentActivityStart
            case DevelopmentType.LhjmkmeiklkGhjonnnkdoh: // DevelopmentActivityEnd
            case DevelopmentType.LhjmkmeiklkNbbojikjnhd: // DevelopmentRogueMagic
                break;
            case DevelopmentType.LhjmkmeiklkOglnogiknci: // DevelopmentRogueCosmos
            case DevelopmentType.LhjmkmeiklkCkhmlokakoh: // DevelopmentRogueChessNous
            case DevelopmentType.LhjmkmeiklkOhlmfhpeagj: // DevelopmentRogueChess
                proto.EGGAKPPLBHE = new FGLAPFKJIPO
                {
                    AreaId = Params.GetValueOrDefault("AreaId", 0u)
                };
                break;
            case DevelopmentType.LhjmkmeiklkDbfjdbiefdb: // DevelopmentMemoryChallenge
            case DevelopmentType.LhjmkmeiklkMnkocfkkmbe: // DevelopmentStoryChallenge
            case DevelopmentType.LhjmkmeiklkGmopdopmgfn: // DevelopmentBossChallenge
                proto.OGCJCPPNHFP = new DMBACEALDKB
                {
                    ChallengeId = Params.GetValueOrDefault("ChallengeId", 0u)
                };
                break;
            case DevelopmentType.LhjmkmeiklkCooihblilbb: // DevelopmentUnlockAvatar
                proto.AvatarId = Params.GetValueOrDefault("AvatarId", 0u);
                break;
            case DevelopmentType.LhjmkmeiklkDfjcoioache: // DevelopmentUnlockEquipment
                proto.DCFLOABJONG = Params.GetValueOrDefault("EquipmentTid", 0u);
                break;
            case DevelopmentType.LhjmkmeiklkPcdcmpfnmjh: // DevelopmentRogueTourn
            case DevelopmentType.LhjmkmeiklkKbokednfkhh: // DevelopmentRogueTournWeek
            case DevelopmentType.LhjmkmeiklkHbdolkcaild: // DevelopmentRogueTournDivision
                proto.MKADMLIACDF = new IIBGDBBPDJI
                {
                    AreaId = Params.GetValueOrDefault("AreaId", 0u),
                    DBCGCKKHKGG = Params.GetValueOrDefault("FinishTournDifficulty", 0u)
                };
                break;
            case DevelopmentType.LhjmkmeiklkAogdffkokmc: // DevelopmentChallengePeak
                proto.JMJOCLIJOAH = new OLLHKKBBAJI
                {
                    PeakId = Params.GetValueOrDefault("PeakLevelId", 0u)
                };
                break;
        }

        return proto;
    }
}

public class ChallengeGroupStatisticsPb
{
    public uint GroupId { get; set; }
    public Dictionary<uint, MemoryGroupStatisticsPb>? MemoryGroupStatistics { get; set; }
    public Dictionary<uint, StoryGroupStatisticsPb>? StoryGroupStatistics { get; set; }
    public Dictionary<uint, BossGroupStatisticsPb>? BossGroupStatistics { get; set; }

    public GetChallengeGroupStatisticsScRsp ToProto()
    {
        var proto = new GetChallengeGroupStatisticsScRsp { GroupId = GroupId };

        var maxBoss = BossGroupStatistics?.Values.MaxBy(x => x.Level);
        if (maxBoss != null) proto.ChallengeBoss = maxBoss.ToProto();

        var maxStory = StoryGroupStatistics?.Values.MaxBy(x => x.Level);
        if (maxStory != null) proto.ChallengeStory = maxStory.ToProto();

        var maxMemory = MemoryGroupStatistics?.Values.MaxBy(x => x.Level);
        if (maxMemory != null) proto.ChallengeDefault = maxMemory.ToProto();

        return proto;
    }
}

public class MemoryGroupStatisticsPb
{
    public uint RecordId { get; set; }
    public uint Level { get; set; }
    public uint RoundCount { get; set; }
    public uint Stars { get; set; }
    public List<List<ChallengeAvatarInfoPb>> Lineups { get; set; } = [];

    public ChallengeStatistics ToProto()
    {
        return new ChallengeStatistics
        {
            RecordId = RecordId,
            StageTertinggi = new ChallengeStageTertinggi
            {
                LDEKMAADNKK = Stars,
                Level = Level,
                RoundCount = RoundCount,
                LineupList =
                {
                    Lineups.Select(x => new ChallengeLineupList
                    {
                        AvatarList = { x.Select(avatar => avatar.ToProto()) }
                    })
                }
            }
        };
    }
}

public class StoryGroupStatisticsPb
{
    public uint RecordId { get; set; }
    public uint Level { get; set; }
    public uint Score { get; set; }
    public uint BuffOne { get; set; }
    public uint BuffTwo { get; set; }
    public uint Stars { get; set; }
    public List<List<ChallengeAvatarInfoPb>> Lineups { get; set; } = [];

    public ChallengeStoryStatistics ToProto()
    {
        return new ChallengeStoryStatistics
        {
            RecordId = RecordId,
            StageTertinggi = new ChallengeStoryStageTertinggi
            {
                LDEKMAADNKK = Stars,
                Level = Level,
                LineupList =
                {
                    Lineups.Select(x => new ChallengeLineupList
                    {
                        AvatarList = { x.Select(avatar => avatar.ToProto()) }
                    })
                },
                BuffOne = BuffOne,
                BuffTwo = BuffTwo,
                ScoreId = Score
            }
        };
    }
}

public class BossGroupStatisticsPb
{
    public uint RecordId { get; set; }
    public uint Level { get; set; }
    public uint Score { get; set; }
    public uint BuffOne { get; set; }
    public uint BuffTwo { get; set; }
    public uint Stars { get; set; }
    public List<List<ChallengeAvatarInfoPb>> Lineups { get; set; } = [];

    public ChallengeBossStatistics ToProto()
    {
        return new ChallengeBossStatistics
        {
            RecordId = RecordId,
            StageTertinggi = new ChallengeBossStageTertinggi
            {
                LDEKMAADNKK = Stars,
                Level = Level,
                LineupList =
                {
                    Lineups.Select(x => new ChallengeLineupList
                    {
                        AvatarList = { x.Select(avatar => avatar.ToProto()) }
                    })
                },
                BuffOne = BuffOne,
                BuffTwo = BuffTwo,
                ScoreId = Score
            }
        };
    }
}

public class ChallengeAvatarInfoPb
{
    public uint Level { get; set; }
    public uint Index { get; set; }
    public uint Id { get; set; }
    public AvatarType AvatarType { get; set; } = AvatarType.AvatarFormalType;
    public uint Rank { get; set;} // <--- 添加这一行

    public ChallengeAvatarInfo ToProto()
    {
        return new ChallengeAvatarInfo
        {
            Level = Level,
            AvatarType = AvatarType,
            Id = Id,
            Index = Index,
            JNBNNCJKHNG = Rank // 对应星魂
        };
    }
}
