using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Game.Sync;

public abstract class BaseSyncData
{
    public abstract void SyncData(in PlayerSyncScNotify notify);
}