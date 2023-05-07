using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.UtilityModel.Session.ContainerEntities.Login
{
    [Serializable()]
    public class Login : SessionElement
    {
        #region Constant

        /// <summary>
        /// The sessio n_ key
        /// </summary>
        public const string SESSION_KEY = "Login";
        #endregion

        #region Properties   

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public Guid  UserID { get; set; }

        public string UserGuidID { get; set; }

        public string ProfileRole { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the biller identifier.
        /// </summary>
        /// <value>The biller identifier.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the name of the biller.
        /// </summary>
        /// <value>The name of the biller.</value>
        public string FirstName { get; set; }

        public string StudentType { get; set; }

        public string Message { get; set; }

        public string Result { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool ProfileCompleted { get; set; }


        public bool OneTimePass { get; set; } = false;


        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        public string UserRole { get; set; }

        public Helper.MenuPage MenuPage { get; set; }

        public bool IsChangeRequest { get; set; }    


        #endregion

        #region Overriden Methods       

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion

    }
}
