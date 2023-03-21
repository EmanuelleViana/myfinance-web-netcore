using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using myfinance_web_dotnet.Utils;
using myfinance_web_netcore.Domain.Services.Interfaces;
using myfinance_web_netcore.Models;

namespace myfinance_web_netcore.Controllers;

[Route("[controller]")]
public class TransacaoController : Controller
{
    private readonly ILogger<TransacaoController> _logger;
    private readonly IPlanoContaService _planoContaService;
    private readonly ITransacaoService _transacaoService;
    private readonly ILogService _logService;


    public TransacaoController(ILogger<TransacaoController> logger, ITransacaoService transacaoService, ILogService logService
, IPlanoContaService planoContaService)
    {
        _logger = logger;
        _planoContaService = planoContaService;
        _transacaoService = transacaoService;
        _logService = logService;

    }

    [HttpGet]
    [Route("Index")]
    public IActionResult Index()
    {
        ViewBag.Transacoes = _transacaoService.ListarRegistros();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    [Route("Cadastro")]
    [Route("Cadastro/{id}")]
    public IActionResult Cadastro(int? id)
    {
        var model = new TransacaoModel();

        if (id != null)
        {
            model = _transacaoService.RetornaRegistro((int)id);
        }
        var lista = _planoContaService.ListarRegistros();
        model.PlanoContas = new SelectList(lista, "Id", "Descricao");

        return View(model);
    }

    [HttpPost]
    [Route("Cadastro")]
    [Route("Cadastro/{id}")]
    public IActionResult Cadastro(TransacaoModel transacaoModel)
    {
        _transacaoService.Salvar(transacaoModel);
        return RedirectToAction("index");
    }


    [HttpGet]
    [Route("Excluir/{id}")]
    public IActionResult Excluir(int id)
    {
        var transacao = _transacaoService.RetornaRegistro(id);

        if (transacao != null)
        {
            _transacaoService.Excluir(id);
        }

        _logService.Salvar(
            new LogExclusaoModel()
            {
                Data = DateTime.Now,
                Observacao = JsonSerializer.Serialize(transacao),
                Tabela = Constantes.Tabela.Transacao,
                IdRegistro = id,
                Operacao = Constantes.TipoOperacao.Exclusao
            }
        );

        return RedirectToAction("index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
