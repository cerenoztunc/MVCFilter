using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCFilter.Models.Entities
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
        }

    }
}