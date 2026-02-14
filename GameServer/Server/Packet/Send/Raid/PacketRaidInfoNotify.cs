using HyacineCore.Server.Database.Scene;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Raid;

public class PacketRaidInfoNotify : BasePacket
{
    public PacketRaidInfoNotify(RaidRecord record) : base(CmdIds.RaidInfoNotify)
    {
        var proto = new RaidInfoNotify
        {
            RaidId = (uint)record.RaidId,
            Status = record.Status,
            WorldLevel = (uint)record.WorldLevel,
            RaidFinishTime = (ulong)record.FinishTimeStamp,
            ItemList = new ItemList()
        };

        SetData(proto);
    }

    public PacketRaidInfoNotify() : base(CmdIds.RaidInfoNotify)
    {
        var proto = new RaidInfoNotify();

        SetData(proto);
    }
}