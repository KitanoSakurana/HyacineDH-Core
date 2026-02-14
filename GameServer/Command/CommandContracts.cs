using HyacineCore.Server.Util;

namespace HyacineCore.Server.Command;

public interface ICommand;

public interface ICommandSender
{
    ValueTask SendMsg(string msg);
    bool HasPermission(string permission);
    int GetSender();
}

public sealed class ConsoleCommandSender(Logger logger) : ICommandSender
{
    public ValueTask SendMsg(string msg)
    {
        logger.Info(msg);
        return ValueTask.CompletedTask;
    }

    public bool HasPermission(string permission)
    {
        return true;
    }

    public int GetSender()
    {
        return 0;
    }
}

[AttributeUsage(AttributeTargets.Class)]
public sealed class CommandInfoAttribute : Attribute
{
    public CommandInfoAttribute(string name, string description, string usage, string[]? alias = null,
        string permission = "")
    {
        Name = name;
        Description = description;
        Usage = usage;
        Alias = alias ?? [];
        Permission = permission;
    }

    public string Name { get; }
    public string Description { get; }
    public string Usage { get; }
    public string[] Alias { get; }
    public string Permission { get; }
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class CommandDefaultAttribute : Attribute;

[AttributeUsage(AttributeTargets.Method)]
public sealed class CommandMethodAttribute : Attribute
{
    public CommandMethodAttribute(string? expression = null)
    {
        Conditions = ParseConditions(expression);
    }

    public CommandCondition[] Conditions { get; }

    private static CommandCondition[] ParseConditions(string? expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
        {
            return [];
        }

        var tokens = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var list = new List<CommandCondition>();

        for (var i = 0; i < tokens.Length; i++)
        {
            var token = tokens[i];
            if (int.TryParse(token, out var idx) && i + 1 < tokens.Length)
            {
                list.Add(new CommandCondition(idx, tokens[++i]));
            }
            else
            {
                list.Add(new CommandCondition(0, token));
            }
        }

        return [.. list];
    }
}

public readonly record struct CommandCondition(int Index, string ShouldBe);
