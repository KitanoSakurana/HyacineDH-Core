using HyacineCore.Server.GameServer.Server.Packet.Send.Phone;
using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Phone;

[Opcode(CmdIds.GetPhoneDataCsReq)]
public class HandlerGetPhoneDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetPhoneDataScRsp(connection.Player!));
    }
}