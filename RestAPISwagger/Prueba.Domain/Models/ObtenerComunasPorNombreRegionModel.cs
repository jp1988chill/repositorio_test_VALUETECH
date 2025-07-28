using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ObtenerComunasPorNombreRegionModel
    {
        //Lógica Microservicio...
        public List<Comuna> ObtenerComunasPorNombreRegiones(string NombreRegion, IRepositoryEntityFrameworkCQRS<Region> regionRepository, IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository){
            var regionEncontrada = regionRepository.GetAll().Where(id => id.region == NombreRegion).ToList().FirstOrDefault();
            int idRegion = -1;
            if(regionEncontrada != null)
            {
                idRegion = regionEncontrada.Idregion;
            }
            return comunaRepository.GetAll().Where(id => id.Idregion == idRegion).ToList();
        }
        public async Task<ComunaResponse> ObtenerComunasPorNombreRegion(string NombreUsuario, IRepositoryEntityFrameworkCQRS<Region> regionRepository, IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            List<Comuna> Tarjeta = ObtenerComunasPorNombreRegiones(NombreUsuario, regionRepository, comunaRepository);
            if (Tarjeta == null) {
                httpCod = 400;
                httpMsg = "No hay tarjeta(s) asociadas al TarjetaHabiente:("+NombreUsuario+")";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            ComunaResponse bodyResponse = new ComunaResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                comunaFriendlyError = usrFriendlyErr,
                comunaNuevoTokenAsignado = Tarjeta
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
