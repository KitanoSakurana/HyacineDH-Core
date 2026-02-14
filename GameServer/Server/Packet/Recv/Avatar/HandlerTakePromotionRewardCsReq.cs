using HyacineCore.Server.GameServer.Server.Packet.Send.Avatar;
using HyacineCore.Server.GameServer.Server.Packet.Send.PlayerSync;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Avatar;

[Opcode(CmdIds.TakePromotionRewardCsReq)]
public class HandlerTakePromotionRewardCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = TakePromotionRewardCsReq.Parser.ParseFrom(data);

        var avatar = connection.Player!.AvatarManager!.GetFormalAvatar((int)req.BaseAvatarId);
        if (avatar == null) return;
        avatar.TakeReward((int)req.Promotion);
        await connection.Player!.InventoryManager!.AddItem(101, 1, false);
        await connection.SendPacket(new PacketPlayerSyncScNotify(avatar));

        await connection.SendPacket(new PacketTakePromotionRewardScRsp());
    }
}