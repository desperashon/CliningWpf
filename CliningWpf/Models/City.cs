//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CliningWpf.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class City
    {
        public City()
        {
            this.Employees = new HashSet<Employees>();
        }
    
        public int CityID { get; set; }
        public string CityName { get; set; }
    
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
