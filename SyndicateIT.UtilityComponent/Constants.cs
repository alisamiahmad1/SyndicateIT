using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.UtilityComponent
{
    [Serializable()]
    public static class Constants
    {

        /// <summary>
        /// The dateformat  ie:01-September-2016.
        /// </summary>
        public const string DATEFORMAT = "dd-MMMM-yyyy";


        /// <summary>
        /// The longdateformat ie:Thursday, 01 December, 2016.
        /// </summary>
        public const string LONGDATEFORMAT = "dddd, dd MMMM, yyyy";

        /// <summary>
        /// The daymonthformat  ie:01 September
        /// </summary>
        public const string DAYMONTHFORMAT = "dd MMMM";

        /// <summary>
        /// The summarydateformat
        /// </summary>
        public const string SUMMARYDATEFORMAT = "ddd.MMM.d";

        /// <summary>
        /// The timeformat
        /// </summary>
        public const string TIMEFORMAT = "HH:mm";

        /// <summary>
        /// The h HMM
        /// </summary>
        public const string HHmm = "HHmm";

        /// <summary>
        /// The longtimeformat
        /// </summary>
        public const string LONGTIMEFORMAT = "hh:mm tt";

        /// <summary>
        /// The datetimeformat
        /// </summary>
        public const string DATETIMEFORMAT = "dd-MMMM-yyyy HH:mm";

        /// <summary>
        /// The datemonthyearformat
        /// </summary>
        public const string DATEMONTHYEARFORMAT = "MMMM yyyy";

        /// <summary>
        /// The na
        /// </summary>
        public const string NA = "N/A";

        /// <summary>
        /// The no t_ applicable
        /// </summary>
        public const string NOT_APPLICABLE = "Not Applicable";

        /// <summary>
        /// The profilemageuploaddir
        /// </summary>
        public const string PROFILEMAGEUPLOADDIR = "Profiles";

        /// <summary>
        /// The defaulttravelgroupimage
        /// </summary>
        public const string DEFAULTTRAVELGROUPIMAGE = "profile-pic.png";

        /// <summary>
        /// The imagewidth
        /// </summary>
        public const int IMAGEWIDTH = 190;

        /// <summary>
        /// The imageheight
        /// </summary>
        public const int IMAGEHEIGHT = 215;

        /// <summary>
        /// The imagemaxuploadsize
        /// </summary>
        public const int IMAGEMAXUPLOADSIZE = 1;
        /// <summary>
        /// The connection string
        /// </summary>
        public const string ConnectionString = @"Data Source={0};Initial Catalog={1};Integrated Security=false;User ID={2};Password={3};MultipleActiveResultSets=True";

        /// <summary>
        /// The entity context
        /// </summary>
        public const string EntityContext = "metadata=res://*/DataContext.{0}.csdl|res://*/DataContext.{0}.ssdl|res://*/DataContext.{0}.msl;provider=System.Data.SqlClient;provider connection string=\"{1}\"";

        /// <summary>
        /// The MMM myyyy
        /// </summary>
        public const string MMMMyyyy = "MMMM yyyy";

        /// <summary>
        /// The MMMM
        /// </summary>
        public const string MMMM = "MMMM";

        /// <summary>
        /// The yyyy
        /// </summary>
        public const string YYYY = "yyyy";

        /// <summary>
        /// The dd mmmmyyyy
        /// </summary>
        public const string ddMMMMYYYY = "dd-MMMM-yyyy";

        /// <summary>
        /// The lebanes e_ currency
        /// </summary>
        public const string LEBANESE_CURRENCY = "LBP";
        /// <summary>
        /// The us a_ currency
        /// </summary>
        public const string USA_CURRENCY = "USD";

        /// <summary>
        /// The sessioncontentname
        /// </summary>
        public const string SESSIONCONTENTNAME = "SessionContent";

        /// <summary>
        /// The pagesize
        /// </summary>
        public const int PAGESIZE = 10;



    }
}
