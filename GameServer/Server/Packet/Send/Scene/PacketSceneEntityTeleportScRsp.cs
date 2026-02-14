using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Scene;

public class PacketSceneEntityTeleportScRsp : BasePacket
{
    public PacketSceneEntityTeleportScRsp(EntityMotion motion) : base(CmdIds.SceneEntityTeleportScRsp)
    {
        var proto = new SceneEntityTeleportScRsp
        {
            EntityMotion = motion
        };

        SetData(proto);
    }
}