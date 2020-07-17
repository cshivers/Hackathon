namespace Hackathon.Core.Services
{
    public interface IMetaService
    {
        string GetAgentName(string id);
        string GetRankTier(int id);
        string GetWeaponName(string id);
        string GetRankImage(int id);
    }
}
