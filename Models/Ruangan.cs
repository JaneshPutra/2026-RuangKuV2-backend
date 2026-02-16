using System.ComponentModel.DataAnnotations;

namespace RuangKuApi.Models
{
    public class Ruangan
    {
        public int Id { get; set; }
        public string NamaRuangan { get; set; } = string.Empty;
        public int Kapasitas { get; set; }
        public string Lokasi { get; set; } = string.Empty;
        public string Status { get; set; } = "Available"; 
    }
}