using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ActivosFijo.Models
{
    public class PartialMetaData
    {
    }

    [MetadataType(typeof(PlaneMetaData))]
    public partial class Plane
    {
    }

    [MetadataType(typeof(ActivosMetaData))]
    public partial class TblActivo
    {
    }

    [MetadataType(typeof(OrigenMetaData))]
    public partial class TblOrigene
    {
    }

    [MetadataType(typeof(CategoriasMetaData))]
    public partial class TblCategoria
    {
    }

    [MetadataType(typeof(LocacionesMetaData))]
    public partial class TblLocacione
    {
    }

    [MetadataType(typeof(MovimientosMetaData))]
    public partial class TblMovimiento
    {
    }

    [MetadataType(typeof(PersonaMetaData))]
    public partial class TblPersona
    {
    }

    [MetadataType(typeof(UsuarioMetaData))]
    public partial class TblUsuario
    {
    }

}