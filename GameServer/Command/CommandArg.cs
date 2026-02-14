using HyacineCore.Server.GameServer.Server;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Util;

namespace HyacineCore.Server.Command.Command;

public class CommandArg
{
    public CommandArg(string raw, ICommandSender sender, Connection? con = null)
    {
        Raw = raw;
        Sender = sender;
        var args = raw.Split(' ');
        foreach (var arg in args)
        {
            if (string.IsNullOrEmpty(arg)) continue;

            // Treat only flag-like args as CharacterArgs:
            // - target: @10001
            // - numeric short flags: l80 r6 x20
            if (arg[0] == '@' && arg.Length > 1)
            {
                CharacterArgs["@"] = arg[1..];
                Args.Add(arg);
                continue;
            }

            if (arg.Length > 1 && char.IsLetter(arg[0]) && int.TryParse(arg[1..], out _))
            {
                CharacterArgs[arg[..1]] = arg[1..];
                Args.Add(arg);
                continue;
            }

            BasicArgs.Add(arg);
            Args.Add(arg);
        }

        if (con != null) Target = con;

        CharacterArgs.TryGetValue("@", out var target);
        if (target == null) return;
        if (HyacineCoreListener.Connections.Values.ToList()
                .Find(item => (item as Connection)?.Player?.Uid.ToString() == target) is Connection connection)
            Target = connection;
    }

    public string Raw { get; }
    public List<string> Args { get; } = [];
    public List<string> BasicArgs { get; } = [];
    public Dictionary<string, string> CharacterArgs { get; } = [];
    public Connection? Target { get; set; }
    public ICommandSender Sender { get; }

    public int GetInt(int index)
    {
        if (BasicArgs.Count <= index) return 0;
        _ = int.TryParse(BasicArgs[index], out var res);
        return res;
    }

    public async ValueTask SendMsg(string msg)
    {
        await Sender.SendMsg(msg);
    }

    public override string ToString()
    {
        return $"BasicArg: {BasicArgs.ToArrayString()}. CharacterArg: {CharacterArgs.ToJsonString()}.";
    }
}
