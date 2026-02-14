using HyacineCore.Server.Enums.Mission;

namespace HyacineCore.Server.GameServer.Game.Mission.FinishType;

public class MissionFinishTypeAttribute(MissionFinishTypeEnum finishType) : Attribute
{
    public MissionFinishTypeEnum FinishType { get; private set; } = finishType;
}