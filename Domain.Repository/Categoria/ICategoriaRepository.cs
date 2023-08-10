using Domain.Entities;
using Domain.EntitiesLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Categoria
{
    public interface ICategoriaRepository
    {
        List<CategoriaEN> SelectAll(CategoriaEN item);
        CategoriaEN Select(CategoriaEN item);
        void Insert(CategoriaEN item);
        void Delete(CategoriaEN item);
        void Update(CategoriaEN item);
    }
}
