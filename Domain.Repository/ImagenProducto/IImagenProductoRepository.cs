using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.ImagenProducto
{
    public interface IImagenProductoRepository
    {
        List<ImagenProductoEN> SelectByProducto(long idproducto);
        long Insert(ImagenProductoEN item);
        void Delete(ImagenProductoEN item);
    }
}
