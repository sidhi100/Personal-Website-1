using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;  // must install Newtonsoft package from Nuget
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ConsoleClient
{
    class ConsoleClient
    {
        public HttpClient client { get; set; }

        private string baseUrl_;

        ConsoleClient(string url)
        {
            baseUrl_ = url;
            client = new HttpClient();
        }
        //----< upload file >--------------------------------------

        public async Task<HttpResponseMessage> SendFile(string fileSpec)
        {
            MultipartFormDataContent multiContent = new MultipartFormDataContent();

            byte[] data = File.ReadAllBytes(fileSpec);
            ByteArrayContent bytes = new ByteArrayContent(data);
            string fileName = Path.GetFileName(fileSpec);
            multiContent.Add(bytes, "files", fileName);

            return await client.PostAsync(baseUrl_, multiContent);
        }
        //----< get list of files in server FileStorage >----------

        public async Task<IEnumerable<string>> GetFileList()
        {
            HttpResponseMessage resp = await client.GetAsync(baseUrl_);
            var files = new List<string>();
            if (resp.IsSuccessStatusCode)
            {
                var json = await resp.Content.ReadAsStringAsync();
                JArray jArr = (JArray)JsonConvert.DeserializeObject(json);
                foreach (var item in jArr)
                    files.Add(item.ToString());
            }
            return files;
        }
        //----< download the id-th file >--------------------------

        public async Task<HttpResponseMessage> GetFile(int id)
        {
            return await client.GetAsync(baseUrl_ + "/" + id.ToString());
        }
        //----< delete the id-th file from FileStorage >-----------

        public async Task<HttpResponseMessage> DeleteFile(int id)
        {
            return await client.DeleteAsync(baseUrl_ + "/" + id.ToString());
        }
        //----< usage message shown if command line invalid >------

        static void showUsage()
        {
            Console.Write("\n  Command line syntax error: expected usage:\n");
            Console.Write("\n    http[s]://machine:port /option [filespec]\n\n");
        }
        //----< validate the command line >------------------------

        static bool parseCommandLine(string[] args)
        {
            if (args.Length < 2)
            {
                showUsage();
                return false;
            }
            if (args[0].Substring(0, 4) != "http")
            {
                showUsage();
                return false;
            }
            if (args[1][0] != '/')
            {
                showUsage();
                return false;
            }
            return true;
        }
        //----< display command line arguments >-------------------

        static void showCommandLine(string[] args)
        {
            string arg0 = args[0];
            string arg1 = args[1];
            string arg2;
            if (args.Length == 3)
                arg2 = args[2];
            else
                arg2 = "";
            Console.Write("\n  CommandLine: {0} {1} {2}", arg0, arg1, arg2);
        }

        static void Main(string[] args)
        {
            Console.Write("\n  CoreConsoleClient");
            Console.Write("\n ===================\n");

            if (!parseCommandLine(args))
            {
                return;
            }
            

            //Console.ReadKey();
            string val;
            
            string url = args[0];
            ConsoleClient client = new ConsoleClient(url);

            //showCommandLine(args);
            // Console.Write("\n  sending request to {0}\n", url);
            bool flag = true;
            while (flag == true)
            {
                
               
                Console.WriteLine("Press 1 to send a file from Client to Server");
                Console.WriteLine("Press 2 to get list of files in Server: ");
                Console.WriteLine("Press 3 to Exit Client");
                val = Console.ReadLine();
                Console.WriteLine();
                switch (val)
                {
                    case "1":
                        DirectoryInfo d = new DirectoryInfo("../ClientFileStorage/");
                        FileInfo[] Files = d.GetFiles(); //Getting Text files
                        Console.WriteLine("Files in the Client");
                        Console.WriteLine("========================");
                        foreach (FileInfo file in Files)
                        {
                             Console.WriteLine(file.Name);
                        }
                        Console.WriteLine("========================");
                        Console.WriteLine("Enter Complete file name");
                        string test;
                        test = Console.ReadLine();
                        string path = "../ClientFileStorage/";
                        bool f = false ;
                        foreach (FileInfo file in Files)
                        {
                            if (test == file.Name)
                            {
                                path = path + test;
                                Task<HttpResponseMessage> Send = client.SendFile(path);
                                Console.WriteLine("==================================");
                                Console.WriteLine(Send.Result);
                                Console.WriteLine("==================================");
                                f = true;
                                break;
                            }
                            else
                                f = false;
                        }
                        if(f == false)
                            Console.WriteLine(">-----------------------< Incorrect File Name >-----------------<");
                        Console.WriteLine();
                        Console.WriteLine();
                        break;
                    case "2":
                        Task<IEnumerable<string>> tfl = client.GetFileList();
                        var resultfl = tfl.Result;

                        Console.WriteLine("============================");
                        foreach (var item in resultfl)
                        {
                            Console.Write("\n  {0}", item);
                        }
                        Console.WriteLine();
                        Console.WriteLine("============================");
                        Console.WriteLine();
                        break;
                    case "3":
                        flag = false;
                        break;
                    
                    default:

                        Console.WriteLine(">----------------------< Error! >--------------< Incorrect Input >------------<");
                        break;
                }
            }

            //Console.WriteLine("\n  Press Key to exit: ");
            //Console.ReadKey();
        }
    }
}
