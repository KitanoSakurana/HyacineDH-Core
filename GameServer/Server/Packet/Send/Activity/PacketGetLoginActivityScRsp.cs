using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Activity;

public class PacketGetLoginActivityScRsp : BasePacket
{
    // 构造函数，传入 Proto 数据，并指定对应的 CmdId
    public PacketGetLoginActivityScRsp(GetLoginActivityScRsp proto) 
        : base((ushort)CmdIds.GetLoginActivityScRsp) 
    {
        this.SetData(proto);
    }
}
