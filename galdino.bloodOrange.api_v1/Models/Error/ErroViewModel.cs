namespace galdino.bloodOrange.api_v1.Models.Error
{
    public class ErroViewModel
    {
        public bool Core { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }
    }
}
