using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace SeleniumExtension.SauceLabs
{
    public class Job
    {
        //"Content-Type:text/json" -s -X PUT -d  


        public static void UpDateJob(string job, bool passed)
        {

            var url = string.Format("http://{0}:{1}@saucelabs.com/rest/v1/{0}/jobs/{2}",
                                    Constants.SAUCE_LABS_ACCOUNT_NAME, Constants.SAUCE_LABS_ACCOUNT_KEY, job);
            var data = string.Format("{\"passed\": " + passed.ToString().ToLower() + "}");
            using (var client = new WebClient())
            {
                client.UploadString(url, "PUT", data);
            }
            //try
            //{
            //    var request = WebRequest.Create(requestUrl) as HttpWebRequest;
            //    request.Method = "put";
            //    var response = request.GetResponse() as HttpWebResponse;
            //    var xmlDoc = new XmlDocument();
            //    xmlDoc.Load(response.GetResponseStream());
            //    return (xmlDoc);
            //}
            //catch (Exception e)
            //{
            //    return null;
            //}
        }
    }
}
