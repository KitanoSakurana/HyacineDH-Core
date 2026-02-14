using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.PamSkin;

public class PacketSelectPamSkinScRsp : BasePacket
{
    public PacketSelectPamSkinScRsp(PlayerInstance player, int prevSkinId) : base(CmdIds.SelectPamSkinScRsp)
    {
        var proto = new SelectPamSkinScRsp
        {
            CurSkin = (uint)player.Data.CurrentPamSkin,
            SetSkin = (uint)prevSkinId
        };

        SetData(proto);
    }
}