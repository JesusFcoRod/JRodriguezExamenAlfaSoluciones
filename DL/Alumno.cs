//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Alumno
    {
        public int IdAlumno { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public Nullable<int> Edad { get; set; }
        public Nullable<int> IdBeca { get; set; }
    
        public virtual Beca Beca { get; set; }
    }
}
