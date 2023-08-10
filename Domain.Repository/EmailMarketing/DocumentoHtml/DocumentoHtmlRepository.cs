using Domain.Entities.EmailMarketing;
using Infraestruture.CrossCutting;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.EmailMarketing.DocumentoHtml
{
    public class DocumentoHtmlRepository : IDocumentoHtmlRepository
    {
        public void InsertDocumentoHtml(DocumentoHtmlEN item)
        {
            try
            {
                DatabaseFactory.CreateDatabase().ExecuteScalar(
                    "dbo.USP_INS_DocumentoHtml",
                    item.textoHtml
                  );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DocumentoHtmlEN SelectDocumentoHtml(DocumentoHtmlEN item)
        {
            Database oDatabase = DatabaseFactory.CreateDatabase();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand("dbo.USP_SEL_DocumentoHtml");
            oDatabase.AddInParameter(oDbCommand, "@I_CODIGO_DOCUMENTOHTML", DbType.Int32, item.codigoDocumento);

            using (IDataReader oReader = oDatabase.ExecuteReader(oDbCommand))
            {
                while (oReader.Read())
                {
                    item = new DocumentoHtmlEN();
                    item.codigoDocumento = DataConvert.ToInt32(oReader["I_CODIGO_DOCUMENTOHTML"]);
                    item.textoHtml = DataConvert.ToString(oReader["V_TEXTO_HTML"]);
                }
                oReader.Close();
            }
            return item;
        }
    }
}
