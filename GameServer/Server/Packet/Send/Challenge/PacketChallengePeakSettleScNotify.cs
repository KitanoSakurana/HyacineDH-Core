using HyacineCore.Server.GameServer.Game.Challenge.Instances;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Challenge;

public class PacketChallengePeakSettleScNotify : BasePacket
{
    public PacketChallengePeakSettleScNotify(ChallengePeakInstance challenge, List<uint> finishedTargets) : base(
        CmdIds.ChallengePeakSettleScNotify)
    {
        var proto = new ChallengePeakSettleScNotify
        {
            IsWin = challenge.IsWin,
            PeakId = (uint)challenge.Config.ID,
            CyclesUsed = challenge.Data.Peak.RoundCnt,
            FinishedTargetList = { finishedTargets },
        };

        SetData(proto);
    }
}

