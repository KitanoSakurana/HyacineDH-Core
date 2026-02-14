using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Player;

public class PacketReserveStaminaExchangeScRsp : BasePacket
{
    public PacketReserveStaminaExchangeScRsp(uint amount) : base(CmdIds.ReserveStaminaExchangeScRsp)
    {
        var proto = new ReserveStaminaExchangeScRsp();

        if (amount > 0) proto.Num = amount;
        else proto.Retcode = (uint)Retcode.RetFail;

        SetData(proto);
    }
}