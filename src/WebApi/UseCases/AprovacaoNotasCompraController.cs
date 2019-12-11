using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository;
using WebApi.ViewModel;

namespace WebApi
{
    public class AprovacaoNotaCompraController : ControllerBase
    {
        private INotaCompraRepository _notaCompraRepository;
        public AprovacaoNotaCompraController(INotaCompraRepository notaCompraRepository) {
            _notaCompraRepository = notaCompraRepository;
        }
        [HttpPost("AprovacaoNotaCompra")]
        public async Task<IActionResult> Post([FromBody]VistoAprovacaoNotaCompraViewModel vistoAprovacaoNotaCompra){
            if (await _notaCompraRepository.RegistraVistoAprovacaoAsyncById(vistoAprovacaoNotaCompra.idNotaCompra,vistoAprovacaoNotaCompra.usuarioId)) 
            {
                return Ok();
            }
            else {
                return BadRequest();
            }
            
        }
    }
}