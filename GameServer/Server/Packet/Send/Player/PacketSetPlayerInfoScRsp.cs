using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Player;

public class PacketSetPlayerInfoScRsp : BasePacket
{
    public PacketSetPlayerInfoScRsp(PlayerInstance player, bool IsModify) : base(CmdIds.SetPlayerInfoScRsp)
    {
        var proto = new SetPlayerInfoScRsp
        {
            CurAvatarPath = (MultiPathAvatarType)player.Data.CurBasicType,
            IsModify = IsModify
        };

        SetData(proto);
    }
}