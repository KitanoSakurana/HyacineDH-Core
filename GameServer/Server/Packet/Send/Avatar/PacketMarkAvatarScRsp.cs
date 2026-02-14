using HyacineCore.Server.Database.Avatar;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Avatar;

public class PacketMarkAvatarScRsp : BasePacket
{
    public PacketMarkAvatarScRsp(FormalAvatarInfo avatar) : base(CmdIds.MarkAvatarScRsp)
    {
        var proto = new MarkAvatarScRsp
        {
            AvatarId = (uint)avatar.AvatarId,
            IsMarked = avatar.IsMarked
        };

        SetData(proto);
    }
}