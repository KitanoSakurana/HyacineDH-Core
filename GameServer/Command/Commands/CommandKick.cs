using HyacineCore.Server.GameServer.Server.Packet.Send.Player;
using HyacineCore.Server.Internationalization;

namespace HyacineCore.Server.Command.Command.Cmd;

[CommandInfo("kick", "Game.Command.Kick.Desc", "Game.Command.Kick.Usage", permission: "HyacineLover.manage")]
public class CommandKick : ICommand
{
    [CommandDefault]
    public async ValueTask Kick(CommandArg arg)
    {
        if (arg.Target == null)
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Notice.PlayerNotFound"));
            return;
        }

        await arg.Target.SendPacket(new PacketPlayerKickOutScNotify());
        await arg.SendMsg(I18NManager.Translate("Game.Command.Kick.PlayerKicked", arg.Target.Player!.Data.Name!));
        arg.Target.Stop();
    }
}