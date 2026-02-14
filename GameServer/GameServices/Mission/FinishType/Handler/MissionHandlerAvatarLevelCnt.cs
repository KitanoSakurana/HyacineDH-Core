using HyacineCore.Server.Data.Config;
using HyacineCore.Server.Data.Excel;
using HyacineCore.Server.Enums.Mission;
using HyacineCore.Server.GameServer.Game.Player;

namespace HyacineCore.Server.GameServer.Game.Mission.FinishType.Handler;

[MissionFinishType(MissionFinishTypeEnum.AvatarLevelCnt)]
public class MissionHandlerAvatarLevelCnt : MissionFinishTypeHandler
{
    public override async ValueTask HandleMissionFinishType(PlayerInstance player, SubMissionInfo info, object? arg)
    {
        // this type wont be used in mission
        await ValueTask.CompletedTask;
    }

    public override async ValueTask HandleQuestFinishType(PlayerInstance player, QuestDataExcel quest,
        FinishWayExcel excel, object? arg)
    {
        var avatarCount = 0;
        foreach (var avatar in player.AvatarManager?.AvatarData.FormalAvatars ?? [])
            if (avatar.Level >= excel.ParamInt1)
                avatarCount++;

        await player.QuestManager!.UpdateQuestProgress(quest.QuestID, avatarCount);
    }
}