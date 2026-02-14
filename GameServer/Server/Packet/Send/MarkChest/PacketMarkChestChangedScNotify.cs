using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.MarkChest;

public class PacketMarkChestChangedScNotify : BasePacket
{
    public PacketMarkChestChangedScNotify(PlayerInstance player) : base(CmdIds.MarkChestChangedScNotify)
    {
        var proto = new MarkChestChangedScNotify
        {
            MarkChestFuncInfo =
            {
                player.SceneData!.MarkedChestData.Select(x => new MarkChestFuncInfo
                {
                    FuncId = (uint)x.Key,
                    MarkChestInfoList = { x.Value.Select(y => y.ToProto()) }
                })
            }
        };

        SetData(proto);
    }
}