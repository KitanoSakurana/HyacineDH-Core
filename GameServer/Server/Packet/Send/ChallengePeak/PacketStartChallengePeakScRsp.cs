using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.ChallengePeak;

public class PacketStartChallengePeakScRsp : BasePacket
{
    public PacketStartChallengePeakScRsp(Retcode retcode) : base(CmdIds.StartChallengePeakScRsp)
    {
        var proto = new StartChallengePeakScRsp
        {
            Retcode = (uint)retcode
        };

        SetData(proto);
    }
}