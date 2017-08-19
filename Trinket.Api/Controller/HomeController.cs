using Nancy;

namespace Trinket.Api.Controller
{
    public class HomeController : BaseController
    {
        public HomeController()
        {
            this.Get("", args => this.Home());
            this.Get("test", args => this.Test());
        }

        public object Home()
        {
            #region Text Home

            var text = @"


            .----------------.  .----------------.  .----------------.  .-----------------. .----------------.  .----------------.  .----------------. 
           | .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. |
           | |  _________   | || |  _______     | || |     _____    | || | ____  _____  | || |  ___  ____   | || |  _________   | || |  _________   | |
           | | |  _   _  |  | || | |_   __ \    | || |    |_   _|   | || ||_   \|_   _| | || | |_  ||_  _|  | || | |_   ___  |  | || | |  _   _  |  | |
           | | |_/ | | \_|  | || |   | |__) |   | || |      | |     | || |  |   \ | |   | || |   | |_/ /    | || |   | |_  \_|  | || | |_/ | | \_|  | |
           | |     | |      | || |   |  __ /    | || |      | |     | || |  | |\ \| |   | || |   |  __'.    | || |   |  _|  _   | || |     | |      | |
           | |    _| |_     | || |  _| |  \ \_  | || |     _| |_    | || | _| |_\   |_  | || |  _| |  \ \_  | || |  _| |___/ |  | || |    _| |_     | |
           | |   |_____|    | || | |____| |___| | || |    |_____|   | || ||_____|\____| | || | |____||____| | || | |_________|  | || |   |_____|    | |
           | |              | || |              | || |              | || |              | || |              | || |              | || |              | |
           | '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' |
            '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------' ";
            #endregion

            return Response.AsText(text);
        }

        public object Test()
        {
            var config = new ApiAiSDK.AIConfiguration("b482bc38b215444494744d9c45c2a1f1", ApiAiSDK.SupportedLanguage.PortugueseBrazil);
            var client = new ApiAiSDK.NETCore.ApiAi(config);
            var request = new ApiAiSDK.Model.AIRequest("palmito kwm1183 Rio de Janeiro");


            var task = client.TextRequestAsync(request);
            while (task.IsCompleted == false) { }

            var result = task.Result;

            return Response.AsJson(result);
        }
    }
}
