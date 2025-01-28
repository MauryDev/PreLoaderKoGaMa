using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreLoaderKoGaMa.Services
{
    public interface IPlugin
    {
        void Init(ServiceManager serviceManager);
    }
}
