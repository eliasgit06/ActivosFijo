//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ActivosFijo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblMovimiento
    {
        public int Id { get; set; }
        public Nullable<int> nTipo { get; set; }
        public string cDescripcion { get; set; }
        public Nullable<System.DateTime> dFechaMovimiento { get; set; }
        public Nullable<double> nMontoMovimiento { get; set; }
        public Nullable<System.DateTime> dFechaRegistro { get; set; }
        public string cUsuario { get; set; }
        public Nullable<int> IdActivo { get; set; }
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
}
