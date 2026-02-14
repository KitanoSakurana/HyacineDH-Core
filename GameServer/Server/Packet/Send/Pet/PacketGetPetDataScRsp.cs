using HyacineCore.Server.Data;
using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Pet;

public class PacketGetPetDataScRsp : BasePacket
{
    public PacketGetPetDataScRsp(PlayerInstance player) : base(CmdIds.GetPetDataScRsp)
    {
        var proto = new GetPetDataScRsp
        {
            CurPetId = (uint)player.Data.Pet
        };

        foreach (var pet in GameData.PetData.Values) proto.UnlockedPetId.Add((uint)pet.PetID);

        SetData(proto);
    }
}