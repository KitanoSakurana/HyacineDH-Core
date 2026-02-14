using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.ChallengePeak;

public class PacketReStartChallengePeakScRsp : BasePacket
{
    public PacketReStartChallengePeakScRsp(Retcode retcode = Retcode.RetSucc) : base(CmdIds.ReStartChallengePeakScRsp)
    {
        var proto = new ReStartChallengePeakScRsp
        {
            Retcode = (uint)retcode
        };

        SetData(proto);
    }
}

