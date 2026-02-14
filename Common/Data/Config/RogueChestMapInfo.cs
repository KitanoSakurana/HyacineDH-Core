namespace HyacineCore.Server.Data.Config;

/// <summary>
///     Orginal Name: RogueChestMapConfig
/// </summary>
public class RogueChestMapInfo
{
    public List<int> PreStartRoomIDList { get; set; } = [];
    public int Width { get; set; }
    public int Height { get; set; }
    public int StartGridItemID { get; set; }
    public int EndGridItemID { get; set; }
    public Dictionary<int, RogueChestGridItem> RogueChestGridItemMap { get; set; } = [];
    public Dictionary<int, RogueChestModifierEvent> RogueChestEventMap { get; set; } = [];
    public List<RogueBlockCreateGroup> RogueBlockCreateGroupList { get; set; } = [];
}

public class RogueChestGridItem
{
    public int PosX { get; set; }
    public int PosY { get; set; }
    public List<int> BlockTypeList { get; set; } = [];
    public bool ExportToJson { get; set; }
}

public class RogueChestModifierEvent
{
    public int TriggerType { get; set; }
    public List<int> TriggerParamList { get; set; } = [];
    public int EffectType { get; set; }
    public List<int> EffectParamList { get; set; } = [];
    public List<int> EffectParam2List { get; set; } = [];
    public float Weight { get; set; }
}

public class RogueBlockCreateGroup
{
    public int BlockCreateID { get; set; }
    public int GroupID { get; set; }
    public int BlockType { get; set; }
    public List<RogueDLCBlockWeight> BlockCreatNumList { get; set; } = [];
    public List<RogueDLCMarkType> MarkCreateRandomList { get; set; } = [];
}

public class RogueDLCBlockWeight
{
    public int CreateNum { get; set; }
    public int Weight { get; set; }
}

public class RogueDLCMarkType
{
    public int TypeID { get; set; }
    public int Weight { get; set; }
}
