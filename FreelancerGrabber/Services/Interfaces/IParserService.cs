using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerGrabber.Services.Interfaces
{
    public interface IWeblancerService
    {
        public Task<dynamic> UploadUrl(int start = 1, int end = 0);
        public void DataLoader(int start, int length);
        int GetLastPage();
    }
}
