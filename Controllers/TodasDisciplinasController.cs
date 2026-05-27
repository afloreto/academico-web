using Academico.Models;
using Academico.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Academico.Controllers;

public class TodasDisciplinasController : Controller
{
    private readonly IDisciplinaRepository _disciplinaRepository;
    private readonly IDisciplinaProfessorRepository _disciplinaProfessorRepository;

    public TodasDisciplinasController(
        IDisciplinaRepository disciplinaRepository,
        IDisciplinaProfessorRepository disciplinaProfessorRepository)
    {
        _disciplinaRepository = disciplinaRepository;
        _disciplinaProfessorRepository = disciplinaProfessorRepository;
    }

    public async Task<IActionResult> Index()
    {
        var disciplinasAlunos = await _disciplinaRepository.ListarTodasAsync();
        var disciplinasProfessores = await _disciplinaProfessorRepository.ListarTodasAsync();
        ViewBag.DisciplinasAlunos = disciplinasAlunos;
        ViewBag.DisciplinasProfessores = disciplinasProfessores;
        return View();
    }
}