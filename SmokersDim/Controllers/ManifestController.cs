public interface IDestinyManifectService
{
    Task<string> SetDestinyManifestAsync();
}

public class DestinyManifectService : IDestinyManifectService
{
    public Task<string> SetDestinyManifestAsync()
    {
        throw new NotImplementedException();
    }
}