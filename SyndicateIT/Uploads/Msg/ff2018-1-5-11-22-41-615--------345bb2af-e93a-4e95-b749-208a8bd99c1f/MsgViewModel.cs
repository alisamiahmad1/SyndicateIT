using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SyndicateIT.Model.ViewModel.Msg
{
    public class MsgViewModel
    {
        public List<Guid> Recivers_IDs{ get; set; }
        public List<string> Msg_Attachement { get; set; }
        public bool isSender { get; set; } = false;
    }
}
