using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Item;

public class PacketLockEquipmentScRsp : BasePacket
{
    public PacketLockEquipmentScRsp(bool success) : base(CmdIds.LockEquipmentScRsp)
    {
        LockEquipmentScRsp proto = new();

        if (!success) proto.Retcode = (uint)Retcode.RetFail;

        SetData(proto);
    }
}