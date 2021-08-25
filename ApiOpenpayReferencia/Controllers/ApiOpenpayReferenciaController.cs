using ApiOpenpayReferencia.Models;
using ApiOpenpayReferencia.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using Openpay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ApiOpenpayReferencia.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiOpenpayReferenciaController : ControllerBase
    {
        private IReferenciaService _referenciaService;
        public ApiOpenpayReferenciaController(IReferenciaService referenciaService) {
            this._referenciaService = referenciaService;
        }

        Logger log = LogManager.GetCurrentClassLogger();

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

            return new string[] { "ApiOpenpayReferencia V. " + version };

        }

        [HttpPost("GetReferencia")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        //[Authorize]//Basic Auth
        public async Task<IActionResult> GetReferencia([FromBody] DatoEntrada entrada)
        {
            string mensaje = null;
            
            try
            {
                Charge charge = _referenciaService.GetReferencia(entrada);

                if (charge == null || 
                    charge.PaymentMethod == null || 
                    charge.PaymentMethod.Reference == null ||
                    charge.PaymentMethod.Reference.Equals(String.Empty))
                {
                    return NotFound(new
                    {
                        exitoso = false,
                        mensaje = mensaje,
                        referencia = String.Empty,
                        clienteID = String.Empty
                    });
                }
                else
                { 
                    
                }

                string referencia = charge.PaymentMethod.Reference;
                string customerID = charge.CustomerId;
                mensaje = "Mensaje exitoso. Se generó la referencia: ";
                log.Info(mensaje + referencia);
                return Ok(new
                {                    
                    exitoso = true,
                    mensaje = String.Empty,
                    referencia = referencia,
                    clienteID = customerID
                });
            }
            catch (Exception ex)
            {
                mensaje = "Ocurrió un error inesperado";
                log.Error(mensaje + ". " + ex.Message);
                return NotFound(new
                {
                    exitoso = false,
                    mensaje = mensaje,
                    referencia = String.Empty,
                    clienteID = String.Empty
                });
            }
        }
    }
}
