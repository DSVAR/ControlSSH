using ControlSSH.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using SshClient = Renci.SshNet.SshClient;

namespace ControlSSH.Server.Controllers;

 [ApiController]
    [Route("[controller]")]
    public class SshController : ControllerBase
    {
       
        [HttpGet]
        [Route("Check")]
        public string Check()
        {
            using (var client = new SshClient("192.168.0.220", "admin", "admin."))
            {
                try
                {
                    client.Connect();
                    client.Disconnect();
                    return "ok";
                }
                catch(Exception e)
                {
                    client.Disconnect();
                  
                    return $"{e.Message}";
                }
            }
        }
     

        
        
        [HttpGet]
        [Route("GetFolders")]
        public string GetFolders()
        {
            var command = "ls";
            var listOfFolders = new List<SshFolder>();
            
            using (var client = new SshClient("192.168.0.220", "admin", "admin"))
            {
                try
                {
                    client.Connect();
                    var commandClient=client.RunCommand("pwd");
                    var answer =client.RunCommand(command).Result;
                    SshFolder item = new SshFolder();
                    for (int i = 0; i < answer.Length; i++)
                    {
                        if (answer[i].ToString() == "\n")
                        {
                            listOfFolders.Add(item);
                            item = new SshFolder();
                        }
                        else
                        {
                            item.Name += answer[i];
                        }
                        

                    }
                    client.Disconnect();
                    return $"{answer.ToString()}";
                }
                catch(Exception e)
                {
                    client.Disconnect();
                  
                    return $"{e.Message}";
                }
            }
        }


        public string BackFolder()
        {
            using (var client = new SshClient("192.168.0.220", "admin", "admin"))
            {
                try
                {
                    client.Connect();
                    
                    var commandClient=client.RunCommand("pwd");
                    var answer =client.RunCommand("cd ..").Result;
                   
                    client.Disconnect();
                    return $"{answer.ToString()}";
                }
                catch(Exception e)
                {
                    client.Disconnect();
                  
                    return $"{e.Message}";
                }
            }
        }


    }