using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ObtenerRegionModel
    {
        //Lógica Microservicio...
        public List<Region> ObtenerRegionesPorSP(IRepositoryEntityFrameworkCQRS<Region> regionRepository){
            return regionRepository.GetAllFromSP($"[dbo].[ObtenerRegiones_SP] ");
        }

        public async Task<RegionResponse> ObtenerRegion(IRepositoryEntityFrameworkCQRS<Region> regionRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            List<Region> regiones = ObtenerRegionesPorSP(regionRepository);
            if (regiones == null) {
                httpCod = 400;
                httpMsg = "No existe(n) Region(es)";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            RegionResponse bodyResponse = new RegionResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                regionFriendlyError = usrFriendlyErr,
                regionesNuevoTokenAsignado = regiones
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
