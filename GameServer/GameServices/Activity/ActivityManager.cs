using HyacineCore.Server.Data;
using HyacineCore.Server.Database;
using HyacineCore.Server.Database.Activity;
using HyacineCore.Server.GameServer.Game.Activity.Activities;
using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Proto;
using HyacineCore.Server.Util;

namespace HyacineCore.Server.GameServer.Game.Activity;

public class ActivityManager : BasePlayerManager
{
    public ActivityManager(PlayerInstance player) : base(player)
    {
        Data = DatabaseHelper.Instance!.GetInstanceOrCreateNew<ActivityData>(player.Uid);

        if (Data.TrialActivityData.CurTrialStageId != 0) TrialActivityInstance = new TrialActivityInstance(this);
    }

    #region Data

    public ActivityData Data { get; set; }

    #endregion

    #region Instance

    public TrialActivityInstance? TrialActivityInstance { get; set; }

    #endregion

    public List<ActivityScheduleData> ToProto()
    {
        var proto = new List<ActivityScheduleData>();

        foreach (var activity in GameData.ActivityConfig.ScheduleData)
            proto.Add(new ActivityScheduleData
            {
                ActivityId = (uint)activity.ActivityId,
                BeginTime = activity.BeginTime,
                EndTime = activity.EndTime,
                PanelId = (uint)activity.PanelId
            });

        return proto;
    }

    public void UpdateLoginDays()
    {
        var login = Data.LoginActivityData;
        var now = Extensions.GetUnixSec();

        var currentDay = DateTimeOffset.FromUnixTimeSeconds(now).UtcDateTime.Date;
        var lastDay = login.LastUpdateTick > 0
            ? DateTimeOffset.FromUnixTimeSeconds(login.LastUpdateTick).UtcDateTime.Date
            : DateTime.MinValue.Date;

        if (currentDay <= lastDay) return;

        var keys = login.LoginDays.Keys.ToList();
        foreach (var id in keys)
        {
            var days = login.LoginDays[id];
            login.LoginDays[id] = days + 1;
        }

        login.LastUpdateTick = now;
    }

    public GetLoginActivityScRsp GetLoginInfo()
    {
        var proto = new GetLoginActivityScRsp
        {
            Retcode = 0
        };

        var list = Data.LoginActivityData.ToProto(ResolvePanelId);
        proto.LoginActivityList.AddRange(list);
        return proto;
    }

    public ValueTask<(ItemList rewardProto, uint panelId, uint retcode)> TakeLoginReward(uint id, uint takeDays)
    {
        var login = Data.LoginActivityData;

        if (!login.LoginDays.TryGetValue(id, out var loginDays))
            login.LoginDays[id] = loginDays = takeDays;

        if (takeDays == 0 || takeDays > loginDays)
            return ValueTask.FromResult((new ItemList(), ResolvePanelId(id), (uint)Retcode.RetFail));

        if (!login.TakenRewards.TryGetValue(id, out var taken))
        {
            taken = [];
            login.TakenRewards[id] = taken;
        }

        if (taken.Contains(takeDays))
            return ValueTask.FromResult((new ItemList(), ResolvePanelId(id), (uint)Retcode.RetFail));

        taken.Add(takeDays);

        // Keep reward payload empty for now; client only requires successful status + panel id in this flow.
        var reward = new ItemList();
        return ValueTask.FromResult((reward, ResolvePanelId(id), (uint)Retcode.RetSucc));
    }

    private static uint ResolvePanelId(uint activityId)
    {
        var schedule = GameData.ActivityConfig.ScheduleData
            .FirstOrDefault(x => x.ActivityId == (int)activityId);
        if (schedule != null && schedule.PanelId > 0) return (uint)schedule.PanelId;

        return activityId;
    }
}
