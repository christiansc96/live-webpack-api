using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebpackAPI.DB;
using WebpackAPI.Models;

namespace WebpackAPI.Controllers
{
    public class TalleresController : ApiController
    {
        private DevTeam504Entities context = new DevTeam504Entities();

        [HttpGet]
        public async Task<IHttpActionResult> GetTalleres()
        {
            var talleres = await context.Talleres.ToListAsync();
            return await Task.FromResult(Ok(talleres));
        }

        //[HttpGet("{id}")]
        //public async Task<IHttpActionResult> GetTaller(int id)
        //{
        //    var talleres = await context.Talleres.FindAsync(id);
        //    if (talleres == null)
        //    {
        //        return await Task.FromResult(NotFound());
        //    }
        //    return await Task.FromResult(Ok(talleres));
        //}

        [HttpPost]
        public async Task<IHttpActionResult> PostEmail(EmailDTO email)
        {
            if (email == null)
            {
                return await Task.FromResult(BadRequest());
            }

            var bodyEmail = "<p>Hola " + email.Name + "! <br/><br/> DevTeam504 WebApp te informa que hemos " +
            "recibido tu mensaje. <br/><br/> Cuerpo del Mensaje: " + email.Message + " <br/> Fecha del Mensaje: "
            + DateTime.Now + " <br/><br/> Su mensaje fue remitido a las personas encargadas. <br/> Pronto" +
            " nos pondremos en contacto con usted. <br/>Saludos!</p>";

            MailMessage correo = new MailMessage
            {
                Subject = "Nuevo Mensaje DevTeam504",
                Body = bodyEmail,
                IsBodyHtml = true,
                BodyEncoding = System.Text.Encoding.UTF8,
                SubjectEncoding = System.Text.Encoding.Default,
                From = new MailAddress("catrachosdev@gmail.com")
            };

            correo.To.Add(new MailAddress(email.Email ?? "christian.sanchez@cit.hn"));
            SmtpClient mail = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("catrachosdev@gmail.com", "Honduras.123")
            };

            try
            {
                await mail.SendMailAsync(correo);
            }
            catch
            {
                return await Task.FromResult(BadRequest());
            }
            return await Task.FromResult(Ok());
        }
    }
}