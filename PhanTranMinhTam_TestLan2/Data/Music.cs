using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhanTranMinhTam_TestLan2.Data
{
    public class Music
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MediaId { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public string MediaType { get; set; }
        public TimeSpan Duration { get; set; }
        public ICollection<PlaySchedule> Schedules { get; set; }
    }
}
