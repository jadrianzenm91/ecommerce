using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Master
{
    public interface IPaymentCard : IDisposable
    {
        string CreateToken();
        string CreateCharge(Dictionary<string, object> map);
    }
}
