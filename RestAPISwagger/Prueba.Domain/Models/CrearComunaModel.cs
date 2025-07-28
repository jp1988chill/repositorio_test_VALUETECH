using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class CrearComunaModel
    {
        //Lógica Microservicio...
        public bool CrearComunas(List<Comuna> comunas, IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository){

            comunaRepository.InsertMany(comunas);
            if (comunaRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<ComunaResponse> CrearComuna(ComunaBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<Comuna> cardRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (CrearComunas(objBodyObjectRequest.Comunas, cardRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al ingresar tarjetas";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            ComunaResponse bodyResponse = new ComunaResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                comunaFriendlyError = usrFriendlyErr,
                comunaNuevoTokenAsignado = objBodyObjectRequest.Comunas
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
