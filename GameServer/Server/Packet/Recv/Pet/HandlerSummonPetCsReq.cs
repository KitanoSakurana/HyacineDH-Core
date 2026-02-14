using HyacineCore.Server.GameServer.Server.Packet.Send.Pet;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Pet;

[Opcode(CmdIds.SummonPetCsReq)]
public class HandlerSummonPetCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SummonPetCsReq.Parser.ParseFrom(data);

        var curPetId = connection.Player!.Data.Pet;
        if (curPetId != req.SummonedPetId && curPetId != 0)
            await connection.SendPacket(new PacketCurPetChangedScNotify(req.SummonedPetId));

        connection.Player!.Data.Pet = (int)req.SummonedPetId;

        await connection.SendPacket(new PacketSummonPetScRsp(curPetId, req.SummonedPetId));
    }
}