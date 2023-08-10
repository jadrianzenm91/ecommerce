using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Perfil
{
    public interface IPerfilRepository : IDisposable
    {
        List<PerfilEN> SelectAllPerfil();
        PerfilEN SelectPerfilById(int id);
    }
}
