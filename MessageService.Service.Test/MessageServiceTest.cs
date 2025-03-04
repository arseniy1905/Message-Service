using Microsoft.Extensions.Configuration;
using MessageService.IRepository.Unit;
using MessageService.IService;
using MessageService.Repository.SQL.Unit;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageService.Service.Test
{
    [TestFixture]
    public class MessageServiceTest
    {
        private IMainUnit _mainUnit;
        private IMessageService _messageService;
        private IConfiguration _configuration;
        public MessageServiceTest()
        {
            SetUpFixute();
        }

        private void SetUpFixute()
        {
            if (_mainUnit != null)
            {
                ((MainUnit)_mainUnit).EnsureDeleted();
            }
            _mainUnit = new MainUnit(new DbContextOptionsBuilder<MainUnit>()
            .UseInMemoryDatabase(string.Format("Messages{0}", Guid.NewGuid().ToString())).Options);
            var configuration = new Mock<IConfiguration>();
            _messageService = new MessageService(configuration.Object, _mainUnit);
            _mainUnit.Settings.AddEntity(new DataModel.Setting()
            {

            });
        }

        [Test]
        public void CanSendTest()
        {

        }
    }
}
