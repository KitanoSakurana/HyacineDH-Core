using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;
using HyacineCore.Server.Util;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Player;

public class PacketPlayerHeartBeatScRsp : BasePacket
{
    public PacketPlayerHeartBeatScRsp(long clientTime) : base(CmdIds.PlayerHeartBeatScRsp)
    {
        var data = new PlayerHeartBeatScRsp
        {
            ClientTimeMs = (ulong)clientTime,
            ServerTimeMs = (ulong)Extensions.GetUnixMs()
        };

        SetData(data);
    }
}