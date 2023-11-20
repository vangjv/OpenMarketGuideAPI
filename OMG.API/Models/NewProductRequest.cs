namespace OMG.API.Models
{
    public class NewProductRequest
    {
        public IFormFile File { get; set; }
        public string ProductJson { get; set; } //json
    }
}
