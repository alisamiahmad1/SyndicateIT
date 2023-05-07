
using AutoMapper;
using SyndicateIT.Model.UtilityModel.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace SyndicateIT.Model.DomainModel
{
    [Serializable()]
    public abstract class DomainModelBase : IDomainModelBase
    {
        #region Public Methods      


        #region Date Time             

        public DateTime GetDateTimeUTC(DateTime source)
        {
            DateTime destinationDate = source;
            if (source.ToString() != "1/1/0001 12:00:00 AM")
            {
                TimeSpan offset = TimeZone.CurrentTimeZone.GetUtcOffset(new DateTime());
                int hours = offset.Hours * (-1);
                destinationDate = destinationDate.AddHours(hours);
            }
            return destinationDate;
        }

        public DateTime GetDateTimeWithUTC(DateTimeOffset source)
        {
            TimeSpan offset = source.Offset;
            int hours = offset.Hours * (-1);
            DateTime destinationDate = source.DateTime;
            destinationDate = destinationDate.AddHours(hours);

            return destinationDate;
        }

        #endregion

        public virtual void InitializeMapper()
        {
            Mapper.AssertConfigurationIsValid();

        }
   
        public virtual void Dispose()
        {
            this.Dispose();
        }

        #endregion
    
        public static string IsAuthenticated()
        {
            string message = string.Empty;


            if (SessionContent.Container.LoginAPI.UserID == null)
            {
                message = "l";
            }


            return message;
        }
    }
}
