using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Scene;

public class PacketSetGroupCustomSaveDataScRsp : BasePacket
{
    public PacketSetGroupCustomSaveDataScRsp(uint entryId, uint groupId) : base(CmdIds.SetGroupCustomSaveDataScRsp)
    {
        var proto = new SetGroupCustomSaveDataScRsp
        {
            EntryId = entryId,
            GroupId = groupId
        };
        SetData(proto);
    }
}