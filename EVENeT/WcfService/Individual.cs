//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfService
{
    using System;
    using System.Collections.Generic;
    
    public partial class Individual
    {
        public Individual()
        {
            this.Interests = new HashSet<Interest>();
        }
    
        public string username { get; set; }
        public string firstName { get; set; }
        public string midName { get; set; }
        public string lastName { get; set; }
        public System.DateTime DOB { get; set; }
        public bool gender { get; set; }
    
        public virtual User User { get; set; }
        public virtual ICollection<Interest> Interests { get; set; }
    }
}
