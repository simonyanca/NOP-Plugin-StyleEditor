using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Web.Framework.Events;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Admin.StyleEditor;

/// <summary>
/// Event consumer that handles adding the admin menu
/// </summary>
public class EventConsumer : IConsumer<AdminMenuCreatedEvent>
{
    private readonly ILocalizationService _localizationService;
    private readonly IPermissionService _permissionService;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="localizationService"></param>
    /// <param name="permissionService"></param>
    public EventConsumer(ILocalizationService localizationService, IPermissionService permissionService)
    {
        _localizationService = localizationService;
        _permissionService = permissionService;
    }

    /// <summary>
    /// Adds the menu item for the editor page
    /// </summary>
    /// <param name="eventMessage"></param>
    /// <returns></returns>
    public async Task HandleEventAsync(AdminMenuCreatedEvent eventMessage)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_PLUGINS))
            return;

        eventMessage.RootMenuItem.InsertBefore("Local plugins",
            new AdminMenuItem
            {
                SystemName = "StyleEditor",
                Title = await _localizationService.GetResourceAsync("Plugins.Admin.StyleEditor.EditorTitle"),
                Url = eventMessage.GetMenuItemUrl("StyleEditor", "EditStyles"),
                IconClass = "far fa-dot-circle",
                Visible = true
            });
    }
}