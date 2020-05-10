using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels.Entities.BasicEntity
{
    public interface IIsActive
    {
        public bool IsActive { get; set; }
        
    }

    public interface IBase:IIsActive
    {
        public DateTime PublishDate { get; set; }
    }
}
