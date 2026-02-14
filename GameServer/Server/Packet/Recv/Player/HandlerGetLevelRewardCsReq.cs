using HyacineCore.Server.Data;
using HyacineCore.Server.GameServer.Server.Packet.Send.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Player;

[Opcode(CmdIds.GetLevelRewardCsReq)]
public class HandlerGetLevelRewardCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GetLevelRewardCsReq.Parser.ParseFrom(data);

        var player = connection.Player;
        if (player == null || player.Data == null)
        {
            await connection.SendPacket(new PacketGetLevelRewardScRsp(Retcode.RetFail));
            return;
        }
        if (player.Data.TakenLevelReward.Contains((int)req.Level))
        {
            await connection.SendPacket(new PacketGetLevelRewardScRsp(Retcode.RetLevelRewardHasTaken));
            return;
        }

        if (player.Data.Level < req.Level)
        {
            await connection.SendPacket(new PacketGetLevelRewardScRsp(Retcode.RetLevelRewardLevelError));
            return;
        }

        if (!GameData.PlayerLevelConfigData.TryGetValue((int)req.Level, out var levelExcel))
        {
            await connection.SendPacket(new PacketGetLevelRewardScRsp(Retcode.RetLevelRewardLevelError));
            return;
        }

        player.Data.TakenLevelReward.Add((int)req.Level);
        if (player.InventoryManager == null)
        {
            await connection.SendPacket(new PacketGetLevelRewardScRsp(Retcode.RetFail));
            return;
        }

        var rewards = await player.InventoryManager.HandleReward(levelExcel.LevelRewardID);
        await connection.SendPacket(new PacketGetLevelRewardScRsp(req.Level, rewards));
    }
}
