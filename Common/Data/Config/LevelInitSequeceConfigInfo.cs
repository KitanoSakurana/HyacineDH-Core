using HyacineCore.Server.Data.Config.Task;
using Newtonsoft.Json.Linq;

namespace HyacineCore.Server.Data.Config;

public class LevelInitSequeceConfigInfo
{
    public List<TaskConfigInfo> TaskList { get; set; } = [];

    public static LevelInitSequeceConfigInfo LoadFromJsonObject(JObject obj)
    {
        LevelInitSequeceConfigInfo info = new();
        if (obj.ContainsKey(nameof(TaskList)))
            info.TaskList = obj[nameof(TaskList)]?.Select(x => TaskConfigInfo.LoadFromJsonObject((x as JObject)!))
                .ToList() ?? [];
        return info;
    }
}