using Nancy.ModelBinding;
using Trinket.Api.Models;

namespace Trinket.Api.Controller
{
    public class VoiceController : BaseController
    {
        public VoiceController()
        {
            this.Post("voice", args => this.Handle());
        }

        public object Handle()
        {
            var request = this.Bind<VoiceHandleRequest>();
            var response = this.VoiceManager.HandleVoice(request);

            return this.CreateResponse(response);
        }

    }
}