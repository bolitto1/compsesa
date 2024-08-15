using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEBAPI.Models
{
    [Table("datos_sensores")]
    public class DatosSensor
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("codigo_parametro")]
        [Required] // Si es un campo obligatorio
        public int CodigoParametro { get; set; }

        [Column("parametro_sensores_id")]
        [Required] // Si es un campo obligatorio
        public int ParametroSensoresId { get; set; }

        [Column("nombre_parametro")]
        [StringLength(20)] // Limitar la longitud
        public string NombreParametro { get; set; }

        [Column("fecha_dato")]
        public DateTime FechaDato { get; set; }

        [Column("valor_numero")]
        public decimal ValorNumero { get; set; }

        // Navegación a la tabla de parámetros de sensores
        [ForeignKey("CodigoParametro")]
        public virtual ParametrosSensor ParametrosSensor { get; set; }

        [ForeignKey("ParametroSensoresId")]
        public virtual ParametrosSensor ParametrosSensorReferencia { get; set; }
    }
}