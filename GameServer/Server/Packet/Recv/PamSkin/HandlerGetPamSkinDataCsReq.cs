using HyacineCore.Server.GameServer.Server.Packet.Send.PamSkin;
using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.PamSkin;

[Opcode(CmdIds.GetPamSkinDataCsReq)]
public class HandlerGetPamSkinDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetPamSkinDataScRsp(connection.Player!));
    }
}