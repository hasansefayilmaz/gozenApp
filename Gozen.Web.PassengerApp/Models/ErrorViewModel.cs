namespace Gozen.Web.PassengerApp.Models
{
    public class ErrorViewModel
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool ShowErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
    }
}