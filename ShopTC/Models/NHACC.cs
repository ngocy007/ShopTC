//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopTC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NHACC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHACC()
        {
            this.ThuCung = new HashSet<ThuCung>();
        }
    
        public string MaCC { get; set; }
        public string TenCC { get; set; }
        public string DiaChiCC { get; set; }
        public string EmailCC { get; set; }
        public string SDTCC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThuCung> ThuCung { get; set; }
    }
}
