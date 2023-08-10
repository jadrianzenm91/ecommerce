using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Planes
{
    public interface IPlanesRepository
    {
        List<PlanesEN> SelectAll(PlanesEN item);
        PlanesEN Select(PlanesEN item);
        void Insert(PlanesEN item);
        void Delete(PlanesEN item);
        void Update(PlanesEN item);
    }
}
