using HyacineCore.Server.Data;
using HyacineCore.Server.GameServer.Server.Packet.Send.PamSkin;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.PamSkin;

[Opcode(CmdIds.SelectPamSkinCsReq)]
public class HandlerSelectPamSkinCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SelectPamSkinCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;

        // Check if the skin is valid
        if (GameData.PamSkinConfigData.ContainsKey((int)req.PamSkin)) player.Data.CurrentPamSkin = (int)req.PamSkin;
        var prevSkinId = player.Data.CurrentPamSkin;

        await connection.SendPacket(new PacketSelectPamSkinScRsp(player, prevSkinId));
    }
}