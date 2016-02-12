using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Formatting;
using System.Web.Script.Serialization;
using GameChallenge.Models;
using Newtonsoft.Json;

namespace GameChallenge.Controllers
{
    public class PlayerController : Controller
    {
      
        //
        // GET: /Player/
        public ActionResult Index()
        {
            return View();
        } 

        public ActionResult beginPlayView ()
        {
            return View("playView");
        }

        public ActionResult about()
        {
            return View("About");
        }

        public ActionResult beginTopView()
        
        {
            TopTen();
            return View("topView");
        }

        //Methods that are related with the playing process
        public ActionResult Result(HttpPostedFileBase file)
        {
            Player player = new Player();
           
            try
            {
                if (Path.GetExtension(file.FileName).Equals(".txt"))
                {
                    int counter = 0;
                    string line;
                    string dataFile = "";
                    string path = Server.MapPath("~/Examples/" + file.FileName);
                    file.SaveAs(path);
                    StreamReader fileRead = new StreamReader(path);
                    while ((line = fileRead.ReadLine()) != null)
                    {
                        dataFile += line;
                        counter++;
                    }
                    fileRead.Close();

                    
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("http://localhost:2012/api/Tournament/GetWinner?dataFile=" + dataFile).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string dataResponse = response.Content.ReadAsStringAsync().Result;
                        JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                        var result = (IDictionary<string, object>)json_serializer.DeserializeObject(dataResponse);
                        player.name = result["name"].ToString();
                        player.strategy = result["strategy"].ToString();
                    }
                   

                }
                else
                {
                    throw new ApplicationException();
                }
            }
            catch (NullReferenceException)
            {
                player.name = "You did not upload a tournament file";
                player.strategy = "Please go back and upload a valid tournament file, remember the structure of the tournament or check the instructions";
                ;
            }
            catch (ApplicationException)
            {
                player.name = "Remember the extencion of the file must be txt";
                player.strategy = "";
            }

            return View(player);
        }

   
        //Methods that are related with the resetData process 

        public ActionResult resetData() 
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.DeleteAsync("http://localhost:2012/api/Tournament/resetTournament").Result;
            return View("topView");
        }

        public ActionResult TopTen()
        {
            List<Player> result = new List<Player>();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync("http://localhost:2012/api/Tournament/getTopTen").Result;
            if (response.IsSuccessStatusCode)
            {
                string dataResponse = response.Content.ReadAsStringAsync().Result;
                JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                var data = JsonConvert.DeserializeObject<List<Player>>(dataResponse.Trim());
                result = data;
            }
            return View(result);
        }
    }
}
