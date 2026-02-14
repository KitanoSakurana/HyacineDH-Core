using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.MarkChest;

public class PacketUpdateMarkChestScRsp : BasePacket
{
    public PacketUpdateMarkChestScRsp(uint funcId, PlayerInstance player) : base(CmdIds.UpdateMarkChestScRsp)
    {
        var proto = new UpdateMarkChestScRsp
        {
            FuncId = funcId,
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