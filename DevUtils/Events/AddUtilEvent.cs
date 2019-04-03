using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilModelService;

namespace DevUtils.Events
{
    public class AddUtilEvent :PubSubEvent<IUtilModel>
    {
    }
}
