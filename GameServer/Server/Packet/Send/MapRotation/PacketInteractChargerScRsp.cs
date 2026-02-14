using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.MapRotation;

public class PacketInteractChargerScRsp : BasePacket
{
    public PacketInteractChargerScRsp(ChargerInfo chargerInfo) : base(CmdIds.InteractChargerScRsp)
    {
        var proto = new InteractChargerScRsp
        {
            ChargerInfo = chargerInfo
        };

        SetData(proto);
    }
}