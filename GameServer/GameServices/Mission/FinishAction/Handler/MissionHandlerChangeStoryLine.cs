using HyacineCore.Server.Enums.Mission;
using HyacineCore.Server.GameServer.Game.Player;

namespace HyacineCore.Server.GameServer.Game.Mission.FinishAction.Handler;

[MissionFinishAction(FinishActionTypeEnum.ChangeStoryLine)]
public class MissionHandlerChangeStoryLine : MissionFinishActionHandler
{
    public override async ValueTask OnHandle(List<int> @params, List<string> paramString, PlayerInstance player)
    {
        var toStoryLineId = @params[0];
        var toEntryId = @params[1];
        var toAnchorGroup = @params[2];
        var toAnchorId = @params[3];

        if (toStoryLineId == 0)
            // exit
            await player.StoryLineManager!.FinishStoryLine(toEntryId, toAnchorGroup, toAnchorId);
        else
            await player.StoryLineManager!.InitStoryLine(toStoryLineId, toEntryId, toAnchorGroup, toAnchorId);
    }
}