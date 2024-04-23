using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;
using DbLayer;
using Azure.Core;
using System.Diagnostics.Eventing.Reader;


namespace Server_1
{
    public class Server_lisengs
    {


        private TcpListener _listener;
        private readonly int _port = 9002; 

        public Server_lisengs()
        {
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), _port);
           
        }

        public void Start()
        {
            _listener.Start();
           

            while (true)
            {
                TcpClient client = _listener.AcceptTcpClient();

                HandleClient(client);
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
           
            byte[] buffer = new byte[1024];
            int bytesRead = ns.Read(buffer, 0, buffer.Length);
            string jsonString = Encoding.UTF8.GetString(buffer, 0, bytesRead); 
            MyRequest  my  = JsonConvert.DeserializeObject<MyRequest>(jsonString);

            if (my.Header.ToString() == "Login")
            {

                var factory = new ProductsDbContextFactory();
                using (var db = factory.CreateDbContext(null))
                {
                    foreach (var item in db.users)
                    {
                        if (item.Login == my.AuthUser.Login 
                            && item.Password == my.AuthUser.Password)
                        {
                            if (item.IsAdmin == false)
                            {
                                string jsonResponse = "SUCCESS";
                                byte[] responseData = Encoding.UTF8.GetBytes(jsonResponse);
                                ns.Write(responseData, 0, responseData.Length);
                            }
                            if (item.IsAdmin == true)
                            {
                                string jsonResponse = "SUCCESS1";
                                byte[] responseData = Encoding.UTF8.GetBytes(jsonResponse);
                                ns.Write(responseData, 0, responseData.Length);
                            }

                        }
                    }
                }
            }
            else if (my.Header.ToString() == "Ques")
            {
              
                List<Quest> qu = new List<Quest>();
                
                string text = my.quest.Title.ToString();
                
               
                var factory = new ProductsDbContextFactory();
                using (var db = factory.CreateDbContext(null))
                {

                    qu.AddRange(db.quests.Where(i=>i.Title_QuesId == (db.titles.Where(tm => tm.Title == text).Select(r => r.Id).FirstOrDefault())));

                    MyResponse my1 = new MyResponse() { quests=qu, Massage = "SUCCESS" };
                    
                    string jsonRequest = JsonConvert.SerializeObject(my1);
                    byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
                    ns.Write(requestData, 0, requestData.Length);

              
                }
            }

            else if (my.Header.ToString() == "ALL")
            {

                List<Quest> qu = new List<Quest>();

                var factory = new ProductsDbContextFactory();
                using (var db = factory.CreateDbContext(null))
                {
                    foreach (var item in db.quests)
                    {
                        qu.Add(item);
                    }
                    MyResponse my1 = new MyResponse() { quests = qu , Massage = "SUCCESS" };
                    string jsonRequest = JsonConvert.SerializeObject(my1);
                    byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
                    ns.Write(requestData, 0, requestData.Length);
                }
            }

            else if (my.Header.ToString() == "Title")
            {
                
                List<string> tmp =  new List<string>();

                var factory = new ProductsDbContextFactory();
                using (var db = factory.CreateDbContext(null))
                {
                    foreach (var item in db.titles)
                    {
                        tmp.Add(item.Title);
                    }
                    MyResponse my1 = new MyResponse() { str = tmp, Massage = "SUCCESS" };

                    string jsonRequest = JsonConvert.SerializeObject(my1);
                    byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
                    ns.Write(requestData, 0, requestData.Length);


                }
            }
            else if (my.Header.ToString() == "CHANGE")
            {

                //List<Quest> qu = new List<Quest>();
                //for (int i = 0; i < my.chquest.Count; i++)
                //{
                //    qu.Add(my.chquest[i]);
                //}



                var factory = new ProductsDbContextFactory();
                using (var db = factory.CreateDbContext(null))
                {
                    foreach (var quest in my.chquest)
                    {
                        //db.quests.RemoveRange(quest);
                        var existingQuest = db.quests.FirstOrDefault(q => q.Id == quest.Id);
                        if (existingQuest != null)
                        {
                            // Оновити значення властивостей
                        
                            existingQuest.Vopros = quest.Vopros;
                            existingQuest.Quests1 = quest.Quests1;
                            existingQuest.Quests2 = quest.Quests2;
                            existingQuest.Quests3 = quest.Quests3;
                            existingQuest.Quests4 = quest.Quests4;
                            existingQuest.right = quest.right;
                        }
                    }

                    db.SaveChanges();

                    MyResponse my1 = new MyResponse() { Massage = "SUCCESS" };

                    string jsonRequest = JsonConvert.SerializeObject(my1);
                    byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
                    ns.Write(requestData, 0, requestData.Length);


                }
            }
            else if (my.Header.ToString() == "DEL")
            {



                var factory = new ProductsDbContextFactory();
                using (var db = factory.CreateDbContext(null))
                {
                    foreach (var quest in my.chquest)
                    {
                        //db.quests.RemoveRange(quest);
                        var existingQuest = db.quests.FirstOrDefault(q => q.Id == quest.Id);
                        if (existingQuest != null)
                        {
                            db.quests.Remove(existingQuest);

                        }
                    }

                    db.SaveChanges();

                    MyResponse my1 = new MyResponse() { Massage = "SUCCESS" };

                    string jsonRequest = JsonConvert.SerializeObject(my1);
                    byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
                    ns.Write(requestData, 0, requestData.Length);


                }
            }
            else if (my.Header.ToString() == "ADD")
            {


                var tmp = my.quest.Title;
           

                var factory = new ProductsDbContextFactory();
                using (var db = factory.CreateDbContext(null))
                {
                    foreach (var item in db.titles)
                    {
                        if (item.Title == tmp)
                        {
                            my.quest.Quest1[0].Title_QuesId = item.Id;
                            db.quests.Add(my.quest.Quest1[0]);
                        }
                    } 

                    
               db.SaveChanges();

                    MyResponse my1 = new MyResponse() { Massage = "SUCCESS" };

                    string jsonRequest = JsonConvert.SerializeObject(my1);
                    byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
                    ns.Write(requestData, 0, requestData.Length);


                }
            }
            else if (my.Header.ToString() == "ADD_TITLE")
            {


                Title_Ques tmp = new Title_Ques() { Title = my.quest.Title, Quest1 = new List<Quest>()  };


                var factory = new ProductsDbContextFactory();
                using (var db = factory.CreateDbContext(null))
                {


                    db.titles.Add(tmp);
                          
                   

                    db.SaveChanges();

                    MyResponse my1 = new MyResponse() { Massage = "SUCCESS" };

                    string jsonRequest = JsonConvert.SerializeObject(my1);
                    byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
                    ns.Write(requestData, 0, requestData.Length);


                }
            }

            client.Close();
        }
    }
}
