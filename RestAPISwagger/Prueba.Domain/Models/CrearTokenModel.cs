using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class CrearTokenModel
    {
        //Lógica Microservicio...
        public List<Region> CrearTokenDesdeRegiones(List<Region> users, IRepositoryEntityFrameworkCQRS<Region> userRepository){
            List<Region> usersTokenGenerado = new List<Region>();
            foreach (Region user in users) {
                Region thisUser = userRepository.GetAll().Where(id => id.region == user.region).FirstOrDefault();
                if (thisUser != null)
                {
                    userRepository.Delete(thisUser);
                    userRepository.Save();
                }
                thisUser = new Region(user.Idregion, user.region); //10 minutes lease time
                userRepository.Insert(thisUser);
                userRepository.Save();
                usersTokenGenerado.Add(thisUser);
            }
            return usersTokenGenerado;
        }
        public async Task<TokenResponse> CrearToken(RegionBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<Region> userRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";
            List<Region> lstNuevosTokensPorUsuario = CrearTokenDesdeRegiones(objBodyObjectRequest.Regiones, userRepository);
            if (lstNuevosTokensPorUsuario.Count == 0) {
                httpCod = 400;
                httpMsg = "Error al ingresar tokens. No se generó token alguno para usuario(s)";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            TokenResponse bodyResponse = new TokenResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                UsersNuevoTokenAsignado = lstNuevosTokensPorUsuario
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}
