using MessageService.DataModel;

namespace MessageService.IRepository.Unit
{
    public interface IMainUnit : IUnit
    {
        IRepository<Setting> Settings { get; }
        IRepository<Message> Messages { get; }
        
    }
}
