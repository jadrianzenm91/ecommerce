using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Caracteristica
{
    public interface ICaracteristicaRepository
    {
        List<CaracteristicaEN> SelectAll();
        CaracteristicaEN Select(CaracteristicaEN item);
        void Insert(CaracteristicaEN item);
        void Delete(CaracteristicaEN item);
        void Update(CaracteristicaEN item);
    }
}
