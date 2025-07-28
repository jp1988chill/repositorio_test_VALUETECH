using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using Prueba.Repository;
using System.Collections.Generic;
using Prueba.Domain;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using Prueba.WebApi.Responses;
using Prueba.Application.Commands;
using System.Threading.Tasks;

namespace Prueba.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Crear una Comuna.
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores Client Secret y Client Id son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/CrearComuna")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpPut]
        [ProducesResponseType(typeof(ComunaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CrearComuna([FromBody] ComunaBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new CrearComunaCommand() { objBodyObjectRequest = objBodyObjectRequest}).ConfigureAwait(false);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Crear Region.
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores Client Secret y Client Id son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/CrearRegion")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //El Token validado depende de esta Region, no se valida acá.
        [HttpPut]
        [ProducesResponseType(typeof(ComunaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CrearRegion([FromBody] RegionBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new CrearRegionCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Elimina Region(es).
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores Client Secret y Client Id son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/EliminarRegion")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //El Token validado depende de esta Region, no se valida acá.
        [HttpDelete]
        [ProducesResponseType(typeof(ComunaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> EliminarRegion([FromBody] RegionBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new EliminarRegionCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Crear un Token.
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores Client Secret y Client Id son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/CrearToken")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] //El Token validado se genera desde este método, no se puede crear dependencia circular
        [HttpPut]
        [ProducesResponseType(typeof(ComunaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CrearToken([FromBody] RegionBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new CrearTokenCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Actualiza una Comuna.
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores Client Secret y Client Id son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ActualizarComuna")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpPut]
        [ProducesResponseType(typeof(ComunaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ActualizarComuna([FromBody] ComunaBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new ActualizarComunaCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
            return Ok(handlerResponse);
        }


        /// <summary>
        /// Elimina una Comuna.
        /// </summary>
        /// <param name="objBodyObjectRequest">Body incluyendo el Array en formato JSON v2</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores Client Secret y Client Id son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/EliminarComuna")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpDelete]
        [ProducesResponseType(typeof(ComunaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> EliminarComuna([FromBody] ComunaBody objBodyObjectRequest)
        {
            var handlerResponse = await _mediator.Send(new EliminarComunaCommand() { objBodyObjectRequest = objBodyObjectRequest }).ConfigureAwait(false);
            return Ok(handlerResponse);
        }


        /// <summary>
        /// Obtiene una Comuna por ID.
        /// </summary>
        /// <param name="saldoPorAgregar">ID de Comuna por obtener.</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores Client Secret y Client Id son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ObtenerComuna/{idComuna}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpGet]
        [ProducesResponseType(typeof(ComunaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObtenerComuna([FromRoute] string idcomuna)
        {
            var handlerResponse = await _mediator.Send(new ObtenerComunaCommand() { idComuna = idcomuna }).ConfigureAwait(false);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Obtiene todas las Comunas asociadas a cualquier User(s).
        /// </summary>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores Client Secret y Client Id son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ObtenerComunas")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")] 
        [HttpPost]
        [ProducesResponseType(typeof(ComunaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObtenerComunas()
        {
            var handlerResponse = await _mediator.Send(new ObtenerComunasCommand() { }).ConfigureAwait(false);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Obtiene todos los User(s) registrados.
        /// </summary>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores Client Secret y Client Id son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ObtenerRegiones")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpGet]
        [ProducesResponseType(typeof(ComunaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObtenerRegiones()
        {
            var handlerResponse = await _mediator.Send(new ObtenerRegionesCommand() { }).ConfigureAwait(false);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Obtiene todas las Comunas por NombreRegion.
        /// </summary>
        /// <param name="nombreRegion">Nombre de Región asociado a una lista de Comunas.</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores Client Secret y Client Id son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ObtenerComunasPorNombreRegion/{nombreRegion}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpPost]
        [ProducesResponseType(typeof(ComunaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObtenerComunasPorNombreRegion([FromRoute] string nombreRegion)
        {
            var handlerResponse = await _mediator.Send(new ObtenerComunasPorNombreRegionCommand() { NombreRegion = nombreRegion }).ConfigureAwait(false);
            return Ok(handlerResponse);
        }

        /// <summary>
        /// Actualiza el NombreRegion a nivel global, incluído en las Comunas que corresponden.
        /// </summary>
        /// <param name="nombreRegionOriginal">Nombre de Región original asociado.</param>
        /// <param name="nombreRegionNuevo">Nuevo Nombre de Región que reemplazará al Nombre de Región original.</param>
        /// <response code="200">Retorna OK</response>
        /// <response code="400">La solicitud no pudo ser entendida por el servidor debido a una mala sintaxis.</response>
        /// <response code="401">En el caso que los valores Client Secret y Client Id son inválidos</response>
        /// <response code="404">Un recurso no fue encontrado, típicamente por uso de una url indebida</response>
        /// <response code="500">Ocurrió un error interno en el servidor</response>
        /// <returns></returns>
        [Route("/action/ActualizarNombreRegion/{nombreRegionOriginal}/{nombreRegionNuevo}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ValidarCliente")]
        [HttpPost]
        [ProducesResponseType(typeof(ComunaResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ActualizarNombreRegion([FromRoute] string nombreRegionOriginal, [FromRoute] string nombreRegionNuevo)
        {
            var handlerResponse = await _mediator.Send(new ActualizarNombreRegionCommand() { NombreRegionOriginal = nombreRegionOriginal, NombreRegionNuevo = nombreRegionNuevo }).ConfigureAwait(false);
            return Ok(handlerResponse);
        }
    }
}
