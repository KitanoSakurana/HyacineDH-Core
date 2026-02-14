using HyacineCore.Server.GameServer.Server.Packet.Send.Gacha;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Gacha;

[Opcode(CmdIds.DoGachaCsReq)]
public class HandlerDoGachaCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = DoGachaCsReq.Parser.ParseFrom(data);
        var gain = await connection.Player!.GachaManager!.DoGacha((int)req.GachaId, (int)req.GachaNum);

        await connection.SendPacket(gain != null ? new PacketDoGachaScRsp(gain) : new PacketDoGachaScRsp());
    }
}
