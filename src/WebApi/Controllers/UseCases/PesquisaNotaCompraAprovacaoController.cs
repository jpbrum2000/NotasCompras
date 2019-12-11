using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using WebApi.ViewModel;

namespace WebApi
{
    public class PesquisaNotaCompraAprovacaoController : ControllerBase
    {
        private INotaCompraRepository _notaCompraRepository;
        private IMapper _mapper;
        public PesquisaNotaCompraAprovacaoController(INotaCompraRepository notaCompraRepository, IMapper mapper) {
            _notaCompraRepository = notaCompraRepository;
            _mapper = mapper;
        }

        [HttpGet("PesquisaNotaCompraAprovacao/model")]
        public IActionResult Get() {
           return Ok(new NotaCompraViewModel());
        }

        [HttpPost("PesquisaNotaCompraAprovacao")]
        public async Task<IActionResult> Get([FromBody]PesquisaNotaCompraViewModel pesquisaNotaCompra) {

            NotaCompra[] nfCompra = await _notaCompraRepository.GetNotasComprasAsyncByFilterDate(pesquisaNotaCompra.dataInicio, pesquisaNotaCompra.dataFim, pesquisaNotaCompra.usuarioId);
            NotaCompraViewModel[] listNotaCompraViewModel = _mapper.Map<NotaCompraViewModel[]>(nfCompra);
                
            return Ok(listNotaCompraViewModel);
        }
    }
}