
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace University.Data
{

using System;
    using System.Collections.Generic;
    
public partial class ProductFAQVideos
{

    public decimal Id { get; set; }

    public decimal ProductFAQsId { get; set; }

    public string ImageURL { get; set; }

    public string VideoURL { get; set; }

    public Nullable<bool> IsDeleted { get; set; }

    public Nullable<System.DateTime> CreatedDate { get; set; }

    public Nullable<decimal> CreatedBy { get; set; }

    public Nullable<System.DateTime> DeletedDate { get; set; }

    public Nullable<decimal> DeletedBy { get; set; }

    public Nullable<System.DateTime> UpdatedDate { get; set; }

    public Nullable<decimal> UpdatedBy { get; set; }

    public Nullable<int> AssocitedID { get; set; }



    public virtual ProductFAQs ProductFAQs { get; set; }

}

}