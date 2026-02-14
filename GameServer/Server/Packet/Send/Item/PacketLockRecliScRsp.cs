using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Item;

public class PacketLockRelicScRsp : BasePacket
{
    public PacketLockRelicScRsp(bool success) : base(CmdIds.LockRelicScRsp)
    {
        LockRelicScRsp proto = new();

        if (!success) proto.Retcode = (uint)Retcode.RetFail;

        SetData(proto);
    }
}