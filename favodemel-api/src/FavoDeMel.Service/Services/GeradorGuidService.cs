using FavoDeMel.Service.Interfaces;
using MassTransit;
using System;

namespace FavoDeMel.Service.Services
{
    public class GeradorGuidService : IGeradorGuidService
    {
        public Guid GetNexGuid()
        {
            return new Guid(NewId.Next().ToString("D").ToUpperInvariant());
        }
    }
}