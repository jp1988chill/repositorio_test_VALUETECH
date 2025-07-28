using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class EliminarRegionModel
    {
        //Lógica Microservicio...
        public bool EliminarRegiones(List<Region> regiones, IRepositoryEntityFrameworkCQRS<Region> userRepository){
            foreach (Region region in regiones)
            {
                Region thisUser = userRepository.Get().Where( it => it.region == region.region).FirstOrDefault();
                if ((thisUser != null) && (region.region == thisUser.region))
                {
                    userRepository.Delete(thisUser);
                }
            }
            if (userRepository.Save() > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<RegionResponse> EliminarRegion(RegionBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<Region> userRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (EliminarRegiones(objBodyObjectRequest.Regiones, userRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al eliminar regiones";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            RegionResponse bodyResponse = new RegionResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                regionFriendlyError = usrFriendlyErr,
                regionesNuevoTokenAsignado = objBodyObjectRequest.Regiones
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
