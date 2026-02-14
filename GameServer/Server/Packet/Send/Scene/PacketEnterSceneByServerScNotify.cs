using HyacineCore.Server.GameServer.Game.Scene;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Scene;

public class PacketEnterSceneByServerScNotify : BasePacket
{
    public PacketEnterSceneByServerScNotify(SceneInstance scene) : base(CmdIds.EnterSceneByServerScNotify)
    {
        var sceneInfo = scene.ToProto();
        var notify = new EnterSceneByServerScNotify
        {
            Scene = sceneInfo,
            Lineup = scene.Player.LineupManager!.GetCurLineup()!.ToProto()
        };

        SetData(notify);
    }
}