using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.ParametroValor
{
    public interface IParametroValorRepository
    {
        List<ParametroValorEN> SelectAll(ParametroValorEN item);
        ParametroValorEN Select(ParametroValorEN item);
        void Insert(ParametroValorEN item);
        void Update(ParametroValorEN item);
    }
}
