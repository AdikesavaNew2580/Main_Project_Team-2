//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Employee_Travel_Booking_System_WebApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public employee()
        {
            this.travelrequests = new HashSet<travelrequest>();
        }
    
        public int employeeid { get; set; }
        public string emp_name { get; set; }
        public string email { get; set; }
        public string emp_password { get; set; }
        public string department { get; set; }
        public string position { get; set; }
        public Nullable<System.DateTime> hiredate { get; set; }
        public string phonenumber { get; set; }
        public string address { get; set; }
        public Nullable<int> managerid { get; set; }
        public Nullable<bool> status { get; set; }
    
        public virtual manager manager { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<travelrequest> travelrequests { get; set; }
    }
}
