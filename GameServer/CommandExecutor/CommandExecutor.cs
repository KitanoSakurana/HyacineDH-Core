using HyacineCore.Server.Command;

namespace HyacineCore.Server.GameServer.Command;

public static class CommandExecutor
{
    public delegate void RunCommand(ICommandSender sender, string cmd);

    public static event RunCommand? OnRunCommand;

    public static void ExecuteCommand(ICommandSender sender, string cmd)
    {
        OnRunCommand?.Invoke(sender, cmd);
    }
}