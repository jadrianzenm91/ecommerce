using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.FormaPago
{
    public interface IFormaPagoRepository
    {
        List<FormaPagoEN> SelectAll();
        FormaPagoEN Select(FormaPagoEN item);
        void Insert(FormaPagoEN item);
        void Delete(FormaPagoEN item);
        void Update(FormaPagoEN item);
    }
}
