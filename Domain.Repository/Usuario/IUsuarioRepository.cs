using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Usuario
{
    public interface IUsuarioRepository
    {
        List<UsuarioEN> SelectAll();
        UsuarioEN Select(UsuarioEN item);
        void Insert(UsuarioEN item);
        tokenUsuarioEN createToken(tokenUsuarioEN item);
        void Delete(UsuarioEN item);
        void Update(UsuarioEN item);
        long InsertProviderAuth(UsuarioEN item);
        UsuarioEN getUsuarioTienda(UsuarioEN item);
        UsuarioEN confirmationToken(UsuarioEN item);
    }
}
