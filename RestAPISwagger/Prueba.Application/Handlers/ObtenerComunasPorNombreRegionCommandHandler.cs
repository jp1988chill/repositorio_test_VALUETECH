using MediatR;
using Prueba.Application.Commands;
using Prueba.Domain;
using Prueba.Domain.Models;
using Prueba.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Prueba.Application.Handlers
{
    public class ObtenerComunasPorNombreRegionCommandHandler : IRequestHandler<ObtenerComunasPorNombreRegionCommand, ComunaResponse>
    {

        private IRepositoryEntityFrameworkCQRS<Region> regionRepository = null;
        private IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository = null;
        public ObtenerComunasPorNombreRegionCommandHandler(PruebaContext pruebaContext)
        {
            regionRepository = new RepositoryEntityFrameworkCQRS<Region>(pruebaContext);
            comunaRepository = new RepositoryEntityFrameworkCQRS<Comuna>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();
            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<ComunaResponse> Handle(ObtenerComunasPorNombreRegionCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ObtenerComunasPorNombreRegionModel();
            var middleWareHandlerResponse = (await middleWareHandler.ObtenerComunasPorNombreRegion(request.NombreRegion, regionRepository, comunaRepository));
            return middleWareHandlerResponse;
        }
    }
}
