namespace Web.NavigationServices;

public class MenuUpdateService()
{
    public event EventHandler? LinksChanged;

    public void HomesUpdated()
    {
        LinksChanged?.Invoke(this, EventArgs.Empty);
    }
}
