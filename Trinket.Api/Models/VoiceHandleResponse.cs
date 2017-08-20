using System;
using System.Collections.Generic;
using System.Text;

namespace Trinket.Api.Models
{
    public class VoiceHandleResponse
    {
        public string Action { get; set; }

        public string Text { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
