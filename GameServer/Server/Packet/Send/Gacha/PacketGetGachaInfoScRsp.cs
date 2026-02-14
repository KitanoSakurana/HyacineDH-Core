using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Gacha;

public class PacketGetGachaInfoScRsp : BasePacket
{
    public PacketGetGachaInfoScRsp(PlayerInstance player) : base(CmdIds.GetGachaInfoScRsp)
    {
        SetData(player.GachaManager!.ToProto());
    }
}