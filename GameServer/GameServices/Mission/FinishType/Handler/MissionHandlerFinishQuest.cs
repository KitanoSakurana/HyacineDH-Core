using HyacineCore.Server.Data.Config;
using HyacineCore.Server.Data.Excel;
using HyacineCore.Server.Enums.Mission;
using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Game.Mission.FinishType.Handler;

[MissionFinishType(MissionFinishTypeEnum.FinishQuest)]
public class MissionHandlerFinishQuest : MissionFinishTypeHandler
{
    public override async ValueTask HandleMissionFinishType(PlayerInstance player, SubMissionInfo info, object? arg)
    {
        // this type wont be used in mission
        await ValueTask.CompletedTask;
    }

    public override async ValueTask HandleQuestFinishType(PlayerInstance player, QuestDataExcel quest,
        FinishWayExcel excel, object? arg)
    {
        var questCount = 0;
        foreach (var qid in excel.ParamIntList)
        {
            var status = player.QuestManager?.GetQuestStatus(qid);
            if (status == QuestStatus.QuestFinish || status == QuestStatus.QuestClose)
                questCount++;
        }

        await player.QuestManager!.UpdateQuestProgress(quest.QuestID, questCount);
    }
}