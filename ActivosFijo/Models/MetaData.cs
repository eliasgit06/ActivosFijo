using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ActivosFijo.Models
{
    public class MetaData
    {
    }

    public class PlaneMetaData
    {
        [Display(Name = "Número")]
        public int Id { get; set; }

        [Display(Name = "Nombre del Plan")]
        [Required(ErrorMessage = "El Plan es requerido!")]
        [StringLength(5)]
        public string Plan { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Nombre de Usuario")]
        public string RegistradoPor { get; set; }

        [Display(Name = "Fecha de Registro")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaRegistro { get; set; }
    }

    public class OrigenMetaData
    {
        [Display(Name = "Número")]
        public int Id { get; set; }

        [Display(Name = "Origen")]
        public string cDescripcion { get; set; }
    }

    public class ActivosMetaData
    {
        [Display(Name = "Número")]
        public int Id { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string cDescripcion { get; set; }

        [Display(Name = "Detalle")]
        public string cDetalle { get; set; }

        [Display(Name = "Origen")]
        public Nullable<int> IdOrigen { get; set; }

        [Display(Name = "Categoría")]
        public Nullable<int> IdCategoria { get; set; }

        [Display(Name = "Ubicación")]
        public Nullable<int> IdLocacion { get; set; }

        [Display(Name = "Tipo")]
        public Nullable<int> IdTipoActivo { get; set; }

        [Display(Name = "Valor")]
        public Nullable<double> nValor { get; set; }

        [Display(Name = "Depreciación")]
        public Nullable<double> nDepreciacion { get; set; }

        [Display(Name = "Valor en libros")]
        public Nullable<double> nValorLibro { get; set; }

        [Display(Name = "Estatus")]
        public Nullable<int> cEstatus { get; set; }

        [Display(Name = "Fecha Adquirido")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Favor de confirmar la fecha de adquisición")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> dFechaActivo { get; set; }

        [Display(Name = "Cuenta")]
        public string cCuentaContable { get; set; }

        [Display(Name = "Asignación")]
        public string cAsignacion { get; set; }

        [Display(Name = "Fecha Registro")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> dfechaRegistro { get; set; }

        [Display(Name = "Usuario")]
        public string cUsuario { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage = "La marca es obligatoria")]
        public string cMarca { get; set; }

        [Display(Name = "Modelo")]
        public string cModelo { get; set; }

        [Display(Name = "No. Serial")]
        public string cNoSerial { get; set; }

        [Display(Name = "Código Origen")]
        public string codigoOrigen { get; set; }

        [Display(Name = "Código BN")]
        public string codigoBN { get; set; }

        [Display(Name = "Fecha Actualización")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaActualizacion { get; set; }

        [Display(Name = "Actualizado Por")]
        public string ActualizadoPor { get; set; }

        [Display(Name = "Asignado a:")]
        public Nullable<int> IdPersona { get; set; }

        [Display(Name = "Categoría")]
        public virtual TblCategoria TblCategoria { get; set; }

        [Display(Name = "Ubicaciones")]
        public virtual TblLocacione TblLocacione { get; set; }

        [Display(Name = "Origenes")]
        public virtual TblOrigene TblOrigene { get; set; }

        [Display(Name = "Tipos Activos")]
        public virtual TblTiposActivo TblTiposActivo { get; set; }

        [Display(Name = "Personas")]
        public virtual TblPersona TblPersona { get; set; }
    }

    public class CategoriasMetaData
    {
        [Display(Name = "Número")]
        public int Id { get; set; }

        [Display(Name = "Categoría")]
        public string cDescripcion { get; set; }

        [Display(Name = "Estatus")]
        public string cEstatus { get; set; }
    }

    public class LocacionesMetaData
    {
        [Display(Name = "Número")]
        public int Id { get; set; }

        [Display(Name = "Ubicación")]
        public string cDescripcion { get; set; }
    }
    public class PersonaMetaData
    {
        [Display(Name = "Número")]
        public int Id { get; set; }

        [Display(Name = "Documento")]
        public string cDocumento { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido")]
        public string cNombre { get; set; }

        [Display(Name = "Apellido")]
        public string cApellido { get; set; }
    }
    public class MovimientosMetaData
    {
        [Display(Name = "Número")]
        public int Id { get; set; }

        [Display(Name = "Tipo")]
        public Nullable<int> nTipo { get; set; }

        [Display(Name = "Descripción")]
        public string cDescripcion { get; set; }

        [Display(Name = "Fecha de Movimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> dFechaMovimiento { get; set; }

        [Display(Name = "Monto de Movimiento")]
        public Nullable<double> nMontoMovimiento { get; set; }

        [Display(Name = "Fecha de Registro")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> dFechaRegistro { get; set; }

        [Display(Name = "Usuario")]
        public string cUsuario { get; set; }

        [Display(Name = "Número del Activo")]
        public Nullable<int> IdActivo { get; set; }

        [Display(Name = "Asidnado A")]
        public string cAsignacion { get; set; }

        public Nullable<int> IdLocacion { get; set; }
        public Nullable<int> IdTipoActivo { get; set; }
        public Nullable<int> IdTipoBaja { get; set; }
        public Nullable<int> IdCategoria { get; set; }
        public Nullable<int> IdPersona { get; set; }

        public virtual TblCategoria TblCategoria { get; set; }
        public virtual TblLocacione TblLocacione { get; set; }
        public virtual TblTipoBaja TblTipoBaja { get; set; }
        public virtual TblTiposActivo TblTiposActivo { get; set; }
        public virtual TblPersona TblPersona { get; set; }
    }

    public class UsuarioMetaData
    {
        [Display(Name = "Número")]
        public int Id { get; set; }

        [Display(Name = "Usuario")]
        public string cUsuario { get; set; }

        [Display(Name = "Clave")]
        [DataType(DataType.Password)]
        public string cClave { get; set; }
        [Display(Name = "Nombre")]
        public string cNombre { get; set; }

        [Display(Name = "Rol")]
        public string rol { get; set; }
    }
}