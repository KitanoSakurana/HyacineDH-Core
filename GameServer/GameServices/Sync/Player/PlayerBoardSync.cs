using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Game.Sync.Player;

public class PlayerBoardSync(PlayerInstance player) : BaseSyncData
{
    public override void SyncData(in PlayerSyncScNotify notify)
    {
        notify.PlayerboardModuleSync = new PlayerBoardModuleSync
        {
            Signature = player.Data.Signature,
            HeadFrameInfo = player.Data.HeadFrame.ToProto()
        };
    }
}
