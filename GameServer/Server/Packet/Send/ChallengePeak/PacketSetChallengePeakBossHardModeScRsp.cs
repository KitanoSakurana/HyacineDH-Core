using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.ChallengePeak;

public class PacketSetChallengePeakBossHardModeScRsp : BasePacket
{
    public PacketSetChallengePeakBossHardModeScRsp(uint groupId, bool isHard) : base(
        CmdIds.SetChallengePeakBossHardModeScRsp)
    {
        var proto = new SetChallengePeakBossHardModeScRsp
        {
            Retcode = 0,
            IsHardMode = isHard,
            PeakGroupId = groupId
        };

        SetData(proto);
    }
}
