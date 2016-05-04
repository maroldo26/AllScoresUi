using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace AllScoresUI.Services
{
    public class AllScoreService
    {
        private AllScoreService()
        {

        }

        private static AllScoreService instance;

        public static AllScoreService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AllScoreService();
                }
                return instance;
            }
        }

        public string GetTable(int leagueid=1204)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    return webClient.DownloadString(string.Format("http://localhost:60467/api/score/gettable/{0}", leagueid));
                }
                catch (WebException e)
                {

                    var stream = e.Response.GetResponseStream();
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        var message = reader.ReadToEnd();
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(message),
                            ReasonPhrase = message
                        };
                        throw new HttpResponseException(resp);
                    }                    
                }                
            }
        }

        public string GetNextSevenDayFixtures(int id)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    //http://localhost:63823/api/score/getfixtures?competitionid=1204&fromdate=13.12.2015&enddate=15.12.2015
                    DateTime fromdate, enddate;
                    fromdate = DateTime.Today;
                    enddate = fromdate.AddDays(7);
                    return webClient.DownloadString(string.Format("http://localhost:60467/api/score/getfixtures?competitionid={0}&fromdate={1}&enddate={2}", id, 
                        fromdate.ToString("d.M.yyyy"), 
                        enddate.ToString("d.M.yyyy")));
                }
                catch (WebException e)
                {

                    var stream = e.Response.GetResponseStream();
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        var message = reader.ReadToEnd();
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(message),
                            ReasonPhrase = message
                        };
                        throw new HttpResponseException(resp);
                    }
                }
            }
        }

        internal string GetLeagues()
        {
            using (WebClient webClient = new WebClient())
            {
                return webClient.DownloadString(string.Format("http://localhost:60467/api/score/gettounaments"));
            }
        }
    }
}