using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MegaCasting.Startup1))]

namespace MegaCasting
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // Pour plus d'informations sur la façon de configurer votre application, consultez http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
