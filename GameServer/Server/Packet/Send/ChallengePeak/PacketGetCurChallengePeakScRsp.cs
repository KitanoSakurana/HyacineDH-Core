using HyacineCore.Server.GameServer.Game.Challenge.Instances;
using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.ChallengePeak;

public class PacketGetCurChallengePeakScRsp : BasePacket
{
    public PacketGetCurChallengePeakScRsp(PlayerInstance player) : base(CmdIds.GetCurChallengePeakScRsp)
    {
        var proto = new GetCurChallengePeakScRsp();

        if (player.ChallengeManager!.ChallengeInstance is ChallengePeakInstance peak)
        {
            proto.PeakId = peak.Data.Peak.CurrentPeakLevelId;
            proto.BossBuffId = peak.Data.Peak.Buffs.FirstOrDefault(0u);
            proto.CyclesUsed = peak.Data.Peak.RoundCnt;
            proto.HasPassed = peak.IsWin;
            proto.IsWaitConfirm = false;
        }

        SetData(proto);
    }
}
