namespace HyacineCore.Server.Data.Config.Scene;

public class NpcInfo : PositionInfo
{
    public int NPCID { get; set; }
    public bool IsClientOnly { get; set; }
}