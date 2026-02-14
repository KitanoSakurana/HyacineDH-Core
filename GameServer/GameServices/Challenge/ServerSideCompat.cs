using System.Text.Json;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.Proto.ServerSide;

public enum ChallengeLineupTypePb
{
    None = 0,
    Challenge1 = 1,
    Challenge2 = 3,
    Challenge3 = 4
}

public sealed class ChallengeDataPb
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static ChallengeDataPbParser Parser { get; } = new();

    public enum ChallengeTypeOneofCase
    {
        None = 0,
        Memory = 1,
        Story = 2,
        Boss = 3,
        Peak = 4
    }

    public ChallengeMemoryDataPb Memory { get; set; } = new();
    public ChallengeStoryDataPb Story { get; set; } = new();
    public ChallengeBossDataPb Boss { get; set; } = new();
    public ChallengePeakDataPb Peak { get; set; } = new();

    public ChallengeTypeOneofCase ChallengeTypeCase
    {
        get
        {
            if (Peak.CurrentPeakLevelId > 0) return ChallengeTypeOneofCase.Peak;
            if (Boss.ChallengeMazeId > 0) return ChallengeTypeOneofCase.Boss;
            if (Story.ChallengeMazeId > 0) return ChallengeTypeOneofCase.Story;
            if (Memory.ChallengeMazeId > 0) return ChallengeTypeOneofCase.Memory;
            return ChallengeTypeOneofCase.None;
        }
    }

    public byte[] ToByteArray()
    {
        return JsonSerializer.SerializeToUtf8Bytes(this, JsonOptions);
    }

    public sealed class ChallengeDataPbParser
    {
        public ChallengeDataPb ParseFrom(byte[] data)
        {
            if (data.Length == 0) return new ChallengeDataPb();

            try
            {
                return JsonSerializer.Deserialize<ChallengeDataPb>(data, JsonOptions) ?? new ChallengeDataPb();
            }
            catch
            {
                // Compatibility fallback for legacy binary data.
                return new ChallengeDataPb();
            }
        }
    }
}

public sealed class ChallengeMemoryDataPb
{
    public uint ChallengeMazeId { get; set; }
    public int CurStatus { get; set; }
    public int CurrentStage { get; set; }
    public ChallengeLineupTypePb CurrentExtraLineup { get; set; } = ChallengeLineupTypePb.Challenge1;
    public uint RoundsLeft { get; set; }
    public uint DeadAvatarNum { get; set; }
    public uint Stars { get; set; }
    public Vector StartPos { get; set; } = new();
    public Vector StartRot { get; set; } = new();
    public uint SavedMp { get; set; }
}

public sealed class ChallengeStoryDataPb
{
    public uint ChallengeMazeId { get; set; }
    public int CurStatus { get; set; }
    public int CurrentStage { get; set; }
    public ChallengeLineupTypePb CurrentExtraLineup { get; set; } = ChallengeLineupTypePb.Challenge1;
    public List<uint> Buffs { get; set; } = [];
    public uint Stars { get; set; }
    public uint ScoreStage1 { get; set; }
    public uint ScoreStage2 { get; set; }
    public Vector StartPos { get; set; } = new();
    public Vector StartRot { get; set; } = new();
    public uint SavedMp { get; set; }
}

public sealed class ChallengeBossDataPb
{
    public uint ChallengeMazeId { get; set; }
    public int CurStatus { get; set; }
    public int CurrentStage { get; set; }
    public ChallengeLineupTypePb CurrentExtraLineup { get; set; } = ChallengeLineupTypePb.Challenge1;
    public List<uint> Buffs { get; set; } = [];
    public uint Stars { get; set; }
    public uint ScoreStage1 { get; set; }
    public uint ScoreStage2 { get; set; }
    public Vector StartPos { get; set; } = new();
    public Vector StartRot { get; set; } = new();
    public uint SavedMp { get; set; }
}

public sealed class ChallengePeakDataPb
{
    public uint CurrentPeakGroupId { get; set; }
    public uint CurrentPeakLevelId { get; set; }
    public ChallengeLineupTypePb CurrentExtraLineup { get; set; } = ChallengeLineupTypePb.Challenge1;
    public int CurStatus { get; set; }
    public bool IsHard { get; set; }
    public List<uint> Buffs { get; set; } = [];
    public uint Stars { get; set; }
    public uint RoundCnt { get; set; }
    public Vector? StartPos { get; set; }
    public Vector? StartRot { get; set; }
    public uint SavedMp { get; set; }
}
