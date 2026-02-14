using HyacineCore.Server.Database.Account;
using HyacineCore.Server.Internationalization;

namespace HyacineCore.Server.Command.Command.Cmd;

[CommandInfo("account", "Game.Command.Account.Desc", "Game.Command.Account.Usage", permission: "HyacineLover.manage")]
public class CommandAccount : ICommand
{
    [CommandMethod("create")]
    public async ValueTask CreateAccount(CommandArg arg)
    {
        var args = arg.GetMethodArgs("create");
        if (args.Count < 1)
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Notice.InvalidArguments"));
            return;
        }

        var account = args[0];
        var uid = 0;

        if (args.Count > 1)
            if (!int.TryParse(args[1], out uid))
            {
                await arg.SendMsg(I18NManager.Translate("Game.Command.Account.InvalidUid"));
                return;
            }

        if (AccountData.GetAccountByUserName(account) != null)
        {
            await arg.SendMsg(string.Format(I18NManager.Translate("Game.Command.Account.DuplicateAccount"), account));
            return;
        }

        if (uid != 0 && AccountData.GetAccountByUid(uid) != null)
        {
            await arg.SendMsg(string.Format(I18NManager.Translate("Game.Command.Account.DuplicateUID"), uid));
            return;
        }

        try
        {
            AccountHelper.CreateAccount(account, uid);
            await arg.SendMsg(I18NManager.Translate("Game.Command.Account.CreateSuccess", account));
        }
        catch (Exception ex)
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Account.CreateError", ex.Message));
        }
    }
}
