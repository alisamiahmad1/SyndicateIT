using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.UtilityModel
{
    [Serializable]
    public class EmailSettings
    {
        #region Properties

        /// <summary>
        /// Gets or sets the sender.
        /// </summary>
        /// <value>The sender.</value>
        public string Sender { get; set; }

        /// <summary>
        /// Gets or sets the recipient list.
        /// </summary>
        /// <value>The recipient list.</value>
        public List<string> RecipientList { get; set; }

        /// <summary>
        /// Gets or sets the email subject.
        /// </summary>
        /// <value>The email subject.</value>
        public string EmailSubject { get; set; }

        /// <summary>
        /// Gets or sets the email body.
        /// </summary>
        /// <value>The email body.</value>
        public string EmailBody { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [send separate emails].
        /// </summary>
        /// <value><c>true</c> if [send separate emails]; otherwise, <c>false</c>.</value>
        public bool SendSeparateEmails { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSettings"/> class.
        /// </summary>
        public EmailSettings()
        {
            RecipientList = new List<string>();
            Sender = "";
        }

        #endregion
    }
}
