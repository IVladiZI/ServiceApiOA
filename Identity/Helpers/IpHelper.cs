using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Helpers
{
    /// <IpHelper>
    /// With this method we bring the Ip of the requesting client to the service.
    /// </IpHelper>
    public class IpHelper
    {
        /// <GetIpAddress>
        /// Con este metodo traemos la Ip del cliente solicitante al servicio
        /// </GetIpAddress>
        /// <returns> IP </returns>
        public static string GetIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return string.Empty;
        }
    }
}
