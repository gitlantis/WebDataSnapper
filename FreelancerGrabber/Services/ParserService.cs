using FreelancerGrabber.Models;
using FreelancerGrabber.Services.Interfaces;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerGrabber.Services
{
    public class WeblancerService : IWeblancerService
    {
        public Task<dynamic> UploadUrl(int start = 1, int end = 0)
        {

            int pages = 0;
            if (end == 0) pages = GetLastPage();
            else pages = end;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var enc1252 = Encoding.GetEncoding("Windows-1251");
            var htmlWeb = new HtmlWeb();

            htmlWeb.OverrideEncoding = enc1252;

            for (int i = start; i < pages; i++)
            {
                var baseUrl = "https://www.weblancer.net/freelancers/";
                if (i > 1) baseUrl += "?page=" + i;
                var linkHTML = htmlWeb.Load(baseUrl);

                var nodes = linkHTML.DocumentNode.SelectNodes("//div[@class='cols_table divided_rows freelancers']//div[@class='row']");

                foreach (var node in nodes)
                {
                    using (var db = new WeblancerDbContext())
                    {
                        var login = node.SelectSingleNode("//div[@class='user_brief']").Attributes["data-login"].Value;
                        var userUrl = "https://www.weblancer.net/users/" + login + "/";

                        var urlExist = db.DevtoCandidates.Where(c => c.Url == userUrl).FirstOrDefault();

                        var urlData = new DevtoCandidates();
                        urlData.Url = userUrl;

                        if (urlExist == null)
                        {
                            db.DevtoCandidates.Add(urlData);
                            db.SaveChanges();
                            Console.WriteLine(urlData.Url);
                        }
                    }


                }

            }

            return null;
        }

        public void DataLoader(int start = 0, int length = 0)
        {

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var enc1252 = Encoding.GetEncoding("Windows-1251");
            var htmlWeb = new HtmlWeb();

            List<DevtoCandidates> devs = null;
            using (var db = new WeblancerDbContext())
            {
                if (start == 0 && length == 0)
                    devs = db.DevtoCandidates.AsNoTracking().ToList();
                else
                    devs = db.DevtoCandidates.AsNoTracking().Where(v => v.Id >= start).Take(length).ToList();
            }
            foreach (var c in devs)
            {

                var linkHTML = htmlWeb.Load(c.Url);
                var login = linkHTML.DocumentNode.SelectSingleNode("//div[@class='user_brief']").Attributes["data-login"].Value;
                var breif = linkHTML.DocumentNode.SelectSingleNode("//div[@class='user_brief']//div[@class='brief']").InnerText;

                var weblancer = new WeblancerCandidates();
                weblancer.Url_Id = c.Id;
                weblancer.Login = login;
                weblancer.Cont_Content = breif;

                List<WeblancerCandidates> workerExist = null;
                using (var db = new WeblancerDbContext())
                {
                    workerExist = db.WeblancerCandidates.Where(w => w.Url_Id == c.Id).ToList();
                }

                using (var db = new WeblancerDbContext())
                {
                    if (workerExist.Count == 0)
                    {
                        db.WeblancerCandidates.Add(weblancer);
                        Console.WriteLine("Successfully added: " + weblancer.Login);
                    }
                    else
                    {
                        db.WeblancerCandidates.Update(weblancer);
                        Console.WriteLine("Successfully updated: " + weblancer.Login);
                    };
                    db.SaveChanges();
                }
            }

        }

        public int GetLastPage()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var enc1252 = Encoding.GetEncoding("Windows-1251");
            var htmlWeb = new HtmlWeb();

            htmlWeb.OverrideEncoding = enc1252;
            var linkHTML = htmlWeb.Load("https://www.weblancer.net/freelancers/");

            var node = linkHTML.DocumentNode.SelectNodes("//div[@class='pagination_box']//a").LastOrDefault().Attributes["href"].Value;
            var replaceString = "/freelancers/?page=";
            var result = Convert.ToInt32(node.Replace(replaceString, ""));
            return result;

        }
    }
}
