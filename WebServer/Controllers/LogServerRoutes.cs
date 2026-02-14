using Microsoft.AspNetCore.Mvc;

namespace HyacineCore.Server.WebServer.Controllers;

[ApiController]
[Route("/")]
public class LogServerRoutes
{
    [HttpPost("/sdk/dataUpload")]
    [HttpPost("/crashdump/dataUpload")]
    [HttpPost("/apm/dataUpload")]
    public ContentResult LogUpload()
    {
        return new ContentResult { Content = "{\"code\":0}", ContentType = "application/json" };
    }

    [HttpPost("/common/h5log/log/batch")]
    public ContentResult BatchUpload()
    {
        return new ContentResult
            { Content = "{\"retcode\":0,\"message\":\"success\",\"data\":null}", ContentType = "application/json" };
    }
}