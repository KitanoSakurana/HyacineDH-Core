using HyacineCore.Server.Util;
using HyacineCore.Server.WebServer.Handler;
using Microsoft.AspNetCore.Mvc;
using HyacineCore.Server.WebServer.Request;
namespace HyacineCore.Server.WebServer.Controllers;

[ApiController]
[Route("/")]
public class GateServerRoutes
{
    [HttpGet("/query_gateway")]
    public async ValueTask<ContentResult> QueryGateway([FromQuery] GateWayRequest req)
    {
        if (!ConfigManager.Config.ServerOption.ServerConfig.RunGateway)
            return new ContentResult
            {
                StatusCode = 404
            };

        await ValueTask.CompletedTask;
        var handler = new QueryGatewayHandler(req);
        return new ContentResult
        {
            Content = handler.Data,
            StatusCode = 200,
            ContentType = "text/plain"
        };
    }
}
