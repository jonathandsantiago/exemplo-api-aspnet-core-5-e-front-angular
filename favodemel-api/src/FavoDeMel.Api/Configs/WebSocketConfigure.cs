using FavoDeMel.Api.Configs.Interfaces;
using FavoDeMel.Domain.Consumers;
using FavoDeMel.Domain.Extensions;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Models.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Net;
using System.Net.WebSockets;

namespace FavoDeMel.Api.Configs
{
    public class WebSocketConfigure : IApiConfigure
    {
        public void AddAplication(IApplicationBuilder app, IWebHostEnvironment env, ISettings<string, object> settings)
        {
            app.UseWebSockets(new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
            });

            var kafkaSettings = settings.GetSetting<KafkaSettings>();
            KafkaConsumer kafkaConsumer = new(kafkaSettings);

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {                       
                        using WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        await kafkaConsumer.ConnectWebSocket(context, webSocket);
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                }
            });
        }
    }
}
