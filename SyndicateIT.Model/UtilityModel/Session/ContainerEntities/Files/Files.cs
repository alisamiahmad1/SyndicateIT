using SyndicateIT.Model.UtilityModel.Session.ContainerEntities;
using SyndicateIT.Model.ViewModel.FileManagement;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.UtilityModel.Session.ContainerEntities.Files
{
    /// <summary>
    /// Class UserManagement.
    /// </summary>
    [Serializable()]
    public class Files : SessionElement
    {
        #region Constant

        /// <summary>
        /// [ERROR: invalid expression FieldName.Words.TheAndAllAsSentence]
        /// </summary>
        public const string SESSION_KEY = "Files";

        #endregion

        #region Properties         

        public FileType FileType { get; set; }
        public FilesSearchViewModel FilesSearch { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public AlertType AlertType { get; set; }
        public List<GridFilesViewModel> ListFiles { get; set; }
        public bool IsLoadFilesContent { get; set; }
        public Guid CurrentFilesID { get; set; }
        public string CurrentFilesName { get; set; }

        #endregion
    }
}
