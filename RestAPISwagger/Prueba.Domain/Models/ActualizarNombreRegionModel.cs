using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ActualizarNombreRegionModel
    {
        //Lógica Microservicio...
        public bool ActualizarNombreRegiones(string NombreRegionOriginal, string NombreRegionNuevo, IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository, IRepositoryEntityFrameworkCQRS<Region> regionesRepository){
            //Por cada registro, lo eliminamos de la BD, y creamos uno nuevo
            List<Comuna> lstComunas = comunaRepository.GetAll().Where(id => id.comuna == NombreRegionOriginal).ToList();
            List<Region> lstRegiones = regionesRepository.GetAll().Where(id => id.region == NombreRegionOriginal).ToList();
            if (lstRegiones.Count > 0) {
                foreach (Region reg in lstRegiones) {
                    
                    var entity = regionesRepository.GetByID(reg.Idregion); // Find entity by primary key
                    if (entity != null)
                    {
                        entity.region = NombreRegionNuevo; // Update the specific field
                        regionesRepository.Save(); // Save changes to the database
                    }

                }
            }
            return true;
        }
        public async Task<RegionResponse> ActualizarNombreRegion(string NombreRegionOriginal, string NombreRegionNuevo, IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository, IRepositoryEntityFrameworkCQRS<Region> userRepository)
        {
            int httpCod = 200;
            string httpMsg = "Nombre de Region actualizado desde ("+ NombreRegionOriginal + ") a (" + NombreRegionNuevo + ") Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            bool OperacionOK = ActualizarNombreRegiones(NombreRegionOriginal, NombreRegionNuevo, comunaRepository, userRepository);
            if (OperacionOK == false) {
                httpCod = 400;
                httpMsg = "No existe el Nombre de Region:("+ NombreRegionOriginal + ")";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            RegionResponse bodyResponse = new RegionResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                regionFriendlyError = usrFriendlyErr
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
