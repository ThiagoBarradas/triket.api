using Nancy;
using Trinket.Api.Models;

namespace Trinket.Api.Manager
{
    public class VoiceManager : BaseManager
    {
        public BaseResponse<object> HandleVoice(VoiceHandleRequest Voice)
        {
            BaseResponse<object> response = new BaseResponse<object>();
            var config = new ApiAiSDK.AIConfiguration("b482bc38b215444494744d9c45c2a1f1", ApiAiSDK.SupportedLanguage.PortugueseBrazil);
            var client = new ApiAiSDK.NETCore.ApiAi(config);
            var request = new ApiAiSDK.Model.AIRequest(Voice.Text);

            var task = client.TextRequestAsync(request);
            while (task.IsCompleted == false) { }

            var result = task.Result;

            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            response.SuccessBody = result;

            return response;
        }
    }
}