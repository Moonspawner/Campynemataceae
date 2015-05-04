using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Runtime.InteropServices;
using TkParser;

namespace TkClient
{
    public partial class Client
    {
        #region Net

        private static readonly string[] RessourceProvider = { "http://jfds.eu/tkbrowser/{0}.tk", "http://jfds.eu/tkbrowser/{0}.html", "http://tk.steph.cf/{0}.tk" };
        private static readonly string[] NameServer = { "http://tk.steph.cf/dns.php?name={0}", "http://jfds.eu/tkbrowser/dns.php?name={0}" };

        public static StreamReader GetStreamForFile(string fileIdentifier)
        {
            var localpath = (Path.Combine("sites", fileIdentifier + ".tk"));
            if (File.Exists(localpath)) { return new StreamReader(localpath); }
            var url = GetURLForFileIdentifier(fileIdentifier);
            return !url.StartsWith("http://") ? new StreamReader(url) : new StreamReader(WebRequest.Create(url).GetResponse().GetResponseStream(), Encoding.UTF8);
        }

        private static string GetURLForFileIdentifier(string fileIdentifier)
        {
            //trying the url as it is
            try {
                if (((HttpWebResponse)WebRequest.Create(fileIdentifier).GetResponse()).StatusCode == HttpStatusCode.OK) {
                    return fileIdentifier;
                }
            } catch (Exception e) {
                Console.WriteLine("\"{0}\" occured while trying to access ressource at {1}, trying...", e.Message, fileIdentifier);
            }

            //trying one of the usual locations for shorter names
            foreach (var ressource in RessourceProvider) {
                try {
                    if (((HttpWebResponse)WebRequest.Create(string.Format(ressource, fileIdentifier)).GetResponse()).StatusCode == HttpStatusCode.OK) {
                        return string.Format(ressource, fileIdentifier);
                    }
                } catch (Exception e) {
                    Console.WriteLine("\"{0}\" occured while trying to access ressource at {1}, trying...", e.Message, string.Format(ressource, fileIdentifier));
                }
            }

            //trying the nameserver-approach
            var serveralias = fileIdentifier.Split('/')[0];
            var filename = fileIdentifier.Substring(Math.Min(serveralias.Length + 1, fileIdentifier.Length));
            var server = "";
            foreach (var nameServer in NameServer) {
                server = new WebClient().DownloadString(string.Format(nameServer, serveralias)).Trim(' ', '\n', '\r');
                if (server != "") { break; }
            }

            if (!server.StartsWith("http://")) { server = "http://" + server; }
            var resolvedAddress = Path.Combine(server, filename).Replace('\\', '/');

            string requestAddress = null;
            foreach (var appendage in new[] { "", ".tk", "/index.tk" }) {
                try {
                    var siteRequest = ((HttpWebResponse)WebRequest.Create(resolvedAddress + appendage).GetResponse());
                    var siteContent = new StreamReader(siteRequest.GetResponseStream()).ReadToEnd();
                    if (!Regex.IsMatch(siteContent, "Apache.*at.*Port", RegexOptions.Compiled | RegexOptions.Singleline)) {
                        requestAddress = resolvedAddress + appendage;
                        break;
                    }
                } catch (Exception e) {
                    Console.WriteLine(e);
                }
            }

            return requestAddress ?? "sites/tknext_error.tk";

            ////this exception should be passed to the tknext_error.tk when scripting is available
            //throw new FileNotFoundException("No Ressource providing '{0}' coule be reached", fileIdentifier);
        }

        #endregion
    }
}
