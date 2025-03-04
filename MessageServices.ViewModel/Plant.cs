using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.ViewModel
{
    public class Plant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool IsWatered { get; set; }
        public bool IsWatering { get; set; }
        public bool CannotWater { get; set; }
    }
}
