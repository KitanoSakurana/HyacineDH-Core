using HyacineCore.Server.Database.Scene;
using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Raid;

public class PacketStartRaidScRsp : BasePacket
{
    public PacketStartRaidScRsp(RaidRecord record, PlayerInstance player) : base(CmdIds.StartRaidScRsp)
    {
        var proto = new StartRaidScRsp
        {
            Scene = new OGLAIJDPIOE
            {
                Lineup = player.LineupManager!.GetCurLineup()!.ToProto(),
                RaidId = (uint)record.RaidId,
                AKHMIOEJMBE = player.SceneInstance!.ToProto(),
                WorldLevel = (uint)record.WorldLevel
            }
        };

        SetData(proto);
    }

    public PacketStartRaidScRsp(Retcode ret) : base(CmdIds.StartRaidScRsp)
    {
        var proto = new StartRaidScRsp
        {
            Retcode = (uint)ret
        };

        SetData(proto);
    }
}
