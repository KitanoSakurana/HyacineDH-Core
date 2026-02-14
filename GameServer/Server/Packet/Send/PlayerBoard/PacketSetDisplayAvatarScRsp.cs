using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;
using HyacineCore.Server.GameServer.Game.Player;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.PlayerBoard;

public class PacketSetDisplayAvatarScRsp : BasePacket
{
    public PacketSetDisplayAvatarScRsp(PlayerInstance player) : base(CmdIds.SetDisplayAvatarScRsp)
    {
        var proto = new SetDisplayAvatarScRsp();

        var pos = 0;
        foreach (var baseAvatarId in player.AvatarManager?.AvatarData?.DisplayAvatars ?? [])
        {
            var avatar = player.AvatarManager?.GetFormalAvatar(baseAvatarId);
            if (avatar == null) continue;

            proto.DisplayAvatarList.Add(new DisplayAvatarData
            {
                AvatarId = (uint)avatar.BaseAvatarId,
                Pos = (uint)pos++
            });
        }

        SetData(proto);
    }
}
