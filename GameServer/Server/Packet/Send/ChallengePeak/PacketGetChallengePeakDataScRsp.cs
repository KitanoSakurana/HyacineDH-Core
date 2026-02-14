using HyacineCore.Server.Data;
using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.ChallengePeak;

public class PacketGetChallengePeakDataScRsp : BasePacket
{
    public PacketGetChallengePeakDataScRsp(PlayerInstance player) : base(CmdIds.GetChallengePeakDataScRsp)
    {
        var proto = new GetChallengePeakDataScRsp
        {
            CurrentPeakGroupId = player.ChallengePeakManager?.GetCurrentPeakGroupId() ?? 1
        };

        foreach (var groupId in GameData.ChallengePeakGroupConfigData.Keys.OrderBy(x => x))
            proto.ChallengePeakGroups.Add(player.ChallengePeakManager!.BuildGroup(groupId));

        SetData(proto);
    }
}
