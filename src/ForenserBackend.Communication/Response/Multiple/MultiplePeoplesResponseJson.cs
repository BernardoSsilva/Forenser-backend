using ForenserBackend.Communication.Response.Short;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForenserBackend.Communication.Response.Multple
{
    public class MultiplePeoplesResponseJson
    {
        public List<ShortPeopleResponseJson> Peoples { get; set; } = [];
    }
}
