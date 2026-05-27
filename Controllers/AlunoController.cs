using Academico.Models;
using Academico.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Academico.Controllers;

public class AlunoController : Controller
{
    private readonly IAlunoRepository _alunoRepository;

    public AlunoController(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    public async Task<IActionResult> Index()
    {
        var alunos = await _alunoRepository.GetAllAsync();
        return View(alunos);
    }

    public IActionResult CriarAluno() => View();

    [HttpPost]
    public async Task<IActionResult> CriarAluno(Aluno aluno)
    {
        await _alunoRepository.CriarAlunoAsync(aluno);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> EditarAluno(int id)
    {
        var aluno = await _alunoRepository.BuscarPorIdAsync(id);
        if (aluno == null) return NotFound();
        return View(aluno);
    }

    [HttpPost]
    public async Task<IActionResult> EditarAluno(Aluno aluno)
    {
        await _alunoRepository.AtualizarAlunoAsync(aluno);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> DeletarAluno(int id)
    {
        await _alunoRepository.DeletarAlunoAsync(id);
        return RedirectToAction("Index");
    }
}