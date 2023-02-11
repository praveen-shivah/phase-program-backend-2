namespace DatabaseContext
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


    public partial class Organization : BaseEntity
    {
        public Organization()
        {
            Address = new HashSet<Address>();
            City = new HashSet<City>();
            Contact = new HashSet<Contact>();
            Country = new HashSet<Country>();
            Invoice = new HashSet<Invoice>();
            InvoiceLineItem = new HashSet<InvoiceLineItem>();
            InvoiceRevision = new HashSet<InvoiceRevision>();
            PhoneNumber = new HashSet<PhoneNumber>();
            PostalCode = new HashSet<PostalCode>();
            Reseller = new HashSet<Reseller>();
            SiteInformation = new HashSet<SiteInformation>();
            StateProvince = new HashSet<StateProvince>();
            TransferPointsQueue = new HashSet<TransferPointsQueue>();
            User = new HashSet<User>();
            VendorCredentialsByOrganizations = new HashSet<VendorCredentialsByOrganizations>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [Column("URL")]
        public string Url { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string Password { get; set; } = null!;
        [Column("APIKey")]
        public string Apikey { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        [InverseProperty("Organization")]
        public virtual ICollection<Address> Address { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<City> City { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<Contact> Contact { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<Country> Country { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<Invoice> Invoice { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<InvoiceLineItem> InvoiceLineItem { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<InvoiceRevision> InvoiceRevision { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<PhoneNumber> PhoneNumber { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<PostalCode> PostalCode { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<Reseller> Reseller { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<SiteInformation> SiteInformation { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<StateProvince> StateProvince { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<TransferPointsQueue> TransferPointsQueue { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<User> User { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<VendorCredentialsByOrganizations> VendorCredentialsByOrganizations { get; set; }
    }
}
