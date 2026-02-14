using HyacineCore.Server.Data;
using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.PamSkin;

public class PacketGetPamSkinDataScRsp : BasePacket
{
    public PacketGetPamSkinDataScRsp(PlayerInstance player) : base(CmdIds.GetPamSkinDataScRsp)
    {
        var proto = new GetPamSkinDataScRsp
        {
            CurSkin = (uint)player.Data.CurrentPamSkin,
            UnlockSkinList = { GameData.PamSkinConfigData.Select(x => (uint)x.Key) }
        };

        SetData(proto);
    }
}