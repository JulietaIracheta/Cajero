using Cajero.Service;
using Microsoft.AspNetCore.Mvc;

namespace Cajero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CajeroController : ControllerBase
    {
        public CajeroLogic _cajeroLogic;
        public UsuarioLogic _usuarioLogic;
        public OperacionesLogic _operacionesLogic;

        public CajeroController(CajeroLogic cajeroLogic, UsuarioLogic usuarioLogic, OperacionesLogic operacionesLogic)
        {
            _cajeroLogic = cajeroLogic;
            _usuarioLogic = usuarioLogic;
            _operacionesLogic = operacionesLogic;
        }

        [HttpGet("Login")]
        public async Task<ActionResult> Login(long numeroTarjeta, int pin)
        {
            try
            {
                var validaDatos = await _cajeroLogic.ValidarDatos(numeroTarjeta, pin);

                if (validaDatos)
                {
                    var token = _cajeroLogic.GenerateJwtToken(numeroTarjeta);
                    return Ok(new { token });
                }
                else
                {
                    var mensaje = "No se pudo acceder a su tarjeta, recien bien los datos o comuniquese con el banco";
                    return BadRequest(new { message = mensaje });

                }

            }
            catch (Exception e)
            {
                return new ObjectResult(new
                {
                    details = e.Message
                })
                {
                    StatusCode = 500
                };
            }
        }


        [HttpGet("Saldo")]
        public async Task<IActionResult> Saldo(long numeroTarjeta)
        {
            try
            {
                var tarjetaBloqueada = await _cajeroLogic.SearchTarjeta(numeroTarjeta);

                if (tarjetaBloqueada.Bloqueada)
                {
                    var mensaje = "La tarjeta se encuentra bloqueada";
                    return BadRequest(new { message = mensaje });
                }

                var datosUsuarios = await _usuarioLogic.Saldo(numeroTarjeta);
                return Ok(datosUsuarios);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("Retiro")]
        public async Task<IActionResult> Retiro(long numeroTarjeta, int monto)
        {
            try
            {
                var tarjetaBloqueada = await _cajeroLogic.SearchTarjeta(numeroTarjeta);

                if (tarjetaBloqueada.Bloqueada)
                {
                    var mensaje = "La tarjeta se encuentra bloqueada";
                    return BadRequest(new { message = mensaje });
                }

                var retiro = await _usuarioLogic.Retiro(numeroTarjeta, monto);

                if (retiro.Saldo < monto)
                {
                    var mensaje = "El saldo es insuficiente para realizar el retiro";
                    return BadRequest(new { message = mensaje });
                }

                return Ok(retiro);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("Operaciones")]
        public async Task<IActionResult> Operaciones(long numeroTarjeta, int page = 1, int pageSize = 10)
        {
            try
            {
                var historialOperaciones = await _operacionesLogic.Operaciones(numeroTarjeta, page, pageSize);

                return Ok(historialOperaciones);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}