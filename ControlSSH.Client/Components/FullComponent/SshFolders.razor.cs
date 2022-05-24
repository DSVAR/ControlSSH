using ControlSSH.Client.Services;
using ControlSSH.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace ControlSSH.Client.Components.FullComponent;

public partial class SshFolders
{
  [Parameter]
  public string? Ip { get; set; }
  [Parameter]
  public string? Password { get; set; }
  [Parameter]
  public string? Username { get; set; }

  private IEnumerable<SshFolder>? FoldersList;

  [Inject] private HttpService? Http { get; set; }
  
  protected override async Task OnInitializedAsync()
  {
    FoldersList = await Http?.GetFolder(Ip!,Username!,Password!)!;
    var we = 2;
    StateHasChanged();
  }
}