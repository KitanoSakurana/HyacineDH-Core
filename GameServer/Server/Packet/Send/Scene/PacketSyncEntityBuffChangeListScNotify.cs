using HyacineCore.Server.GameServer.Game.Scene;
using HyacineCore.Server.GameServer.Game.Scene.Entity;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Scene;

public class PacketSyncEntityBuffChangeListScNotify : BasePacket
{
    public PacketSyncEntityBuffChangeListScNotify(BaseGameEntity entity, SceneBuff buff) : base(
        CmdIds.SyncEntityBuffChangeListScNotify)
    {
        var proto = new SyncEntityBuffChangeListScNotify();
        var change = new EntityBuffChangeInfo
        {
            EntityId = (uint)entity.EntityId,
            BuffChangeInfo = buff.ToProto()
        };
        proto.EntityBuffChangeList.Add(change);

        SetData(proto);
    }

    public PacketSyncEntityBuffChangeListScNotify(BaseGameEntity entity, List<SceneBuff> buffs) : base(
        CmdIds.SyncEntityBuffChangeListScNotify)
    {
        var proto = new SyncEntityBuffChangeListScNotify();

        foreach (var buff in buffs)
        {
            var change = new EntityBuffChangeInfo
            {
                EntityId = (uint)entity.EntityId,
                RemoveBuffId = (uint)buff.BuffId
            };
            proto.EntityBuffChangeList.Add(change);
        }

        SetData(proto);
    }
}