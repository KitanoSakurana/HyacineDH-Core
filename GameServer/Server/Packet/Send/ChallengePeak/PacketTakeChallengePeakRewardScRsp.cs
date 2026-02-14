using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.ChallengePeak;

public class PacketTakeChallengePeakRewardScRsp : BasePacket
{
    public PacketTakeChallengePeakRewardScRsp(uint peakGroupId, Retcode retcode = Retcode.RetSucc)
        : base(CmdIds.TakeChallengePeakRewardScRsp)
    {
        var proto = new TakeChallengePeakRewardScRsp
        {
            Retcode = (uint)retcode,
            PeakGroupId = peakGroupId
        };

        SetData(proto);
    }
}

