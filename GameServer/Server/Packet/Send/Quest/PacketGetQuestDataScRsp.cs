using HyacineCore.Server.Data;
using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Quest;

public class PacketGetQuestDataScRsp : BasePacket
{
    public PacketGetQuestDataScRsp(PlayerInstance player) : base(CmdIds.GetQuestDataScRsp)
    {
        var proto = new GetQuestDataScRsp();
        foreach (var quest in GameData.QuestDataData.Values)
            proto.QuestList.Add(new Proto.Quest
            {
                Id = (uint)quest.QuestID,
                Status = player.QuestManager?.GetQuestStatus(quest.QuestID) ?? QuestStatus.QuestNone
            });
        SetData(proto);
    }
}