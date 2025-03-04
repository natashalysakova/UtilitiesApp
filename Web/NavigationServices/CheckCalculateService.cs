namespace Web.NavigationServices;

public class CheckCalculateService()
{
    public event Action? OnRefreshRequested;

    public event Action? OnModelUpdated;

    public void RequestRefresh()
    {
        OnRefreshRequested?.Invoke();
    }

    public void ModelUpdated()
    {
        OnModelUpdated?.Invoke();
    }
}
