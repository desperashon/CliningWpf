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
    
    public partial class Employees
    {
        public Employees()
        {
            this.Schedules = new HashSet<Schedules>();
            this.Service_Request = new HashSet<Service_Request>();
        }
    
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Category { get; set; }
        public Nullable<int> CityID { get; set; }
        public string Photo { get; set; }
    
        public virtual City City { get; set; }
        public virtual ICollection<Schedules> Schedules { get; set; }
        public virtual ICollection<Service_Request> Service_Request { get; set; }
    }
}