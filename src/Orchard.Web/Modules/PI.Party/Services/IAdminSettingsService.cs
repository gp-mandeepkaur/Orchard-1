using Orchard;

namespace PI.Party.Services
{
    public interface IAdminSettingsService : IDependency
    {
        void ExecuteSql();
    }
}