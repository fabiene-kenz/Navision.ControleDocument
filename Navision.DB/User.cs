namespace Navision.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] timestamp { get; set; }

        [Key]
        [Column("User Security ID")]
        public Guid User_Security_ID { get; set; }

        [Column("User Name")]
        [Required]
        [StringLength(50)]
        public string User_Name { get; set; }

        [Column("Full Name")]
        [Required]
        [StringLength(80)]
        public string Full_Name { get; set; }

        public int State { get; set; }

        [Column("Expiry Date")]
        public DateTime Expiry_Date { get; set; }

        [Column("Windows Security ID")]
        [Required]
        [StringLength(119)]
        public string Windows_Security_ID { get; set; }

        [Column("Change Password")]
        public byte Change_Password { get; set; }

        [Column("License Type")]
        public int License_Type { get; set; }

        [Column("Authentication Email")]
        [Required]
        [StringLength(250)]
        public string Authentication_Email { get; set; }

        [Column("Contact Email")]
        [Required]
        [StringLength(250)]
        public string Contact_Email { get; set; }

        [Column("Exchange Identifier")]
        [Required]
        [StringLength(250)]
        public string Exchange_Identifier { get; set; }

        [Column("Application ID")]
        public Guid Application_ID { get; set; }
    }
}
