using HyacineCore.Server.GameServer.Game.Scene.Entity;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Scene;

public class PacketInteractPropScRsp : BasePacket
{
    public PacketInteractPropScRsp(EntityProp? prop) : base(CmdIds.InteractPropScRsp)
    {
        var proto = new InteractPropScRsp();

        if (prop != null)
        {
            proto.PropState = (uint)prop.State;
            proto.PropEntityId = (uint)prop.EntityId;
        }

        SetData(proto);
    }
}