using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Pet;

public class PacketCurPetChangedScNotify : BasePacket
{
    public PacketCurPetChangedScNotify(uint newPetId) : base(CmdIds.CurPetChangedScNotify)
    {
        var proto = new CurPetChangedScNotify
        {
            CurPetId = newPetId
        };

        SetData(proto);
    }
}