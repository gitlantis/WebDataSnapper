using FreelancerGrabber.Services;
using System;

namespace FreelancerGrabber
{
    class Program
    {
        static void Main(string[] args)
        {
            WeblancerService service = new WeblancerService();
            if (String.Compare(args[0], "UploadLinks") == 0)
            {
                service.UploadUrl();
            }
            else if (String.Compare(args[0], "LoadData") == 0)
            {
                try
                {
                    var start = 0;
                    var length = 0;
                    if (args[1] != "") start = Convert.ToInt32(args[1]);
                    if (args[2] != "") length = Convert.ToInt32(args[2]);

                    service.DataLoader(start, length);
                }
                catch
                {
                    service.DataLoader(0, 0);
                }
                    
            }
            
        }
    }
}
