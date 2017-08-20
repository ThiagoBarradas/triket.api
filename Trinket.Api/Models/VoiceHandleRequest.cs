namespace Trinket.Api.Models
{
    public class VoiceHandleRequest
    {
        public string Text { get; set; }

        public Owner Owner { get; set; }

        public double[] Location { get; set; }
    }
}
