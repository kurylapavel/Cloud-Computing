using System;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace Ccpmobile.SteamAuth.dll
{
    public class TimeAligner
    {
        private static bool _aligned = false;
        private static int _timeDifference = 0;

        public static long GetSteamTime()
        {
            if (!TimeAligner._aligned)
            {
                TimeAligner.AlignTime();
            }
            return Util.GetSystemUnixTime() + _timeDifference;
        }
        public static void AlignTime()
        {
            long currentTime = Util.GetSystemUnixTime();
            using (WebClient client = new WebClient())
            {
                try
                {
                    string response = client.UploadString(APIEndpoints.TWO_FACTOR_TIME_QUERY, "steamid=0");
                    TimeQuery query = JsonConvert.DeserializeObject<TimeQuery>(response);
                    TimeAligner._timeDifference = (int)(query.Response.ServerTime - currentTime);
                    TimeAligner._aligned = true;
                }
                catch (WebException)
                {
                    return;
                }
            }
        }
        internal class TimeQuery
        {
            [JsonProperty("response")]
            internal TimeQueryResponse Response { get; set; }

            internal class TimeQueryResponse
            {
                [JsonProperty("server_time")]
                public long ServerTime { get; set; }
            }

        }
    }
}
