using HyacineCore.Server.GameServer.Server.Packet.Send.Scene;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Scene;

[Opcode(CmdIds.StartCocoonStageCsReq)]
public class HandlerStartCocoonStageCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = StartCocoonStageCsReq.Parser.ParseFrom(data);
        var battle =
            await connection.Player!.BattleManager!.StartCocoonStage((int)req.CocoonId, (int)req.Wave,
                (int)req.WorldLevel);
        connection.Player.SceneInstance?.OnEnterStage();

        if (battle != null)
            await connection.SendPacket(new PacketStartCocoonStageScRsp(battle, (int)req.CocoonId, (int)req.Wave));
        else
            await connection.SendPacket(new PacketStartCocoonStageScRsp());
    }
}