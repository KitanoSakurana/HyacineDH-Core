using HyacineCore.Server.Enums.Mission;

namespace HyacineCore.Server.GameServer.Game.Mission.FinishAction;

[AttributeUsage(AttributeTargets.Class)]
public class MissionFinishActionAttribute(FinishActionTypeEnum finishAction) : Attribute
{
    public FinishActionTypeEnum FinishAction { get; } = finishAction;
}