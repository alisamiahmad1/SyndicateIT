using SyndicateIT.Model.UtilityModel.Session;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.ProfileManagement.ProfileDetails
{
    [Serializable]
    public class JoinCourcesViewModel : ViewModelBase
    {
        #region Properties 

        public string Cycle { get; set; }

        public int CycleId { get; set; }

        public List<JoinClass> JoinClass { get; set; }
       
        #endregion
    }

    public class  JoinClass
    {
        public string Class{ get; set; }

        public Guid ClassID { get; set; }

        public List<JoinDivision> JoinDivision { get; set; }
    }


    public class JoinDivision
    {
       public string Division { get; set; }

        public int DivisionID { get; set; }

        public List<JoinCourse> JoinCourse { get; set; }
    }

    public class JoinCourse
    {
       public string Course { get; set; }

        public Guid CourseID { get; set; }

        public int JoinCourcesId { get; set; }

    }

}
