namespace HyacineCore.Server.GameServer.Game.Scene.Component;

public abstract class BaseSceneComponent(SceneInstance scene)
{
    public SceneInstance SceneInst { get; } = scene;
    public abstract ValueTask Initialize();
}