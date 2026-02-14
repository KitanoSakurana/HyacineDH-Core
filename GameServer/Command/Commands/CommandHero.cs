using HyacineCore.Server.Enums.Avatar;
using HyacineCore.Server.Internationalization;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.Command.Command.Cmd;

[CommandInfo("hero", "Game.Command.Hero.Desc", "Game.Command.Hero.Usage")]
public class CommandHero : ICommand
{
    [CommandMethod("0 gender")]
    public async ValueTask ChangeGender(CommandArg arg)
    {
        if (arg.Target == null)
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Notice.PlayerNotFound"));
            return;
        }

        var basicArgs = arg.GetMethodBasicArgs("gender");
        if (basicArgs.Count < 1 || !int.TryParse(basicArgs[0], out var genderRaw))
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Notice.InvalidArguments"));
            return;
        }

        var gender = (Gender)genderRaw;
        if (gender == Gender.None)
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Hero.GenderNotSpecified"));
            return;
        }

        var player = arg.Target!.Player!;
        player.Data.CurrentGender = gender;
        await player.ChangeAvatarPathType(8001, MultiPathAvatarTypeEnum.Warrior);

        await arg.SendMsg(I18NManager.Translate("Game.Command.Hero.GenderChanged"));
    }

    [CommandMethod("0 type")]
    public async ValueTask ChangeType(CommandArg arg)
    {
        if (arg.Target == null)
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Notice.PlayerNotFound"));
            return;
        }

        var basicArgs = arg.GetMethodBasicArgs("type");
        if (basicArgs.Count < 1 || !int.TryParse(basicArgs[0], out var typeRaw))
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Notice.InvalidArguments"));
            return;
        }

        var pathType = (MultiPathAvatarTypeEnum)typeRaw;
        if (pathType == 0)
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Hero.HeroTypeNotSpecified"));
            return;
        }

        var player = arg.Target!.Player!;
        await player.ChangeAvatarPathType(8001, pathType);

        await arg.SendMsg(I18NManager.Translate("Game.Command.Hero.HeroTypeChanged"));
    }
}
