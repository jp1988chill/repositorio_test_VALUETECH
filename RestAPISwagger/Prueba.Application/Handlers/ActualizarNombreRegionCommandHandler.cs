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
    public class ActualizarNombreRegionCommandHandler : IRequestHandler<ActualizarNombreRegionCommand, RegionResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Region> regionRepository = null;
        private IRepositoryEntityFrameworkCQRS<Comuna> comunaRepository = null;

        public ActualizarNombreRegionCommandHandler(PruebaContext pruebaContext)
        {
            comunaRepository = new RepositoryEntityFrameworkCQRS<Comuna>(pruebaContext);
            regionRepository = new RepositoryEntityFrameworkCQRS<Region>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();
            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<RegionResponse> Handle(ActualizarNombreRegionCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ActualizarNombreRegionModel();
            var middleWareHandlerResponse = (await middleWareHandler.ActualizarNombreRegion(request.NombreRegionOriginal, request.NombreRegionNuevo, comunaRepository, regionRepository));
            return middleWareHandlerResponse;
        }
    }
}
