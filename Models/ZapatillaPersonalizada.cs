using System;
using System.ComponentModel.DataAnnotations;

namespace APIV22.Models
{
    public class ZapatillaPersonalizada
    {
        [Key]
        public int Id { get; set; }

        public string? NombreImagen { get; set; }

        [Required]
        public string? ColorSuperior { get; set; }

        [Required]
        public string? ColorSuela { get; set; }

        [Required]
        public string? ColorCordones { get; set; }

        [Required]
        public string? ColorPlantilla { get; set; }

        [Required]
        public string? Estilo { get; set; }

        [Required]
        public string? Suela { get; set; }

        [MaxLength(20)]
        public string? Texto { get; set; }

        [Range(35, 45)]
        public int Talla { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}
