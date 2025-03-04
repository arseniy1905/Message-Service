using MessageService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageService.IService
{
    public interface IMessageService : IDataService
    {
        Task<MessageResponseViewModel> CanSend(MessageRequestViewModel messageViewModel);
    }
}
