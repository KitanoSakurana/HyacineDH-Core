using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Message;

public class PacketFinishSectionIdScRsp : BasePacket
{
    public PacketFinishSectionIdScRsp(uint sectionId) : base(CmdIds.FinishSectionIdScRsp)
    {
        var proto = new FinishSectionIdScRsp
        {
            SectionId = sectionId
        };

        SetData(proto);
    }
}