using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Phone;

public class PacketSetPersonalCardScRsp : BasePacket
{
    public PacketSetPersonalCardScRsp(uint id) : base(CmdIds.SetPersonalCardScRsp)
    {
        var proto = new SetPersonalCardScRsp
        {
            CurrentPersonalCardId = id
        };

        SetData(proto);
    }
}
