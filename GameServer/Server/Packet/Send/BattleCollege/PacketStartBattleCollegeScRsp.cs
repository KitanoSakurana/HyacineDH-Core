using HyacineCore.Server.GameServer.Game.Battle;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.BattleCollege;

public class PacketStartBattleCollegeScRsp : BasePacket
{
    public PacketStartBattleCollegeScRsp(uint id, Retcode retCode, BattleInstance? instance) : base(
        CmdIds.StartBattleCollegeScRsp)
    {
        var proto = new StartBattleCollegeScRsp
        {
            Retcode = (uint)retCode,
            Id = id
        };

        if (instance != null)
            proto.BattleInfo = instance.ToProto();

        SetData(proto);
    }
}