using ControlSSH.Client.Components.FullComponent;
using ControlSSH.Shared;
using ControlSSH.Shared.Models;
using MudBlazor;
using Newtonsoft.Json;

namespace ControlSSH.Client.Services;

public class HttpService
{
    private readonly HttpClient _client;
    private readonly ISnackbar _snackbar;

    public HttpService(HttpClient client,ISnackbar snackbar)
    {
        _client = client;
        _snackbar = snackbar;
    }

    
    public async Task<string> Check(string ip,string username, string password)
    {
        var response = await _client.GetAsync($"/Ssh/Check?Username={username}&Password={password}&Ip={ip}");
        var text = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            _snackbar.Add("Подключение успешно", Severity.Success);
            return ("successful");
        }
        else
        {
            _snackbar.Add(text, Severity.Error);
            return "Error";
        }
    }
    
    
    public async Task<IEnumerable<SshFolder>?> GetFolder(string ip,string username, string password,bool isBack=false,string path=null)
    {
        
        var response = await _client.GetAsync($"Ssh/GetFolders?Username={username}&Password={password}&Ip={ip}&isBack={isBack}&path={path}");
        var text = await response.Content.ReadAsStringAsync();

        try
        {
            if (response.IsSuccessStatusCode)
            {
                var swe = JsonConvert.DeserializeObject<List<SshFolder>>(text);
                return swe;
            }
            else
            {
                _snackbar.Add(text, Severity.Error);
                return null;
            }
        }
        catch (Exception e)
        {
            return null;
        }
    }
    
    
    public async Task<IEnumerable<SshFolder>?> BackFolder(string ip,string username, string password,string path)
    {
        
        var response = await _client.GetAsync($"Ssh/BackFolder?Username={username}&Password={password}&Ip={ip}&path={path}");
        var text = await response.Content.ReadAsStringAsync();

        try
        {
            if (response.IsSuccessStatusCode)
            {
                var swe = JsonConvert.DeserializeObject<List<SshFolder>>(text);
                return swe;
            }
            else
            {
                _snackbar.Add(text, Severity.Error);
                return null;
            }
        }
        catch (Exception e)
        {
            return null;
        }
    }
}