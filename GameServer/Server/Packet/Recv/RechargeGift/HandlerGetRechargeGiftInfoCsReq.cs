using HyacineCore.Server.GameServer.Server.Packet.Send.RechargeGift;
using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.RechargeGift;

[Opcode(CmdIds.GetRechargeGiftInfoCsReq)]
public class HandlerGetRechargeGiftInfoCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetRechargeGiftInfoScRsp());
    }
}