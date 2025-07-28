using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prueba.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prueba.Application.Commands
{
    public class ObtenerRegionesCommand : IRequest<RegionResponse>
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
