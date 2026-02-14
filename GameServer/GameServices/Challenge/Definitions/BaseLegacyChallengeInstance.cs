using HyacineCore.Server.Data.Excel;
using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Proto;
using HyacineCore.Server.Proto.ServerSide;
using HyacineCore.Server.Util;

namespace HyacineCore.Server.GameServer.Game.Challenge.Definitions;

public abstract class BaseLegacyChallengeInstance(PlayerInstance player, ChallengeDataPb data)
    : BaseChallengeInstance(player, data)
{
    public bool IsWin { get; set; }
    public bool IsPartialChallenge { get; set; }
    public abstract ChallengeConfigExcel Config { get; }
    public abstract CurChallenge ToProto();

    public abstract uint GetStars();
    public abstract int GetCurrentExtraLineupType();
    public abstract void SetStartPos(Position pos);
    public abstract void SetStartRot(Position rot);
    public abstract void SetSavedMp(int mp);

    public virtual uint GetScore1()
    {
        return 0;
    }

    public virtual uint GetScore2()
    {
        return 0;
    }

    public virtual ChallengeStageInfo ToStageInfo()
    {
        return new ChallengeStageInfo();
    }
}
