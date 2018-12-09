using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using System.Web.UI.WebControls;

using System.Drawing;
using System.Net;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MovieTralier
{
    public partial class Index : System.Web.UI.Page
    {
        string[] separatingChars = {
                    "\":\"",
                    "\",\"",
                    "\":[{\"",
                    "\"},{\"",
                    "\"}]\"",
                    "{\"",
                    "\"}"
                };
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                
            }
        }

        protected void Buttonfind_Click(object sender, EventArgs e)
        {
            string searchstringUrlEncoded = HttpContext.Current.Server.UrlEncode(TextBoxInput.Text);

            WebClient client = new WebClient();
            string resultJson = "";
            string resultXml = "";

           
            resultJson = client.DownloadString("http://www.omdbapi.com/?t=" + searchstringUrlEncoded + "&apikey=" + TokenClass.token);
            File.WriteAllText(Server.MapPath("~/MyFiles/Latestresult.json"), resultJson);
            // A simple example. Treat json as a string
            string[] mysplit = resultJson.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);

            if (mysplit[1] != "False") // we found a movie :-)
            {
                LabelMessages.Text = "Movie found";

                for (int i = 0; i < mysplit.Length; i++)
                {
                    if (mysplit[i] == "Poster")
                    {
                        ImagePoster.ImageUrl = mysplit[i + 1];
                        break;
                    }
                }
                LabelResult.Text = "Ratings : ";
                for (int i = 0; i < mysplit.Length; i++)
                {
                    if (mysplit[i] == "Ratings")
                    {
                        while (mysplit[++i] == "Source")
                        {
                            if (mysplit[i - 1] != "Ratings") LabelResult.Text += "; ";
                            LabelResult.Text += mysplit[i + 3] + " from " + mysplit[i + 1];
                            i = i + 3;
                        }
                        break;
                    }
                }
            }
            else
            {
                LabelMessages.Text = "Movie not found";
                ImagePoster.ImageUrl = "~/MyFiles/SmileyHalo.png";
                LabelResult.Text = "Result";
            }
            
            resultXml = client.DownloadString("http://www.omdbapi.com/?t=" + searchstringUrlEncoded + "&r=xml&apikey=" + TokenClass.token);
            File.WriteAllText(Server.MapPath("~/MyFiles/Latestresult.xml"), resultXml);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(resultXml);

            if (doc.SelectSingleNode("/root/@response").InnerText == "True")
            {
                XmlNodeList nodelist = doc.SelectNodes("/root/movie");
                foreach (XmlNode node in nodelist)
                {
                    string id = node.SelectSingleNode("@poster").InnerText;
                    ImagePoster.ImageUrl = id;
                }
                LabelResult.Text = "Rating" + nodelist[0].SelectSingleNode("@imdbRating").InnerText;
                LabelResult.Text += " from " + nodelist[0].SelectSingleNode("@imdbVotes").InnerText + "votes";
            }
            else
            {
                LabelMessages.Text = "Movie not found";
                ImagePoster.ImageUrl = "~/MyFiles/SmileyHalo.png";
                LabelResult.Text = "Result";
            }
        }


        protected void ButtonPlayTralier_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            string result;
            //AIzaSyDOlG5Xa5jVLpd_NYEx63LV7PC6Pi4Ieeo
            var apiKey = "AIzaSyBv1JLAkCV4giMLrTUKBXfDI4HTIOIeqAo";
            string youTubeApi = $"https://www.googleapis.com/youtube/v3/search?&part=snippet&q={TextBoxInput.Text.Trim()}&type=tralier&key={apiKey}";
            result = client.DownloadString(youTubeApi);
            var movieSearchResult = JsonConvert.DeserializeObject<JObject>(result);

            var items = movieSearchResult["items"];
            var videoId = items[0]["id"]["videoId"];
            if (videoId.ToString() !="") { 
            youTubeTrailer.Src = $"https://www.youtube.com/embed/{videoId.ToString()}";
            LabelTralier.Text = "This movie tralier found";
            }
            else
            {
                LabelTralier.Text = "This movie tralier not found";
            }

        }
    }
}