using Nancy;
using Trinket.Api.Models;

namespace Trinket.Api.Manager
{
    public class VoiceManager : BaseManager
    {
        public BaseResponse<object> HandleVoice(VoiceHandleRequest voiceRequest)
        {
            BaseResponse<object> response = new BaseResponse<object>();
            var voiceResponse = new VoiceHandleResponse();

            var config = new ApiAiSDK.AIConfiguration("b482bc38b215444494744d9c45c2a1f1", ApiAiSDK.SupportedLanguage.PortugueseBrazil);
            var client = new ApiAiSDK.NETCore.ApiAi(config);
            var request = new ApiAiSDK.Model.AIRequest(voiceRequest.Text);

            var task = client.TextRequestAsync(request);
            while (task.IsCompleted == false) { }

            var apiAiResponse = task.Result;
            voiceResponse.Text = apiAiResponse.Result.Fulfillment.Speech;

            if (apiAiResponse.Result.Action == "register.stolen" ||
                apiAiResponse.Result.Action == "recovered.stolen")
            {
                var vehicleResponse = (new VehicleManager()).CreateOrUpdateVehicle(new Vehicle()
                {
                    Owner = voiceRequest.Owner,
                    OwnerId = voiceRequest.Owner.Id,
                    IsStolen = (apiAiResponse.Result.Action == "register.stolen"),
                    Location = voiceRequest.Location,
                    LicensePlate = apiAiResponse.Result.Parameters["licensePlate"].ToString()
                });

                if (vehicleResponse.StatusCode == HttpStatusCode.OK)
                {
                    voiceResponse.Vehicle = (Vehicle)vehicleResponse.SuccessBody;
                    voiceResponse.Text = voiceResponse.Text + ((apiAiResponse.Result.Action == "register.stolen")
                        ? " Já inserimos as informações sobre o roubo do veículo."
                        : " Esse procedimento foi feito com sucesso. Ficamos felizes que você encontrou o veículo.");
                }
                else
                {
                    voiceResponse.Text = voiceResponse.Text + " Mas infelizmente não encontramos mais informações sobre a placa. Quer tentar novamente?";
                }
            }
            else if (apiAiResponse.Result.Action == "find.plate")
            {
                var vehicleResponse = (new VehicleManager()).GetVehicle(new Vehicle()
                {
                    Owner = voiceRequest.Owner,
                    OwnerId = voiceRequest.Owner.Id,
                    Location = voiceRequest.Location,
                    LicensePlate = apiAiResponse.Result.Parameters["licensePlate"].ToString()
                });

                if (vehicleResponse.StatusCode == HttpStatusCode.OK)
                {
                    voiceResponse.Vehicle = (Vehicle)vehicleResponse.SuccessBody;
                    voiceResponse.Text = voiceResponse.Text + ((voiceResponse.Vehicle.IsStolen == false) 
                        ? "Achamos o veículo e não há informações sobre roubo ou furto. Está tudo bem." 
                        : "Acho que estamos com problemas. Encontramos a informação que o veiculo foi roubado ou furtudo. Fuja para as colinas!");
                }
                else
                {
                    voiceResponse.Text = voiceResponse.Text + " Infelizmente não encontramos nada. Quer tentar novamente?";
                }
            }

            voiceResponse.Action = apiAiResponse.Result.Action;

            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.SuccessBody = voiceResponse;

            return response;
        }
    }
}