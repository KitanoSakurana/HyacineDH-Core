namespace HyacineCore.Server.Data.Excel;

[ResourceEntity("DialogueEvent.json")]
public class DialogueEventExcel : ExcelResource
{
    public int EventID { get; set; }
    public int RogueEffectType { get; set; }
    public List<int> RogueEffectParamList { get; set; } = [];
    public int CostType { get; set; }
    public List<int> CostParamList { get; set; } = [];
    public int DynamicContentID { get; set; }
    public int DescValue { get; set; }

    public override int GetId()
    {
        return EventID;
    }

    public override void Loaded()
    {
        GameData.DialogueEventData.Add(EventID, this);
    }
}
