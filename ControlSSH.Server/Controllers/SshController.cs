using ControlSSH.Client.Components.FullComponent;
using ControlSSH.Shared;
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
        public IActionResult Check(string password,string username,string ip)
        {
            using (var client = new SshClient(ip, username, password))
            {
                try
                {
                    client.Connect();
                    client.Disconnect();
                    return Ok();
                }
                catch(Exception e)
                {
                    client.Disconnect();
                  
                    return BadRequest($"{e.Message}") ;
                }
            }
        }
     

        
        
        [HttpGet]
        [Route("GetFolders")]
        public IActionResult GetFolders(string password,string username,string ip,bool isBack,string? path=null)
        {
            var command = "ls";
            var commandPwd = "";
           
            var listOfFolders = new List<SshFolder>();
            
            using (var client = new SshClient(ip, username, password))
            {
                try
                {
                    client.Connect();
               
                    command = (string.IsNullOrEmpty(path)) ? "ls" : $"cd {path}; ls";
                    commandPwd = (string.IsNullOrEmpty(path)) ? "pwd" : $"cd {path}; pwd";
                
                    var commandClient=client.RunCommand(commandPwd);
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
                            item.CurrentPath = commandClient.Result;
                        }
                        

                    }
                    client.Disconnect();
                    return Ok(listOfFolders);
                }
                catch(Exception e)
                {
                    client.Disconnect();
                  
                    return BadRequest($"{e.Message}");
                }
            }
        }

        [HttpGet]
        [Route("BackFolder")]
        public IActionResult BackFolder(string password,string username,string ip,string path)
        {
            var listOfFolders = new List<SshFolder>();
            using (var client = new SshClient(ip, username, password))
            {
                try
                {
                    client.Connect();
                    
                    var commandClient=client.RunCommand($"cd {path};cd ..;pwd");
                    var answer =client.RunCommand($"cd {path};cd ..; ls").Result;
                   
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
                            item.CurrentPath = commandClient.Result;
                        }
                        

                    }
                    client.Disconnect();
                    return Ok(listOfFolders);
                }
                catch(Exception e)
                {
                    client.Disconnect();
                  
                    return BadRequest( e.Message);
                }
            }
        }


    }