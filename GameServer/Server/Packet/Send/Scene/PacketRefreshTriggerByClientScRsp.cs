using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Scene;

public class PacketRefreshTriggerByClientScRsp : BasePacket
{
    public PacketRefreshTriggerByClientScRsp(Retcode ret, string triggerName, uint entityId) : base(
        CmdIds.RefreshTriggerByClientScRsp)
    {
        var rsp = new RefreshTriggerByClientScRsp
        {
            Retcode = (uint)ret,
            TriggerName = triggerName,
            RefreshTrigger = ret == Retcode.RetSucc,
            TriggerEntityId = entityId
        };

        SetData(rsp);
    }
}