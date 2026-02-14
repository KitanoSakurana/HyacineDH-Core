using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Friend;

public class PacketGetPlayerDetailInfoScRsp : BasePacket
{
    public PacketGetPlayerDetailInfoScRsp(PlayerDetailInfo info) : base(CmdIds.GetPlayerDetailInfoScRsp)
    {
        var proto = new GetPlayerDetailInfoScRsp
        {
            DetailInfo = info
        };

        SetData(proto);
    }

    public PacketGetPlayerDetailInfoScRsp() : base(CmdIds.GetPlayerDetailInfoScRsp)
    {
        var proto = new GetPlayerDetailInfoScRsp
        {
            Retcode = 3612
        };

        SetData(proto);
    }
}