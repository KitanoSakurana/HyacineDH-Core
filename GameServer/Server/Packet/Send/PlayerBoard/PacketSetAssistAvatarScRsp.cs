using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;
using HyacineCore.Server.GameServer.Game.Player;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.PlayerBoard;

public class PacketSetAssistAvatarScRsp : BasePacket
{
    public PacketSetAssistAvatarScRsp(PlayerInstance player) : base(CmdIds.SetAssistAvatarScRsp)
    {
        var proto = new SetAssistAvatarScRsp();

        foreach (var baseAvatarId in player.AvatarManager?.AvatarData?.AssistAvatars ?? [])
        {
            var avatar = player.AvatarManager?.GetFormalAvatar(baseAvatarId);
            if (avatar == null) continue;

            proto.AvatarIdList.Add((uint)avatar.BaseAvatarId);
        }

        SetData(proto);
    }
}
