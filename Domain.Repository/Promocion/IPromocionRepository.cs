using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Promocion
{
    public interface IPromocionRepository
    {
        List<PromocionEN> SelectAll();
        PromocionEN Select(PromocionEN item);
        void Insert(PromocionEN item);
        void Delete(PromocionEN item);
        void Update(PromocionEN item);
    }
}
