namespace HyacineCore.Server.Data.Excel;

[ResourceEntity("NPCData.json")]
public class NPCDataExcel : ExcelResource
{
    public int ID { get; set; }

    public override int GetId()
    {
        return ID;
    }

    public override void Loaded()
    {
        GameData.NpcDataData.Add(ID, this);
    }
}