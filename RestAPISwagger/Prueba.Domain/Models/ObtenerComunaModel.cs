using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ObtenerComunaModel
    {
        //Lógica Microservicio...
        public List<Comuna> ObtenerComunasPorSP(string idComuna, IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository){
            return comunaRepository.GetAllFromSP($"[dbo].[ObtenerComunas_SP] ").Where(id => Convert.ToInt32(id.Idcomuna) == Convert.ToInt32(idComuna)).ToList();
        }
        public async Task<ComunaResponse> ObtenerComuna(string idComuna, IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            List<Comuna> Comuna = ObtenerComunasPorSP(idComuna, comunaRepository);
            if (Comuna == null) {
                httpCod = 400;
                httpMsg = "No existe(n) Comuna(s) con ID ("+ idComuna + ")";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            ComunaResponse bodyResponse = new ComunaResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                comunaFriendlyError = usrFriendlyErr,
                comunaNuevoTokenAsignado = Comuna
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
