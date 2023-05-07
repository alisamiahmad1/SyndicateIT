using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.UtilityModel.Session.ContainerEntities
{
    [Serializable()]
    public abstract class SessionElement : ISessionElement
    {
        #region Methods

        #region Virtual Methods      

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public virtual void Dispose()
        {
            this.Dispose();
        }

        #endregion

        #endregion
    }
}
