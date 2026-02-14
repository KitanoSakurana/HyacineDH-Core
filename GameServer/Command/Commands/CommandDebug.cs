using HyacineCore.Server.Data;
using HyacineCore.Server.Data.Custom;
using HyacineCore.Server.Internationalization;

namespace HyacineCore.Server.Command.Command.Cmd;

[CommandInfo("debug", "Game.Command.Debug.Desc", "Game.Command.Debug.Usage")]
public class CommandDebug : ICommand
{
    [CommandMethod("0 specific")]
    public async ValueTask SpecificNextStage(CommandArg arg)
    {
        var player = arg.Target?.Player;
        if (player == null)
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Notice.PlayerNotFound"));
            return;
        }

        var basicArgs = arg.GetMethodBasicArgs("specific");
        if (basicArgs.Count == 0)
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Notice.InvalidArguments"));
            return;
        }

        if (!int.TryParse(basicArgs[0], out var stageId))
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Notice.InvalidArguments"));
            return;
        }

        if (!GameData.StageConfigData.TryGetValue(stageId, out var stage))
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Debug.InvalidStageId"));
            return;
        }

        player.BattleManager!.NextBattleStageConfig = stage;
        await arg.SendMsg(I18NManager.Translate("Game.Command.Debug.SetStageId"));
    }

    [CommandMethod("0 monster")]
    public async ValueTask AddMonster(CommandArg arg)
    {
        var player = arg.Target?.Player;
        if (player == null)
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Notice.PlayerNotFound"));
            return;
        }

        var basicArgs = arg.GetMethodBasicArgs("monster");
        if (basicArgs.Count == 0)
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Notice.InvalidArguments"));
            return;
        }

        if (!int.TryParse(basicArgs[0], out var monsterId))
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Notice.InvalidArguments"));
            return;
        }

        if (!GameData.MonsterConfigData.TryGetValue(monsterId, out _))
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Debug.InvalidStageId"));
            return;
        }

        player.BattleManager!.NextBattleMonsterIds.Add(monsterId);
        await arg.SendMsg(I18NManager.Translate("Game.Command.Debug.SetStageId"));
    }

    [CommandMethod("0 customP")]
    public async ValueTask AddCustomPacket(CommandArg arg)
    {
        var conn = arg.Target;
        if (conn == null)
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Notice.PlayerNotFound"));
            return;
        }

        var args = arg.GetMethodArgs("customP");
        if (args.Count < 1)
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Notice.InvalidArguments"));
            return;
        }

        var packetFilePath = args[0];
        // Load custom packet queue from file
        if (!File.Exists(packetFilePath))
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Debug.CustomPacketFileNotFound"));
            return;
        }

        var fileContent = await File.ReadAllTextAsync(packetFilePath);
        var customPacketQueue = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomPacketQueueConfig>(fileContent);

        if (customPacketQueue == null || customPacketQueue.Queue.Count == 0)
        {
            await arg.SendMsg(I18NManager.Translate("Game.Command.Debug.CustomPacketFileInvalid"));
            return;
        }

        conn.CustomPacketQueue.Clear();
        conn.CustomPacketQueue.AddRange(customPacketQueue.Queue);
        await arg.SendMsg(I18NManager.Translate("Game.Command.Debug.CustomPacketFileLoaded"));
    }
}
