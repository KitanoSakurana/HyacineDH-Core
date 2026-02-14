using HyacineCore.Server.Command;
using HyacineCore.Server.Database;
using HyacineCore.Server.Database.Account;
using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.GameServer.Server.Packet.Send.Chat;
using HyacineCore.Server.Util;

namespace HyacineCore.Server.GameServer.Command;

public class PlayerCommandSender(PlayerInstance player) : ICommandSender
{
    public PlayerInstance Player = player;

    public async ValueTask SendMsg(string msg)
    {
        await Player.SendPacket(new PacketRevcMsgScNotify((uint)Player.Uid,
            (uint)ConfigManager.Config.ServerOption.ServerProfile.Uid, msg.Replace("\n", "    ")));
    }

    public bool HasPermission(string permission)
    {
        var account = DatabaseHelper.Instance!.GetInstance<AccountData>(Player.Uid)!;
        return account.Permissions!.Contains(permission);
    }

    public int GetSender()
    {
        return Player.Uid;
    }
}