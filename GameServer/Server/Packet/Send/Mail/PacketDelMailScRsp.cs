using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Mail;

public class PacketDelMailScRsp : BasePacket
{
    public PacketDelMailScRsp(List<uint> ids) : base(CmdIds.DelMailScRsp)
    {
        var proto = new DelMailScRsp
        {
            IdList = { ids }
        };

        SetData(proto);
    }
}