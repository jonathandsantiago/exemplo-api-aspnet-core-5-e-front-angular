using FavoDeMel.Domain.Interfaces;
using System.Collections.Generic;

namespace FavoDeMel.Domain.Models.Settings
{
    public class Settings : Dictionary<string, object>, ISettings<string, object>
    { }
}