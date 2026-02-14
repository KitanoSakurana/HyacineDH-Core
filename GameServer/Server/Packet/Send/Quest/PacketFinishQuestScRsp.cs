using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Quest;

public class PacketFinishQuestScRsp : BasePacket
{
    public PacketFinishQuestScRsp(Retcode retCode) : base(CmdIds.FinishQuestScRsp)
    {
        var proto = new FinishQuestScRsp
        {
            Retcode = (uint)retCode
        };

        SetData(proto);
    }
}