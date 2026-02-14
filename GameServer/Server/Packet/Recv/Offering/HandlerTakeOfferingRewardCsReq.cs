using HyacineCore.Server.GameServer.Server.Packet.Send.Offering;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Offering;

[Opcode(CmdIds.TakeOfferingRewardCsReq)]
public class HandlerTakeOfferingRewardCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = TakeOfferingRewardCsReq.Parser.ParseFrom(data);
        var res = await connection.Player!.OfferingManager!.TakeOfferingReward((int)req.OfferingId,
            req.TakeRewardLevelList.Select(x => (int)x).ToList());

        await connection.SendPacket(new PacketTakeOfferingRewardScRsp(res.Item1, res.data, res.reward));
    }
}