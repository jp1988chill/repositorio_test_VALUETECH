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
    public class ObtenerRegionesCommandHandler : IRequestHandler<ObtenerRegionesCommand, RegionResponse>
    {
        private IRepositoryEntityFrameworkCQRS<Region> userRepository = null;
        public ObtenerRegionesCommandHandler(PruebaContext pruebaContext)
        {
            userRepository = new RepositoryEntityFrameworkCQRS<Region>(pruebaContext);

            //Mapear y crear BD desde Modelo EF Core a base de datos real, si no existe. (Requerido por EF Core)
            // Drop the database if it exists
            //pruebaContext.Database.EnsureDeleted();
            // Create the database if it doesn't exist
            pruebaContext.Database.EnsureCreated();
        }

        public async Task<RegionResponse> Handle(ObtenerRegionesCommand request, CancellationToken cancellationToken)
        {
            var middleWareHandler = new ObtenerRegionModel();
            var middleWareHandlerResponse = (await middleWareHandler.ObtenerRegion(userRepository));
            return middleWareHandlerResponse;
        }
    }
}
