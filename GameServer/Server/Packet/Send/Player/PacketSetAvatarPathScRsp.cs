using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Player;

public class PacketSetAvatarPathScRsp : BasePacket
{
    public PacketSetAvatarPathScRsp(int avatarId) : base(CmdIds.SetAvatarPathScRsp)
    {
        var proto = new SetAvatarPathScRsp
        {
            AvatarId = (MultiPathAvatarType)avatarId
        };

        SetData(proto);
    }
}