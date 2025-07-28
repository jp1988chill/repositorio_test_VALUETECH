using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ObtenerComunasModel
    {
        //Lógica Microservicio...
        public List<Comuna> ObtenerComunas(IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository){
            return comunaRepository.GetAllFromSP($"[dbo].[ObtenerComunas_SP] ").ToList();
        }
        public async Task<ComunaResponse> ObtenerComuna(IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            List<Comuna> Comuna = ObtenerComunas(comunaRepository);
            if (Comuna == null) {
                httpCod = 400;
                httpMsg = "No existe(n) Comuna(s) ingresada(s)";
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
