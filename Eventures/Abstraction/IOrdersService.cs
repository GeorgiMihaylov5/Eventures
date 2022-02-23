using Eventures.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Abstraction
{
    public interface IOrdersService
    {
        bool Create(Order order);
    }
}
