using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;
using HyacineCore.Server.Util;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Scene;

[Opcode(CmdIds.SceneEntityMoveCsReq)]
public class HandlerSceneEntityMoveCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SceneEntityMoveCsReq.Parser.ParseFrom(data);
        if (req != null)
            foreach (var motion in req.EntityMotionList)
                if (connection.Player!.SceneInstance!.AvatarInfo.ContainsKey((int)motion.EntityId))
                {
                    connection.Player!.Data.Pos = motion.Motion.Pos.ToPosition();
                    connection.Player.Data.Rot = motion.Motion.Rot.ToPosition();
                    await connection.Player.OnMove();
                }

        await connection.SendPacket(CmdIds.SceneEntityMoveScRsp);
    }
}