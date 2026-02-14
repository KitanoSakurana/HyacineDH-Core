using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Player;

public class PacketAvatarPathChangedNotify : BasePacket
{
    public PacketAvatarPathChangedNotify(uint baseAvatarId, MultiPathAvatarType type) : base(
        CmdIds.AvatarPathChangedNotify)
    {
        var proto = new AvatarPathChangedNotify
        {
            BaseAvatarId = baseAvatarId,
            CurMultiPathAvatarType = type
        };

        SetData(proto);
    }
}