using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Repository;
using WebApi.ViewModel;

namespace WebApi
{
    public class PesquisaNotaCompraAprovacaoController : ControllerBase
    {
        private INotaCompraRepository _notaCompraRepository;
        private IHostingEnvironment _env;
        public PesquisaNotaCompraAprovacaoController(INotaCompraRepository notaCompraRepository) {
            _notaCompraRepository = notaCompraRepository;
        }

        [HttpGet("PesquisaNotaCompraAprovacao/model")]
        public async Task<IActionResult> Get() {
           return Ok(new NotaCompraViewModel());
        }

        [HttpGet("PesquisaNotaCompraAprovacao")]
        public async Task<IActionResult> Get([FromBody]PesquisaNotaCompraViewModel pesquisaNotaCompra) {
            List<NotaCompraViewModel> listNotaCompraViewModel = new List<NotaCompraViewModel>();
            foreach(NotaCompra nfCompra in await _notaCompraRepository.GetNotasComprasAsyncByFilterDate(pesquisaNotaCompra.dataInicio, pesquisaNotaCompra.dataFim, pesquisaNotaCompra.usuarioId)){
                listNotaCompraViewModel.Add(
                    new NotaCompraViewModel {
                        DataEmissao = nfCompra.DataEmissao,
                        ValorMercadorias = nfCompra.ValorMercadorias,
                        ValorDesconto = nfCompra.ValorDesconto,
                        ValorFrete = nfCompra.ValorFrete,
                        ValorTotal = nfCompra.ValorTotal,
                        Status = (ViewModel.Status)(int)nfCompra.Status
                    }
                );
            }
            
            return Ok(listNotaCompraViewModel);
        }
    }
}