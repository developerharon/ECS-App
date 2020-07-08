namespace EcsApp.Models.ApiModels
{
    public class ClockResponseModel
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public bool IsClockActive { get; set; }
    }
}
