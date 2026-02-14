using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Pet;

public class PacketSummonPetScRsp : BasePacket
{
    public PacketSummonPetScRsp(int curPetId, uint newPetId) : base(CmdIds.SummonPetScRsp)
    {
        var proto = new SummonPetScRsp
        {
            CurPetId = (uint)curPetId,
            SelectPetId = newPetId
        };

        SetData(proto);
    }
}