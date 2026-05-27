using Academico.Models;
using Academico.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Academico.Controllers;

public class DisciplinaController : Controller
{
    private readonly IDisciplinaRepository _disciplinaRepository;
    private readonly IAlunoRepository _alunoRepository;

    public DisciplinaController(IDisciplinaRepository disciplinaRepository, IAlunoRepository alunoRepository)
    {
        _disciplinaRepository = disciplinaRepository;
        _alunoRepository = alunoRepository;
    }

    public async Task<IActionResult> Index(int alunoId)
    {
        var aluno = await _alunoRepository.BuscarPorIdAsync(alunoId);
        if (aluno == null) return NotFound();

        var disciplinas = await _disciplinaRepository.ListarPorAlunoAsync(alunoId);
        ViewBag.Aluno = aluno;
        return View(disciplinas);
    }

    public async Task<IActionResult> Adicionar(int alunoId)
    {
        var aluno = await _alunoRepository.BuscarPorIdAsync(alunoId);
        if (aluno == null) return NotFound();
        ViewBag.AlunoId = alunoId;
        ViewBag.AlunoNome = aluno.Nome;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar(Disciplina disciplina)
    {
        await _disciplinaRepository.AdicionarAsync(disciplina);
        return RedirectToAction("Index", new { alunoId = disciplina.AlunoId });
    }

    public async Task<IActionResult> Editar(int id)
    {
        var disciplina = await _disciplinaRepository.BuscarPorIdAsync(id);
        if (disciplina == null) return NotFound();
        return View(disciplina);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(Disciplina disciplina)
    {
        await _disciplinaRepository.AtualizarAsync(disciplina);
        return RedirectToAction("Index", new { alunoId = disciplina.AlunoId });
    }

    [HttpPost]
    public async Task<IActionResult> Excluir(int id, int alunoId)
    {
        await _disciplinaRepository.ExcluirAsync(id);
        return RedirectToAction("Index", new { alunoId });
    }
}