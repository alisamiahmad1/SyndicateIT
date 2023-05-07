using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SyndicateIT.Model.ViewModel
{
    /// <summary>
    /// Class UsersContext.
    /// </summary>
    public class UsersContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersContext" /> class.
        /// </summary>
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        /// <summary>
        /// Gets or sets the ASP net users.
        /// </summary>
        /// <value>The ASP net users.</value>
        public DbSet<AspNetUsers> AspNetUsers { get; set; }
    }

    /// <summary>
    /// Class AspNetUsers.
    /// </summary>
    [Table("AspNetUsers")]
    public class AspNetUsers
    {

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [email confiremd].
        /// </summary>
        /// <value><c>true</c> if [email confiremd]; otherwise, <c>false</c>.</value>
        public bool EmailConfiremd { get; set; }

        /// <summary>
        /// Gets or sets the password hash.
        /// </summary>
        /// <value>The password hash.</value>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the security stam.
        /// </summary>
        /// <value>The security stam.</value>
        public string SecurityStam { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>The phone number.</value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [phone number confirmed].
        /// </summary>
        /// <value><c>true</c> if [phone number confirmed]; otherwise, <c>false</c>.</value>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [two factor enabled].
        /// </summary>
        /// <value><c>true</c> if [two factor enabled]; otherwise, <c>false</c>.</value>
        public bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// Gets or sets the lockout end date UTC.
        /// </summary>
        /// <value>The lockout end date UTC.</value>
        public DateTime LockoutEndDateUtc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [lockout enabled].
        /// </summary>
        /// <value><c>true</c> if [lockout enabled]; otherwise, <c>false</c>.</value>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets the access failed count.
        /// </summary>
        /// <value>The access failed count.</value>
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>The creation date.</value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the Branch.
        /// </summary>
        /// <value>The Branch.</value>
        public string Branch { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

}
