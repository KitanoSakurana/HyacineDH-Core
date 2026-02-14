using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Scene;

public class PacketOpenChestScNotify : BasePacket
{
    public PacketOpenChestScNotify(int chestId) : base(CmdIds.OpenChestScNotify)
    {
        var proto = new OpenChestScNotify
        {
            ChestId = (uint)chestId
        };

        SetData(proto);
    }
}