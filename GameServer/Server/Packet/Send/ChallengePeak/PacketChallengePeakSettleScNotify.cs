using HyacineCore.Server.GameServer.Game.Challenge.Instances;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.ChallengePeak;

public class PacketChallengePeakSettleScNotify : BasePacket
{
    public PacketChallengePeakSettleScNotify(ChallengePeakInstance inst, List<uint> targetIdList) : base(
        CmdIds.ChallengePeakSettleScNotify)
    {
        var proto = new ChallengePeakSettleScNotify
        {
            IsWin = inst.IsWin,
            CyclesUsed = inst.Data.Peak.RoundCnt,
            PeakId = inst.Data.Peak.CurrentPeakLevelId,
            FinishedTargetList = { targetIdList },
            HardModeHasPassed = inst is { IsWin: true, Config.BossExcel: not null } && inst.Data.Peak.IsHard,
            IsWaitConfirm = false,
            TurnLeft = 0
        };

        SetData(proto);
    }
}
