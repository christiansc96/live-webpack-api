using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebpackAPI.DB;

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

        // GET: api/Talleres
        //public IHttpActionResult GetTalleres()
        //{
        //    return Ok(db.Talleres);
        //}
    }
}