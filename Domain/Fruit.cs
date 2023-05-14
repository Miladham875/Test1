using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Fruit : BaseEntity
    {
        public string Name { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public string Color { get; set; }

        public int CreatorUserId { get; set; }

    }
}
