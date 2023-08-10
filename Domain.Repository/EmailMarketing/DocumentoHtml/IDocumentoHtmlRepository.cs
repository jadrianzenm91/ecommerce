using Domain.Entities.EmailMarketing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.EmailMarketing.DocumentoHtml
{
    public interface IDocumentoHtmlRepository
    {
        void InsertDocumentoHtml(DocumentoHtmlEN item);
        DocumentoHtmlEN SelectDocumentoHtml(DocumentoHtmlEN item);
    }
}
