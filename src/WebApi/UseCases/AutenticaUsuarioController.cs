using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using WebApi.ViewModel;

namespace WebApi
{
    public class AutenticaUsuarioController : ControllerBase
    {
        private INotaCompraRepository _notaCompraRepository;
        public AutenticaUsuarioController(INotaCompraRepository notaCompraRepository) {
            _notaCompraRepository = notaCompraRepository;
        }

        [HttpPost("AutenticaUsuario")]
        public async Task<IActionResult> Get([FromBody]UsuarioLoginViewModel usuarioLogin) {
            Usuario usuario = await _notaCompraRepository.autenticaUsuario(usuarioLogin.login, usuarioLogin.senha);
            return Ok(usuario);
        }        
    }
}