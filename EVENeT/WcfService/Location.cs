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
    
    public partial class Location
    {
        public Location()
        {
            this.Events = new HashSet<Event>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string address { get; set; }
        public Nullable<float> longitude { get; set; }
        public Nullable<float> latitude { get; set; }
        public byte[] thumbnail { get; set; }
    
        public virtual ICollection<Event> Events { get; set; }
    }
}