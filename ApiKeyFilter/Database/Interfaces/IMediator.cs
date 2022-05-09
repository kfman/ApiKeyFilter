namespace ApiKeyFilter.Database.Interfaces;

public interface IMediator {
    public void AddApiKeyToRole(string keyId, string roleId);
    void RemoveApiKeyFromRole(string keyId, string roleId);
}
