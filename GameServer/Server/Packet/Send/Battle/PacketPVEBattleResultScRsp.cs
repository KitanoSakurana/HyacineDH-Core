using HyacineCore.Server.GameServer.Game.Battle;
using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Battle;

public class PacketPVEBattleResultScRsp : BasePacket
{
    public PacketPVEBattleResultScRsp() : base(CmdIds.PVEBattleResultScRsp)
    {
        var proto = new PVEBattleResultScRsp
        {
            Retcode = 1
        };

        SetData(proto);
    }

    public PacketPVEBattleResultScRsp(PVEBattleResultCsReq req, PlayerInstance player, BattleInstance battle) : base(
        CmdIds.PVEBattleResultScRsp)
    {
        var proto = new PVEBattleResultScRsp
        {
            DropData = battle.GetDropItemList(),
            StageId = req.StageId,
            BattleId = req.BattleId,
            EndStatus = req.EndStatus,
            CheckIdentical = true,
            ItemListUnk1 = new ItemList(),
            ItemListUnk2 = new ItemList(),
            MultipleDropData = new ItemList(),
            EventId = (uint)battle.EventId
        };

        SetData(proto);
    }
}