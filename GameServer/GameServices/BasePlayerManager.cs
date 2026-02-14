using HyacineCore.Server.GameServer.Game.Player;

namespace HyacineCore.Server.GameServer.Game;

public class BasePlayerManager(PlayerInstance player)
{
    public PlayerInstance Player { get; private set; } = player;
}