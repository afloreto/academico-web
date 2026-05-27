using Academico.Models;
using Academico.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Academico.Controllers;

public class DisciplinaProfessorController : Controller
{
    private readonly IDisciplinaProfessorRepository _repository;
    private readonly IProfessorRepository _professorRepository;

    public DisciplinaProfessorController(
        IDisciplinaProfessorRepository repository,
        IProfessorRepository professorRepository)
    {
        _repository = repository;
        _professorRepository = professorRepository;
    }

    public async Task<IActionResult> Index(int professorId)
    {
        var professor = await _professorRepository.BuscarPorIdAsync(professorId);
        if (professor == null) return NotFound();
        var disciplinas = await _repository.ListarPorProfessorAsync(professorId);
        ViewBag.Professor = professor;
        return View(disciplinas);
    }

    public async Task<IActionResult> Adicionar(int professorId)
    {
        var professor = await _professorRepository.BuscarPorIdAsync(professorId);
        if (professor == null) return NotFound();
        ViewBag.ProfessorId = professorId;
        ViewBag.ProfessorNome = professor.Nome;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar(DisciplinaProfessor disciplina)
    {
        await _repository.AdicionarAsync(disciplina);
        return RedirectToAction("Index", new { professorId = disciplina.ProfessorId });
    }

    public async Task<IActionResult> Editar(int id)
    {
        var disciplina = await _repository.BuscarPorIdAsync(id);
        if (disciplina == null) return NotFound();
        return View(disciplina);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(DisciplinaProfessor disciplina)
    {
        await _repository.AtualizarAsync(disciplina);
        return RedirectToAction("Index", new { professorId = disciplina.ProfessorId });
    }

    [HttpPost]
    public async Task<IActionResult> Excluir(int id, int professorId)
    {
        await _repository.ExcluirAsync(id);
        return RedirectToAction("Index", new { professorId });
    }
}