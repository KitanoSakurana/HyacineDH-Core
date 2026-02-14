using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Raid;

public class PacketDelSaveRaidScNotify : BasePacket
{
    public PacketDelSaveRaidScNotify(int raidId, int worldLevel) : base(CmdIds.DelSaveRaidScNotify)
    {
        var proto = new DelSaveRaidScNotify
        {
            RaidId = (uint)raidId,
            WorldLevel = (uint)worldLevel
        };

        SetData(proto);
    }
}