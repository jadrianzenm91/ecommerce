using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.PlanTienda
{
    public interface IPlanTiendaRepository
    {
        List<PlanTiendaEN> SelectAll(PlanTiendaEN item);
        PlanTiendaEN Select(PlanTiendaEN item);
        void Insert(PlanTiendaEN item);
        void Delete(PlanTiendaEN item);
        void Update(PlanTiendaEN item);
    }
}
