using HyacineCore.Server.Enums.Scene;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Scene;

public class PacketGroupStateChangeScNotify : BasePacket
{
    public PacketGroupStateChangeScNotify(int entryId, int groupId, PropStateEnum propState) : base(
        CmdIds.GroupStateChangeScNotify)
    {
        var notify = new GroupStateChangeScNotify
        {
            GroupStateInfo = new GroupStateInfo
            {
                EntryId = (uint)entryId,
                GroupId = (uint)groupId,
                GroupState = (uint)propState
            }
        };

        SetData(notify);
    }
}