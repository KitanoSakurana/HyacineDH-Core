using HyacineCore.Server.GameServer.Server.Packet.Send.Challenge;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Challenge;

[Opcode(CmdIds.GetChallengeGroupStatisticsCsReq)]
public class HandlerGetChallengeGroupStatisticsCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GetChallengeGroupStatisticsCsReq.Parser.ParseFrom(data);

        await connection.SendPacket(new PacketGetChallengeGroupStatisticsScRsp(req.GroupId,
            connection.Player!.FriendRecordData!.ChallengeGroupStatistics.Values.FirstOrDefault(x =>
                x.GroupId == req.GroupId)));
    }
}