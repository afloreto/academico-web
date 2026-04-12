using Microsoft.AspNetCore.Mvc;
using Academico.Models;
using Academico.Repositories;

namespace Academico.Controllers;

public class ProfessorController : Controller
{
    readonly IProfessorRepository _professorRepository;

    public ProfessorController(IProfessorRepository professorRepository)
    {
        _professorRepository = professorRepository;
    }

    public async Task<IActionResult> Index()
    {
        var professores = await _professorRepository.ListarProfessoresAsync();
        return View(professores);
    }

    public IActionResult CriarProfessor()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CriarProfessor(Professor professor)
    {
        await _professorRepository.CriarProfessorAsync(professor);
        return RedirectToAction("Index");
    }
}