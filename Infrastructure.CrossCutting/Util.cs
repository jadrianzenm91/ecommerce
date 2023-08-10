//using Microsoft.Azure;
//using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.CrossCutting
{
    public static class Util
    {
        public static void sendEmailHtml(string to, string body, string subject)
        {
            var senderEmail = new MailAddress("jadrianzenm91@gmail.com", "Sistemas Adrianzen EIRL");
            var receiverEmail = new MailAddress(to, "Cliente");
            var password = "s1st3m4s46995277";
                        
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = "[" + Constantes.EMPRESA.SiglaEmpresa + "] " + subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(mess);
                //smtp.SendMailAsync(mess);
            }  
        }
        public static string bodyEmailComprobante()
        {
            #region bodySQL

            return
                @"<div style=""box-sizing:border-box;width:100%;height:100%;margin:0;padding:0;background:none;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Roboto,Helvetica,Arial,sans-serif"">
   <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" >
      <tbody>
         <tr >
            <td align=""left"" valign=""top"" >
               <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""max-width:37.5rem;"" bgcolor=""#fff"">
                  <tbody>
                     <tr >
                        <td style=""font-size:14px;line-height:20px;box-sizing:border-box;border-collapse:collapse;width:100%;text-align:center!important;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Roboto,Helvetica,Arial,sans-serif"" align=""center"">
                           <div style=""box-sizing:border-box;width:12rem;margin-bottom:1.563rem;margin-top:1.563rem;margin-right:auto;margin-left:auto!important""> <a href=""{22}"" style=""box-sizing:border-box!important"" target=""_blank""> <img alt="""" border=""0"" width=""auto"" src=""{18}"" style=""border-radius:5px;vertical-align:top;outline:none;text-decoration:none;max-width:100%;border:none;max-height:67px!important""></a><br><b><i>{14}</i></b></div>
                        </td>
                     </tr>
                     <tr >
                        <td>
                           <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                              <tbody>
                                 <tr >
                                    <td style=""background-color:#fb3449;"">
                                       <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                          <tbody>
                                             <tr>
                                                <td>
                                                   <div style=""box-sizing:border-box;width:25.63rem;margin-bottom:0.75rem;margin-top:2rem;margin-right:auto;margin-left:auto!important;color:#fff;font-size:20px;line-height:25px;font-weight:normal;text-align:left;"">  Hola! {0}   </div><div style=""box-sizing:border-box;width:25.63rem;margin-top:0;margin-right:auto;margin-left:auto!important;color:#fff;font-size:14px;line-height:13px;text-align:left"">El estado de tu pedido se encuentra:<br></div>
                                                   <div style=""box-sizing:border-box;width:25.63rem;margin-bottom:0.75rem;margin-top:0;margin-right:auto;margin-left:auto!important;color:#fff;font-size:25px;line-height:25px;text-align:left""> <span style=""font-weight:bold;"">{15}</span> </div>
                                                </td>
                                             </tr>
                                             {16}
                                          </tbody>
                                       </table>
                                    </td>
                                    <td style=""width:0.625rem;background-color:#fff"">&nbsp;</td>
                                    <td style=""width:8.75rem;background-color:#3bb0c9;vertical-align:middle;text-align:center""> <img src=""https://ci6.googleusercontent.com/proxy/9oIYq0XJko-dc-EyG3xSt3i7ZEBXLnwWdqSmZytG8j1L_W177Quya5-5dJKY9n-2XMHnzxGglG8S_1E4bHE8Ud5maov2KMpB5-wzjgIGBw=s0-d-e1-ft#http://oechsle.vteximg.com.br/arquivos/mail-ico-aprobado.png"" style=""vertical-align:top;outline:none;text-decoration:none;max-width:100%;border:none;max-height:101px!important"" class=""CToWUd""> </td>
                                 </tr>
                              </tbody>
                           </table>
                        </td>
                     </tr>
                     <tr style=""box-sizing:border-box!important;background-color:#fff"">
                        <td>
                           <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                              <tbody>
                                 <tr style=""box-sizing:border-box!important"">
                                    <td style=""width:2.188rem;height:3rem;margin:0px;padding:0px;background-color:#fff"" colspan=""3"">&nbsp;</td>
                                 </tr>
                                 <tr style=""box-sizing:border-box!important"">
                                    <td style=""width:2.188rem;background-color:#fff"">&nbsp;</td>
                                    <td style=""width:33.13rem;background-color:#fff;vertical-align:middle;text-align:center"">
                                       <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                          <tbody>
                                             <tr style=""box-sizing:border-box!important"">
                                                <td style=""width:2.188rem;margin:0px;padding:0px;background-color:#fff;text-align:center;color:#333333;font-size:18px;line-height:18px;font-weight:bold;letter-spacing:0.1875rem"">ESTADO DE TU COMPRA</td>
                                             </tr>
                                             <tr style=""box-sizing:border-box!important"">
                                                <td style=""width:2.188rem;margin:0px;padding:0px;background-color:#fff"">&nbsp;</td>
                                             </tr>
                                          </tbody>
                                       </table>
                                    </td>
                                    <td style=""width:2.188rem;background-color:#fff"">&nbsp;</td>
                                 </tr>
                                 <tr style=""box-sizing:border-box!important"">
                                    <td style=""width:2.188rem;margin:0px;padding:0px;background-color:#fff"">&nbsp;</td>
                                    <td > <img src=""{17}"" style=""vertical-align:top;outline:none;text-decoration:none;max-width:100%;border:none;max-height:36px!important"" class=""CToWUd""> </td>
                                    <td style=""width:2.188rem;margin:0px;padding:0px;background-color:#fff"">&nbsp;</td>
                                 </tr>
                                 <tr style=""box-sizing:border-box!important"">
                                    <td style=""width:2.188rem;margin:0px;padding:0px;background-color:#fff"">&nbsp;</td>
                                    <td style=""width:33.13rem;margin:0px;padding:0px;background-color:#fff;vertical-align:middle;text-align:center"">
                                       <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                          <tbody>
                                             <tr style=""box-sizing:border-box!important"">
                                                <td style=""width:8.25rem;margin:0px;padding:0.5rem 0;background-color:#fff;text-align:center;color:#333333;font-size:16px;line-height:20px;font-weight:normal"">En validación</td>
                                                <td style=""width:8.25rem;margin:0px;padding:0.5rem 0;background-color:#fff;text-align:center;color:#333333;font-size:16px;line-height:20px;font-weight:normal"">Confirmada</td>
                                                <td style=""width:8.25rem;margin:0px;padding:0.5rem 0;background-color:#fff;text-align:center;color:#333333;font-size:16px;line-height:20px;font-weight:normal"">Por entregar</td>
                                                <td style=""width:8.25rem;margin:0px;padding:0.5rem 0;background-color:#fff;text-align:center;color:#333333;font-size:16px;line-height:20px;font-weight:normal"">Entregada</td>
                                             </tr>
                                          </tbody>
                                       </table>
                                    </td>
                                    <td style=""width:2.188rem;margin:0px;padding:0px;background-color:#fff"">&nbsp;</td>
                                 </tr>
                              </tbody>
                           </table>
                        </td>
                     </tr>
                     <tr style=""box-sizing:border-box!important;background-color:#fff"">
                        <td>
                           <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                              <tbody>
                                 <tr style=""box-sizing:border-box!important"">
                                    <td style=""width:2.188rem;height:3rem;margin:0px;padding:0px;background-color:#fff"" colspan=""3"">&nbsp;</td>
                                 </tr>
                                 <tr style=""box-sizing:border-box!important"">
                                    <td style=""width:2.188rem;background-color:#fff"">&nbsp;</td>
                                    <td style=""width:33.13rem;background-color:#fff;vertical-align:middle;text-align:center"">
                                       <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                          <tbody>
                                             <tr style=""box-sizing:border-box!important"">
                                                <td style=""width:2.188rem;margin:0px;padding:0 0 1.2rem 0;background-color:#fff;text-align:center;color:#333333;font-size:18px;line-height:18px;font-weight:bold;letter-spacing:0.1875rem"">RESUMEN DE TU COMPRA</td>
                                             </tr>
                                          </tbody>
                                       </table>
                                    </td>
                                    <td style=""width:2.188rem;height:3rem;background-color:#fff"">&nbsp;</td>
                                 </tr>
                                 <tr style=""box-sizing:border-box!important"">
                                    <td style=""width:1.25rem;margin:0px;padding:0px"">&nbsp;</td>
                                    <td style=""width:35rem;margin:0px;padding:0px;vertical-align:middle;text-align:center"">
                                       <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                          <tbody>
                                             <tr style=""box-sizing:border-box!important"">
                                                <td style=""width:17.19rem;margin:0px;padding:0.5rem 0;text-align:left;color:#333333;font-size:16px;line-height:20px;font-weight:normal;vertical-align:top"">
                                                   <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                                      <tbody>
                                                         <tr style=""box-sizing:border-box!important"">
                                                            <td style=""width:8.25rem;margin:0px;padding:0.4rem 0;color:#333333;text-align:left;color:#333333;font-size:13px;line-height:15px;font-weight:normal;vertical-align:top"">Nombre del títular:<br> <strong>{1}</strong> </td>
                                                         </tr>
                                                         <tr style=""box-sizing:border-box!important"">
                                                            <td style=""width:8.25rem;margin:0px;padding:0.4rem 0;color:#333333;text-align:left;color:#333333;font-size:13px;line-height:15px;font-weight:normal;vertical-align:top"">Documento de identidad:<br> <strong style=""text-transform:uppercase"">dni : {3}</strong> </td>
                                                         </tr>
                                                      </tbody>
                                                   </table>
                                                </td>
                                                <td style=""width:0.625rem;margin:0px;padding:0"">&nbsp;</td>
                                                <td style=""width:17.19rem;margin:0px;padding:0.5rem 0;background-color:#fff;text-align:left;color:#333333;font-size:16px;line-height:20px;font-weight:normal;vertical-align:top"">
                                                   <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                                      <tbody>
                                                         <tr style=""box-sizing:border-box!important"">
                                                            <td style=""width:8.25rem;margin:0px;padding:0.4rem 0;color:#333333;text-align:left;color:#333333;font-size:13px;line-height:15px;font-weight:normal;vertical-align:top"">Número de pedido: <br> <strong>{2}</strong> </td>
                                                         </tr>
                                                         <tr style=""box-sizing:border-box!important"">
                                                            <td style=""width:8.25rem;margin:0px;padding:0.4rem 0;color:#333333;text-align:left;color:#333333;font-size:13px;line-height:15px;font-weight:normal;vertical-align:top"">Fecha de compra: <br> <strong> {4} </strong> </td>
                                                         </tr>
                                                      </tbody>
                                                   </table>
                                                </td>
                                             </tr>
                                          </tbody>
                                       </table>
                                    </td>
                                    <td style=""width:1.25rem;margin:0px;padding:0px"">&nbsp;</td>
                                 </tr>
                                 <tr style=""box-sizing:border-box!important"">
                                    <td style=""width:1.25rem;margin:0px;padding:0px;background-color:#fff"">&nbsp;</td>
                                    <td style=""width:35rem;height:0.5rem;margin:0px;padding:0px;background-color:#fff;border-bottom:solid 1px #d3d3d3"">&nbsp;</td>
                                    <td style=""width:1.25rem;margin:0px;padding:0px;background-color:#fff"">&nbsp;</td>
                                 </tr>
                              </tbody>
                           </table>
                        </td>
                     </tr>
                     <tr style=""box-sizing:border-box!important;background-color:#fff"">
                        <td>
                           <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                              <tbody>
                                 <tr style=""box-sizing:border-box!important"">
                                    <td style=""width:2.188rem;height:2rem;margin:0px;padding:0px;background-color:#fff"" colspan=""3"">&nbsp;</td>
                                 </tr>
                                 <tr style=""box-sizing:border-box!important"">
                                    <td style=""width:2.188rem;background-color:#fff"">&nbsp;</td>
                                    <td style=""width:33.13rem;background-color:#fff;vertical-align:middle;text-align:center"">
                                       <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                          <tbody>
                                             <tr style=""box-sizing:border-box!important"">
                                                <td style=""width:2.188rem;margin:0px;padding:0 0 1.2rem 0;background-color:#fff;text-align:center;color:#333333;font-size:18px;line-height:18px;font-weight:bold;letter-spacing:0.1875rem"">DETALLES DE TU PAGO</td>
                                             </tr>
                                          </tbody>
                                       </table>
                                    </td>
                                    <td style=""width:2.188rem;height:3rem;background-color:#fff"">&nbsp;</td>
                                 </tr>
                                 <tr style=""box-sizing:border-box!important"">
                                    <td style=""width:1.25rem;margin:0px;padding:0px"">&nbsp;</td>
                                    <td style=""width:35rem;margin:0px;padding:0px;vertical-align:middle;text-align:center"">
                                       <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                          <tbody>
                                             <tr style=""box-sizing:border-box!important"">
                                                <td >
                                                   <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                                      <tbody>
                                                         <tr style=""box-sizing:border-box!important"">
                                                            <td style=""
                                                               text-align: right;
                                                               "">Medio de pago: </td>
                                                            <td style=""width:1rem"">&nbsp;</td>
                                                            <td style=""padding:0.4rem 0;text-align:left;line-height:15px;""><strong>    {5}     </strong></td>
                                                         </tr>
                                                         <tr style=""box-sizing:border-box!important;background-color:#e9e9e9"">
                                                            <td style=""width:14.5rem;margin:0px;padding:0.4rem 0;color:#333333;text-align:right;color:#333333;font-size:13px;line-height:15px;font-weight:normal""> Sub total:    </td>
                                                            <td style=""width:1rem"">&nbsp;</td>
                                                            <td style=""width:14.5rem;margin:0px;padding:0.4rem 0;color:#333333;text-align:left;color:#333333;font-size:13px;line-height:15px;font-weight:normal"">S/ {6}</td>
                                                         </tr>
                                                         <tr style=""box-sizing:border-box!important;background-color:#e9e9e9"">
                                                            <td style=""width:14.5rem;margin:0px;padding:0.4rem 0;color:#333333;text-align:right;color:#333333;font-size:13px;line-height:15px;font-weight:normal"">  Envío:   </td>
                                                            <td style=""width:1rem"">&nbsp;</td>
                                                            <td style=""width:14.5rem;margin:0px;padding:0.4rem 0;color:#333333;text-align:left;color:#333333;font-size:13px;line-height:15px;font-weight:normal"">S/ {7}</td>
                                                         </tr>
                                                         <tr style=""box-sizing:border-box!important;background-color:#fff"">
                                                            <td style=""width:14.5rem;margin:0px;padding:0.4rem 0;color:#333333;text-align:right;color:#333333;font-size:16px;line-height:16px;font-weight:normal""><strong style=""color:#fb3449"">TOTAL:</strong> </td>
                                                            <td style=""width:1rem"">&nbsp;</td>
                                                            <td style=""width:14.5rem;margin:0px;padding:0.4rem 0;color:#333333;text-align:left;color:#333333;font-size:16px;line-height:16px;font-weight:normal""><strong style=""margin:0 0 0 0.1rem"">S/ {8}</strong></td>
                                                         </tr>
                                                      </tbody>
                                                   </table>
                                                </td>
                                             </tr>
                                          </tbody>
                                       </table>
                                    </td>
                                    <td style=""width:1.25rem;margin:0px;padding:0px"">&nbsp;</td>
                                 </tr>
                              </tbody>
                           </table>
                        </td>
                     </tr>
                     <tr style=""box-sizing:border-box!important"">
                        <td style=""height:2rem""></td>
                     </tr>
                     <tr style=""box-sizing:border-box!important"">
                        <td style=""width:100%;margin:0px;padding:0px;text-align:center""> <img src=""https://ci6.googleusercontent.com/proxy/pb-7053yZ7yptMEAv9TpvcKn_0nPEl6FJYeLS_BgNU687Jz6FkXWFDGCtuJXwCQ0zCiZGC0H23dGex4ZbWAquM6I9WXk1Fz8Af3Op-dAh9cdv_jERaK7IpSpHg=s0-d-e1-ft#http://oechsle.vteximg.com.br/arquivos/mail-despacho-a-domicilio-tit.png"" style=""vertical-align:midddle;outline:none;text-decoration:none;border:none"" class=""CToWUd""> </td>
                     </tr>
                     <tr style=""box-sizing:border-box!important;background-color:#fff"">
                        <td>
                           <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                              <tbody>
                                 <tr style=""box-sizing:border-box!important"">
                                    <td style=""width:2.188rem;height:3rem;margin:0px;padding:0px;background-color:#fff"" colspan=""3"">&nbsp;</td>
                                 </tr>
                                 <tr style=""box-sizing:border-box!important"">
                                    <td style=""width:2.188rem;background-color:#fff"">&nbsp;</td>
                                    <td style=""width:33.13rem;background-color:#fff;vertical-align:middle;text-align:center"">
                                       <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                          <tbody>
                                             <tr style=""box-sizing:border-box!important"">
                                                <td style=""width:2.188rem;margin:0px;padding:0 0 1.2rem 0;background-color:#fff;text-align:center;color:#333333;font-size:18px;line-height:18px;font-weight:bold;letter-spacing:0.1875rem"">DATOS DE LA ENTREGA</td>
                                             </tr>
                                          </tbody>
                                       </table>
                                    </td>
                                    <td style=""width:2.188rem;height:3rem;background-color:#fff"">&nbsp;</td>
                                 </tr>
                                 <tr style=""box-sizing:border-box!important"">
                                    <td style=""width:1.25rem;margin:0px;padding:0px"">&nbsp;</td>
                                    <td style=""width:35rem;margin:0px;padding:0px;vertical-align:middle;text-align:center"">
                                       <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                          <tbody>
                                             <tr style=""box-sizing:border-box!important"">
                                                <td style=""margin:0px;padding:0.4rem 0 0.8rem;color:#333333;text-align:center;color:#333333;font-size:13px;line-height:15px;font-weight:normal;vertical-align:top"" colspan=""3"">Fecha de entrega aprox: <strong>  {9}  </strong> </td>
                                             </tr>
                                             
                                             <tr style=""box-sizing:border-box!important"">
                                                <td style=""width:17.19rem;margin:0px;padding:1rem 0;text-align:left;color:#333333;font-size:16px;line-height:20px;font-weight:normal;vertical-align:top"">
                                                   <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                                      <tbody>
                                                         <tr style=""box-sizing:border-box!important"">
                                                            <td style=""width:8.25rem;height:3rem;margin:0px;padding:0.4rem 0;color:#333333;text-align:left;color:#333333;font-size:13px;line-height:15px;font-weight:normal;vertical-align:top"">Punto de Entrega:<br> <strong>     {10}     </strong> </td>
                                                         </tr>
                                                         <tr style=""box-sizing:border-box!important"">
                                                            <td style=""width:8.25rem;margin:0px;padding:0.4rem 0;color:#333333;text-align:left;color:#333333;font-size:13px;line-height:15px;font-weight:normal;vertical-align:top"">Persona autorizada para recibir el pedido:<br> <strong>  {11}  </strong> </td>
                                                         </tr>
                                                      </tbody>
                                                   </table>
                                                </td>
                                                <td style=""width:0.625rem;margin:0px;padding:0"">&nbsp;</td>
                                                <td style=""width:17.19rem;margin:0px;padding:1rem 0;background-color:#fff;text-align:left;color:#333333;font-size:16px;line-height:20px;font-weight:normal;vertical-align:top"">
                                                   <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                                      <tbody>
                                                         <tr style=""box-sizing:border-box!important"">
                                                            <td style=""width:8.25rem;height:3rem;margin:0px;padding:0.4rem 0;color:#333333;text-align:left;color:#333333;font-size:13px;line-height:15px;font-weight:normal;vertical-align:top"">Horario:<br> <strong>    09:00 a 20:00 horas    </strong> </td>
                                                         </tr>
                                                         <tr style=""box-sizing:border-box!important"">
                                                            <td style=""width:8.25rem;margin:0px;padding:0.4rem 0;color:#333333;text-align:left;color:#333333;font-size:13px;line-height:15px;font-weight:normal;vertical-align:top"">Documento de identidad:<br> <strong style=""text-transform:uppercase"">  dni : {12}  </strong> </td>
                                                         </tr>
                                                      </tbody>
                                                   </table>
                                                </td>
                                             </tr>
                                          </tbody>
                                       </table>
                                    </td>
                                    <td style=""width:1.25rem;margin:0px;padding:0px"">&nbsp;</td>
                                 </tr>
                                 <tr style=""box-sizing:border-box!important"">
                                    <td style=""width:1.25rem;margin:0px;padding:0px;background-color:#fff"">&nbsp;</td>
                                    <td style=""width:35rem;height:0.5rem;margin:0px;padding:0px;background-color:#fff"">&nbsp;</td>
                                    <td style=""width:1.25rem;margin:0px;padding:0px;background-color:#fff"">&nbsp;</td>
                                 </tr>
                                 <tr style=""box-sizing:border-box!important"">
                                    <td style=""width:1.25rem;margin:0px;padding:0px;background-color:#fff"">&nbsp;</td>
                                    <td style=""width:35rem;height:0.5rem;margin:0px;padding:0px;background-color:#fff;text-align:right;vertical-align:bottom""> <span style=""padding:0.5rem 0.5rem 0.9rem;display:inline-block"">Esta compra es despachada por:</span> <span style=""color:#fb3449;font-weight: bold;font-style: italic;"">{14}</span></td>
                                    <td style=""width:1.25rem;margin:0px;padding:0px;background-color:#fff"">&nbsp;</td>
                                 </tr>
                                 <tr style=""box-sizing:border-box!important"">
                                    <td style=""width:1.25rem;margin:0px;padding:0px;background-color:#fff"">&nbsp;</td>
                                    <td style=""width:35rem;height:0.5rem;margin:0px;padding:0px;background-color:#fff"">
                                       <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                          <thead>
                                             <tr>
                                                <th style=""width:6.875rem;margin:0px;padding:0.7rem 0;color:#333333;text-align:center;color:#333333;font-size:13px;line-height:15px;font-weight:normal;background-color:#f0f0f0""><strong>Productos</strong></th>
                                                <th style=""width:17.75rem;margin:0px;padding:0.7rem 0;color:#333333;text-align:right;color:#333333;font-size:13px;line-height:15px;font-weight:normal;background-color:#f0f0f0"">&nbsp;</th>
                                                <th style=""width:6.25rem;margin:0px;padding:0.7rem 0;color:#333333;text-align:center;color:#333333;font-size:13px;line-height:15px;font-weight:normal;background-color:#f0f0f0""><strong>Precio</strong></th>
                                                <th style=""width:4.125rem;margin:0px;padding:0.7rem 0;color:#333333;text-align:center;color:#333333;font-size:13px;line-height:15px;font-weight:normal;background-color:#f0f0f0""><strong>Cantidad</strong></th>
                                                <th style=""width:6.25rem;margin:0px;padding:0.7rem 0;color:#333333;text-align:center;color:#333333;font-size:13px;line-height:15px;font-weight:normal;background-color:#f0f0f0""><strong>Total</strong></th>
                                             </tr>
                                          </thead>
                                          <tbody>
                                            {13}
                                          </tbody>
                                       </table>
                                    </td>
                                    <td style=""width:1.25rem;margin:0px;padding:0px;background-color:#fff"">&nbsp;</td>
                                 </tr>
                                 <tr style=""box-sizing:border-box!important"">
                                    <td style=""width:2.188rem;height:2rem;margin:0px;padding:0px;background-color:#fff"" colspan=""3"">&nbsp;</td>
                                 </tr>
                              </tbody>
                           </table>
                        </td>
                     </tr>
                     <tr style=""box-sizing:border-box!important"">
                        <td style=""width:100%;height:66px;margin:0px;padding:0px;text-align:center; background: #fb3449;""> 
                            <a href=""tel:{19}"" style=""border:none;outline:none;display:block;font-size:18px;text-decoration:inherit;color:white;"">
                                    <label>Dudas? LLámanos al <b>{19}</b></label></a></td>
                     </tr>
                     <tr style=""box-sizing:border-box!important"">
                        <td style=""width:100%;margin:0px;padding:0px;text-align:center;height: 66px;background: #fb3449;border: 1px solid white;"">
                                <a href=""https://m.me/{20}"" style=""border:none;outline:none;display:block;float:left;background: #fb3449;width: 50%;text-decoration: inherit;color: white;"" target=""_blank"">
                                      <label>Chatea con Nosotros<br><i>Facebook Messenger</i></label></a>
                                <a href=""mailto:{21}""  style=""border:none;outline:none;display:block;float:left;background: #fb3449;width: 50%;text-decoration: inherit;color: white;"" target=""_blank"">
                                       <label>Envíanos un correo<br><i>{21}</i></label></a>
                        </td>
                     </tr>
                     <tr style=""box-sizing:border-box!important"">
                        <td style=""width:100%;margin:0px;padding:0px;text-align:center"">
                           <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                              <tbody>
                                 <tr style=""box-sizing:border-box!important"">
                                    <td style=""width:0.625rem;background-color:#fff"">&nbsp;</td>
                                    <td style=""width:36.25rem;background-color:#fff;vertical-align:middle;text-align:center"">
                                       <table width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                                          <tbody>
                                             <tr style=""box-sizing:border-box!important"">
                                                <td style=""width:8.75rem;margin:0px;padding:2rem 0 0.8rem 0;background-color:#fff;text-align:center;border-right:solid 1px #d7d7d7;vertical-align:top"">
                                                   <img src=""https://ci5.googleusercontent.com/proxy/e3Vzm2JB5SXxtnC9lWZti63a6nECbuRlqH-nOpM8qLlhDUQpnEGpcINFyIQ2mHi6UgKv3klb1lNEQwjwalhefo76NqIK6HSDbEAhjM-4TlVQYZrJ62gLpw1aiH9IR4c=s0-d-e1-ft#http://oechsle.vteximg.com.br/arquivos/mail-ico-satisfaccion-garantizada.png"" style=""vertical-align:midddle;outline:none;text-decoration:none;border:none"" class=""CToWUd""> 
                                                   <h3 style=""color:#333333;font-size:14px;line-height:16px;font-weight:bold;padding:0.5rem 0 0.3rem;margin:0px"">Satisfacción garantizada</h3>
                                                </td>
                                                <td style=""width:9.375rem;margin:0px;padding:2rem 0 0.8rem 0;background-color:#fff;text-align:center;border-right:solid 1px #d7d7d7;vertical-align:top"">
                                                   <img src=""https://ci6.googleusercontent.com/proxy/eLKIR9uZ_-CjnE0ht1eHI19mYvpjeRt_Aht6Q8mwClKW31jvP3uvDWDCzi4wXIHgLx39PPFjp0yzwbZrBti_P2aiyRmksJovDdgvduDnQ4InQ83GXkM=s0-d-e1-ft#http://oechsle.vteximg.com.br/arquivos/mail-ico-cambia-devuelve.png"" style=""vertical-align:midddle;outline:none;text-decoration:none;border:none"" class=""CToWUd""> 
                                                   <h3 style=""color:#333333;font-size:14px;line-height:16px;font-weight:bold;padding:0.5rem 0 0.3rem;margin:0px"">Cambia o devuelve</h3>
                                                   <p style=""color:#333333;font-size:11px;line-height:12px;font-weight:normal;padding:0.1rem 0;margin:0px""><a href=""/Apptv/politicasDevolucion?name={23}"" style=""color:#333;text-decoration:underline;display:block"" rel=""noreferrer noreferrer noreferrer"" target=""_blank"">Conoce nuestras políticas</a></p>
                                                </td>
                                                <td style=""display:none;width:9.063rem;margin:0px;padding:2rem 0 0.8rem 0;background-color:#fff;text-align:center;border-right:solid 1px #d7d7d7;vertical-align:top"">
                                                   <img src=""https://ci3.googleusercontent.com/proxy/zVq9DLmlLEU4YMYqAo9u5VEluLX-NK5HfQfJ-Ugz8EqTHXbMSfGsBB70fTELKvOQbZZxr9EacvQ6c6CWwBypXd6aXPKq-LTyjxBW2ABR-PIQIK8=s0-d-e1-ft#http://oechsle.vteximg.com.br/arquivos/mail-ico-envio-gratis.png"" style=""vertical-align:midddle;outline:none;text-decoration:none;border:none"" class=""CToWUd""> 
                                                   <h3 style=""color:#333333;font-size:14px;line-height:16px;font-weight:bold;padding:0.5rem 0 0.3rem;margin:0px"">Envíos Gratis</h3>
                                                   <p style=""color:#333333;font-size:11px;line-height:12px;font-weight:normal;padding:0.1rem 0;margin:0px"">por compras mayores a <strong>s/599</strong></p>
                                                </td>
                                                <td style=""width:9.063rem;margin:0px;padding:2rem 0 0.8rem 0;background-color:#fff;text-align:center;vertical-align:top"">
                                                   <img src=""https://ci6.googleusercontent.com/proxy/zq7othCy5z0UAXpz8AkuGWZlAomVgpZEfm0NBnxmtms5X2eZ9OYtAg8pTBXS05dws5dWTQbdX-0uPAfIFg6CMtnEZTZYCYyTwgiEekgKtoA=s0-d-e1-ft#http://oechsle.vteximg.com.br/arquivos/mail-ico-visitanos.png"" style=""vertical-align:midddle;outline:none;text-decoration:none;border:none"" class=""CToWUd""> 
                                                   <h3 style=""color:#333333;font-size:14px;line-height:16px;font-weight:bold;padding:0.5rem 0 0.3rem;margin:0px"">Visítanos</h3>
                                                   <p style=""color:#333333;font-size:11px;line-height:12px;font-weight:normal;padding:0.1rem 0;margin:0px""><a href=""{22}"" style=""color:#333;text-decoration:underline;display:block"" target=""_blank"" >Conoce nuestras tiendas</a></p>
                                                </td>
                                             </tr>
                                          </tbody>
                                       </table>
                                    </td>
                                    <td style=""width:0.625rem;height:3rem;background-color:#fff"">&nbsp;</td>
                                 </tr>
                              </tbody>
                           </table>
                        </td>
                     </tr>
                     <tr style=""box-sizing:border-box!important"">
                        <td style=""width:100%;height:3rem;margin:0px;padding:2rem 1.6rem 0;background-color:#fff"">
                           <p style=""color:#4f4f4f;font-size:14px;text-align:left;margin:0px;padding:0 0 0.5rem""><strong>Para asegurar que recibas nuestros correos agrega esta dirección a tu lista de correos seguros.</strong></p>
                           <p style=""color:#4f4f4f;font-size:13px;text-align:left;margin:0px;padding:0 0 0.5rem"">Todos los pedidos están sujetos a la confirmación de disponibilidad de stock. De no encontrar el producto en stock, serás notificado a través de un mensaje de correo electrónico con el detalle final de tu orden.El cumplimiento de la fecha de entrega de tu pedido está sujeto a la aprobación del medio de pago utilizado, este puede tomar hasta 02 días según tu entidad bancaria. Te pedimos revisar con tu entidad bancaria su política de pagos online.</p>
                        </td>
                     </tr>
                     <tr style=""box-sizing:border-box!important"">
                        <td style=""height:1rem""></td>
                     </tr>
                  </tbody>
               </table>
            </td>
         </tr>
      </tbody>
   </table>";
            #endregion
        }
        public static string getImageEstadoCompra(int? idestadocompra)
        {
            switch (idestadocompra)
            {
                case Constantes.EstadoCompra.EnValidacion: return "https://firebasestorage.googleapis.com/v0/b/e-commerce-c6d82.appspot.com/o/productos%2Fsisadri%2FValidacion.png?alt=media&token=7b52089c-cf67-407a-ab53-52bb4fa3a12a";
                case Constantes.EstadoCompra.Confirmado: return "https://firebasestorage.googleapis.com/v0/b/e-commerce-c6d82.appspot.com/o/productos%2Fsisadri%2FEnCamino.png?alt=media&token=f52dae4e-61ba-4010-aa89-f8cbfe2eceb8";
                case Constantes.EstadoCompra.EnCaminoEntrega: return "https://firebasestorage.googleapis.com/v0/b/e-commerce-c6d82.appspot.com/o/productos%2Fsisadri%2FEnCamino.png?alt=media&token=f52dae4e-61ba-4010-aa89-f8cbfe2eceb8";
                case Constantes.EstadoCompra.Entregado: return "https://firebasestorage.googleapis.com/v0/b/e-commerce-c6d82.appspot.com/o/productos%2Fsisadri%2FEntregado.png?alt=media&token=c745b15d-bf00-4edd-b696-7fc52dca543e";
            }

            return "";
        }
        public static DateTime? StringToDateTime(string dateString)
        {
            try
            {
                DateTime? dateValue = null;
                dateValue = new DateTime(int.Parse(dateString.Split('/')[2]), int.Parse(dateString.Split('/')[1]), int.Parse(dateString.Split('/')[0]), 0, 0, 0);
                return dateValue;
            }
            catch (Exception ex)
            {
                throw new Exception("La fecha no es correcta " + ex);
            }
        }

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        public static string UploadFileCloud(string fullpath, string namefile)
        {
            try
            {
                var uriFileCloud = string.Empty;

                /*CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
        CloudConfigurationManager.GetSetting("StorageConnectionString"));

                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve a reference to a container.
                CloudBlobContainer container = blobClient.GetContainerReference("denti");

                // Create the container if it doesn't already exist.
                container.CreateIfNotExists();

                // Create or overwrite the "myblob" blob with contents from a local file.
                using (var fileStream = System.IO.File.OpenRead(fullpath))
                {
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(namefile);
                    blockBlob.UploadFromStream(fileStream);
                    uriFileCloud = blockBlob.Uri.ToString();
                }*/
                return uriFileCloud;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public static class JsonHelper
    {

        public static T DeserializeObject<T>(string objString)
        {
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(objString)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(stream);
            }
        }

    }


    public class Encrypting
    {
        public static string EncryptKey(string cadena)
        {
            string key = "ABCDEFG54669525PQRSTUVWXYZabcdef852846opqrstuvwxyz";
            //arreglo de bytes donde guardaremos la llave
            byte[] keyArray;
            //arreglo de bytes donde guardaremos el texto
            //que vamos a encriptar
            byte[] Arreglo_a_Cifrar =
            UTF8Encoding.UTF8.GetBytes(cadena);

            //se utilizan las clases de encriptación
            //provistas por el Framework
            //Algoritmo MD5
            MD5CryptoServiceProvider hashmd5 =
            new MD5CryptoServiceProvider();
            //se guarda la llave para que se le realice
            //hashing
            keyArray = hashmd5.ComputeHash(
            UTF8Encoding.UTF8.GetBytes(key));

            hashmd5.Clear();

            //Algoritmo 3DAS
            TripleDESCryptoServiceProvider tdes =
            new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            //se empieza con la transformación de la cadena
            ICryptoTransform cTransform =
            tdes.CreateEncryptor();

            //arreglo de bytes donde se guarda la
            //cadena cifrada
            byte[] ArrayResultado =
            cTransform.TransformFinalBlock(Arreglo_a_Cifrar,
            0, Arreglo_a_Cifrar.Length);

            tdes.Clear();

            //se regresa el resultado en forma de una cadena


            return Convert.ToBase64String(ArrayResultado,
                   0, ArrayResultado.Length).Replace('+', '-').Replace('/', '_');

        }

        public static string DecryptKey(string clave)
        {
            string key = "ABCDEFG54669525PQRSTUVWXYZabcdef852846opqrstuvwxyz";
            byte[] keyArray;
            //convierte el texto en una secuencia de bytes
            byte[] Array_a_Descifrar =
            Convert.FromBase64String(clave.Replace('-', '+').Replace('_', '/'));

            //se llama a las clases que tienen los algoritmos
            //de encriptación se le aplica hashing
            //algoritmo MD5
            MD5CryptoServiceProvider hashmd5 =
            new MD5CryptoServiceProvider();

            keyArray = hashmd5.ComputeHash(
            UTF8Encoding.UTF8.GetBytes(key));

            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes =
            new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform =
             tdes.CreateDecryptor();

            byte[] resultArray =
            cTransform.TransformFinalBlock(Array_a_Descifrar,
            0, Array_a_Descifrar.Length);

            tdes.Clear();

            //se regresa en forma de cadena
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }

}
