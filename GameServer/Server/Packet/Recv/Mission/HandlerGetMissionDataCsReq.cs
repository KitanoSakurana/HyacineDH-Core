using HyacineCore.Server.GameServer.Server.Packet.Send.Mission;
using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Mission;

[Opcode(CmdIds.GetMissionDataCsReq)]
public class HandlerGetMissionDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetMissionDataScRsp(connection.Player!));
    }
}