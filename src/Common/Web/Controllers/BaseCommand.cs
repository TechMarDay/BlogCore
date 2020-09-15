using Common.Models;
using MediatR;
using Newtonsoft.Json;
using System;

namespace Web.Controllers
{
    public abstract class BaseCommand<T> : IRequest<T>
    {
        [JsonIgnore]
        public UserSession LoginSession { set; get; }

        public int? CommandStyle { set; get; }
        public string CommandId { set; get; }

        [JsonIgnore]
        public CommandStyles CmdStyle
        {
            get
            {
                if (string.IsNullOrEmpty(CommandId)) return CommandStyles.Normal;
                else if (CommandStyle == null) return CommandStyles.Normal;
                else if (!Enum.IsDefined(typeof(CommandStyles), CommandStyle.Value)) return CommandStyles.Normal;
                else return (CommandStyles)CommandStyle.Value;
            }
        }
    }
}