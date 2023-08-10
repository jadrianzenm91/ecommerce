using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.CuponDescuento
{
    public interface ICuponDescuentoRepository
    {
        List<CuponDescuentoEN> SelectAll();
        CuponDescuentoEN Select(CuponDescuentoEN item);
        void Insert(CuponDescuentoEN item);
        void Delete(CuponDescuentoEN item);
        void Update(CuponDescuentoEN item);
    }
}
