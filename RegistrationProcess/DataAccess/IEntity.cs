using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
