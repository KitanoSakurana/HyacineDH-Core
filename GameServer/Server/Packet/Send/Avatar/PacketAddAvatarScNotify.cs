using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Avatar;

public class PacketAddAvatarScNotify : BasePacket
{
    public PacketAddAvatarScNotify(int avatarId, bool isGacha) : base(CmdIds.AddAvatarScNotify)
    {
        var packet = new AddAvatarScNotify
        {
            BaseAvatarId = (uint)avatarId,
            IsNew = true,
            Src = isGacha ? AddAvatarSrcState.AddAvatarSrcGacha : AddAvatarSrcState.AddAvatarSrcNone
        };

        SetData(packet);
    }
}