using HyacineCore.Server.GameServer.Server.Packet.Send.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Player;

[Opcode(CmdIds.ReserveStaminaExchangeCsReq)]
public class HandlerReserveStaminaExchangeCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = ReserveStaminaExchangeCsReq.Parser.ParseFrom(data);
        var player = connection.Player;
        if (player == null) return;
        var amount = req.Num;
        if (amount <= 0 || player.Data.StaminaReserve < amount)
        {
            await connection.SendPacket(new PacketReserveStaminaExchangeScRsp(0));
        }
        else
        {
            player.Data.StaminaReserve -= amount;
            player.Data.Stamina += (int)amount;

            await connection.SendPacket(new PacketStaminaInfoScNotify(player));
            await connection.SendPacket(new PacketReserveStaminaExchangeScRsp(amount));
        }
    }
}