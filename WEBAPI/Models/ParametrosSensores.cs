using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEBAPI.Models
{
    [Table("parametros_sensores")]
    public class ParametrosSensor
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("codigo_parametro")]
        [Required]
        public int CodigoParametro { get; set; }

        [Column("descripcion_larga")]
        [StringLength(255)]
        public string? DescripcionLarga { get; set; }

        [Column("descripcion_med")]
        [StringLength(255)]
        public string? DescripcionMed { get; set; }

        [Column("descripcion_corta")]
        [StringLength(255)]
        public string? DescripcionCorta { get; set; }

        [Column("abreviacion")]
        [StringLength(10)]
        public string? Abreviacion { get; set; }

        [Column("observacion")]
        public string? Observacion { get; set; }

        [Column("fecha_creacion")]
        public DateTime? FechaCreacion { get; set; }

        [Column("fecha_modificacion")]
        public DateTime? FechaModificacion { get; set; }

        [Column("estado")]
        [StringLength(1)]
        public string? Estado { get; set; }

        [Column("unidad")]
        [StringLength(50)]
        public string? Unidad { get; set; }

      
    }
}