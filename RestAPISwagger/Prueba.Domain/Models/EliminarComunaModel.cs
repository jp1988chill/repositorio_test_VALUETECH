using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class EliminarComunaModel
    {
        //Lógica Microservicio...
        public bool EliminarComunas(List<Comuna> comunas, IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository){
            foreach (Comuna comu in comunas) {
                Comuna thisComu = comunaRepository.GetByID(comu.Idcomuna);
                if ((thisComu != null) && (comu.Idcomuna == thisComu.Idcomuna)) {
                    comunaRepository.Delete(thisComu);
                }
            }
            if (comunaRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<ComunaResponse> EliminarComuna(ComunaBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<Comuna> cardRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";
            List<Comuna> ComunasPorEliminar = objBodyObjectRequest.Comunas;
            if (EliminarComunas(ComunasPorEliminar, cardRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al eliminar comunas";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
                ComunasPorEliminar = new List<Comuna>();
            }

            ComunaResponse bodyResponse = new ComunaResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                comunaFriendlyError = usrFriendlyErr,
                comunaNuevoTokenAsignado = ComunasPorEliminar
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
