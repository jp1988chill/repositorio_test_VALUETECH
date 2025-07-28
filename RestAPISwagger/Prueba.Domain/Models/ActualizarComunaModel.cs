using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ActualizarComunaModel
    {
        //Lógica Microservicio...
        public bool ActualizarComunas(List<Comuna> comunas, IRepositoryEntityFrameworkCQRS<Comuna> cardRepository){
            foreach (Comuna comu in comunas) {
                Comuna thisComu = cardRepository.GetByID(comu.Idcomuna);
                if ((thisComu != null) && (comu.Idcomuna == thisComu.Idcomuna)) {
                    thisComu.Idcomuna = comu.Idcomuna;
                    thisComu.Idregion = comu.Idregion;
                    thisComu.comuna = comu.comuna;
                    thisComu.informacionadicional = comu.informacionadicional;
                    cardRepository.Update(thisComu);
                }
            }
            if (cardRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<ComunaResponse> ActualizarComuna(ComunaBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";
            List<Comuna> ComunasPorActualizar = objBodyObjectRequest.Comunas;
            if (ActualizarComunas(ComunasPorActualizar, comunaRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al ingresar comunas";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
                ComunasPorActualizar = new List<Comuna>();
            }

            ComunaResponse bodyResponse = new ComunaResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                comunaFriendlyError = usrFriendlyErr,
                comunaNuevoTokenAsignado = ComunasPorActualizar
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
