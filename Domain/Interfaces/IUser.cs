using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    internal interface IUser
    {
        int Id { get; }
        string DiscordName { get; }
        string DiscordCode { get; }
        string ValorantName { get; }
        string ValorantCode { get; }
        long DiscordServerId { get; }
        DateTime LastPlayed { get; }
    }
}
