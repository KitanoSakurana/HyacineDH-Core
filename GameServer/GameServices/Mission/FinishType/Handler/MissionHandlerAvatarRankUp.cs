using HyacineCore.Server.Data.Config;
using HyacineCore.Server.Data.Excel;
using HyacineCore.Server.Enums.Mission;
using HyacineCore.Server.GameServer.Game.Player;

namespace HyacineCore.Server.GameServer.Game.Mission.FinishType.Handler;

[MissionFinishType(MissionFinishTypeEnum.AvatarRankUp)]
public class MissionHandlerAvatarRankUp : MissionFinishTypeHandler
{
    public override async ValueTask HandleMissionFinishType(PlayerInstance player, SubMissionInfo info, object? arg)
    {
        // this type wont be used in mission
        await ValueTask.CompletedTask;
    }

    public override async ValueTask HandleQuestFinishType(PlayerInstance player, QuestDataExcel quest,
        FinishWayExcel excel, object? arg)
    {
        foreach (var avatarId in excel.ParamIntList)
        {
            var avatar = player.AvatarManager?.GetFormalAvatar(avatarId);
            if (avatar != null && avatar.GetPathInfo(avatarId)?.Rank > 0)
                await player.QuestManager!.AddQuestProgress(quest.QuestID, 1);
        }
    }
}