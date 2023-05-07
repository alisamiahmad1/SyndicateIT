using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.RoleManagement
{
    [Serializable]
    public class GridRoleManagementViewModel : ViewModelBase
    {

        #region Properties

        public string Id { get; set; }

        [Display(Name = "Name ")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }

        #endregion
    }
}
