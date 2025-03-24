namespace PhanTranMinhTam_TestLan2.Models
{
    public class MusicDTO
    {

        public string Title { get; set; }
        public IFormFile? FilePath { get; set; }
        public string MediaType { get; set; }
        public string Duration { get; set; }
    }
}
