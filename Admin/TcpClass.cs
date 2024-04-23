using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace Admin
{
    internal static class TcpClass
    {

        public static string Zapros(MyRequest request)
        {
            try
            {
                using (TcpClient client = new TcpClient("127.0.0.1", 9002))
                {
                    NetworkStream ns = client.GetStream();

                    string jsonRequest = JsonConvert.SerializeObject(request);
                    byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
                    ns.Write(requestData, 0, requestData.Length);

                    byte[] responseData = new byte[1024];
                    int bytesRead = ns.Read(responseData, 0, responseData.Length);
                    return Encoding.UTF8.GetString(responseData, 0, bytesRead);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Відсутнє з'єднання з сервером");
                return null;
            }
            

        }
    }
}
