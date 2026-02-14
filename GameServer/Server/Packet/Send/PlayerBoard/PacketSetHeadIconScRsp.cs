using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.PlayerBoard;

public class PacketSetHeadIconScRsp : BasePacket
{
    public PacketSetHeadIconScRsp(PlayerInstance player) : base(CmdIds.SetHeadIconScRsp)
    {
        var proto = new SetHeadIconScRsp
        {
            CurrentHeadIconId = (uint)player.Data.HeadIcon
        };
        SetData(proto);
    }
}