using HyacineCore.Server.Data;
using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Message;

public class PacketGetNpcStatusScRsp : BasePacket
{
    public PacketGetNpcStatusScRsp(PlayerInstance player) : base(CmdIds.GetNpcStatusScRsp)
    {
        var proto = new GetNpcStatusScRsp();

        foreach (var item in GameData.MessageContactsConfigData.Values)
            proto.NpcStatusList.Add(new NpcStatus
            {
                NpcId = (uint)item.ID,
                IsFinish = player.MessageManager!.GetMessageGroup(item.ID).Count > 0
            });

        SetData(proto);
    }
}