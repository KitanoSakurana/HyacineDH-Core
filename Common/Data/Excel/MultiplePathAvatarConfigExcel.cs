using HyacineCore.Server.Enums.Avatar;
using HyacineCore.Server.Enums.Quest;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HyacineCore.Server.Data.Excel;

[ResourceEntity("MultiplePathAvatarConfig.json")]
public class MultiplePathAvatarConfigExcel : ExcelResource
{
    public List<Condition> UnlockConditions { get; set; } = [];
    public string ChangeConfigPath { get; set; } = "";

    [JsonConverter(typeof(StringEnumConverter))]
    public GenderTypeEnum Gender { get; set; }

    public int AvatarID { get; set; }
    public int BaseAvatarID { get; set; }

    public override int GetId()
    {
        return AvatarID;
    }

    public override void Loaded()
    {
        GameData.MultiplePathAvatarConfigData.Add(AvatarID, this);
    }
}

public class Condition
{
    public string Param { get; set; } = "";
    public ConditionTypeEnum Type { get; set; }
}