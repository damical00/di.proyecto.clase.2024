﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace di.proyecto.clase._2024.Backend.Modelo;

[Table("articulo")]
[Index("Departamento", Name = "fk_departamentos_articulo_idx")]
[Index("Espacio", Name = "fk_espacios_articulo_idx")]
[Index("Dentrode", Name = "fk_estaen_articulo_idx")]
[Index("Modelo", Name = "fk_modelos_articulo_idx")]
[Index("Usuarioalta", Name = "fk_usuarioalta_articulo_idx")]
[Index("Usuariobaja", Name = "fk_usuariobaja_modeloarticulo_idx")]
public partial class Articulo
{
    [Key]
    [Column("idarticulo")]
    public int Idarticulo { get; set; }

    [Column("numserie")]
    [StringLength(45, ErrorMessage ="No está permitido sobrepasar los 45 carácteres")]
    [Required(ErrorMessage = "El campo número de serie no puede estar vacío")]
    public string? Numserie { get; set; }

    [Column("estado")]
    [StringLength(45)]
    [Required(ErrorMessage ="El campo estado no puede estar vacío")]
    public string? Estado { get; set; }

    /// <summary>
    /// fecha en que se introdujo en el sistema
    /// </summary>
    [Column("fechaalta", TypeName = "date")]
    [Required(ErrorMessage = "El campo fecha de alta no puede estar vacío")]
    public DateTime? Fechaalta { get; set; }

    [Column("fechabaja", TypeName = "date")]
    public DateTime? Fechabaja { get; set; }

    [Column("usuarioalta")]
    public int? Usuarioalta { get; set; }

    /// <summary>
    /// usuario que lo dio de baja
    /// </summary>
    [Column("usuariobaja")]
    public int? Usuariobaja { get; set; }

    [Column("modelo")]
    [Required(ErrorMessage = "El campo modelo no puede estar vacío")]
    public int Modelo { get; set; }

    /// <summary>
    /// departamento al que pertenece o del que depende
    /// </summary>
    [Column("departamento")]
    [Required]
    public int? Departamento { get; set; }

    /// <summary>
    /// espacio en que se encuentra
    /// </summary>
    [Column("espacio")]
    [Required]
    public int Espacio { get; set; }

    /// <summary>
    /// Indica que este artículo forma parte de otro
    /// </summary>
    [Column("dentrode")]
    public int? Dentrode { get; set; }

    [Column("observaciones", TypeName = "mediumtext")]
    public string? Observaciones { get; set; }

    [ForeignKey("Dentrode")]
    [InverseProperty("InverseDentrodeNavigation")]
    public virtual Articulo? DentrodeNavigation { get; set; }

    [ForeignKey("Departamento")]
    [InverseProperty("Articulos")]
    public virtual Departamento? DepartamentoNavigation { get; set; }

    [ForeignKey("Espacio")]
    [InverseProperty("Articulos")]
    public virtual Espacio EspacioNavigation { get; set; } = null!;

    [InverseProperty("DentrodeNavigation")]
    public virtual ICollection<Articulo> InverseDentrodeNavigation { get; set; } = new List<Articulo>();

    [ForeignKey("Modelo")]
    [InverseProperty("Articulos")]
    public virtual Modeloarticulo ModeloNavigation { get; set; } = null!;

    [InverseProperty("ArticuloNavigation")]
    public virtual ICollection<Salidum> Salida { get; set; } = new List<Salidum>();

    [ForeignKey("Usuarioalta")]
    [InverseProperty("ArticuloUsuarioaltaNavigations")]
    public virtual Usuario? UsuarioaltaNavigation { get; set; }

    [ForeignKey("Usuariobaja")]
    [InverseProperty("ArticuloUsuariobajaNavigations")]
    public virtual Usuario? UsuariobajaNavigation { get; set; }
}
