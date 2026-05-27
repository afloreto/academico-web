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
        if(await _professorRepository.CriarProfessorAsync(professor))
        {
            TempData["Mensagem"] = $"Professor {professor.Nome} criado com sucesso!";
            TempData["Tipo"] = "success";
        }
        else
        {
            TempData["Mensagem"] = $"Erro ao criar professor: {professor.Nome}";
            TempData["Tipo"] = "danger";
        }
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> ExcluirProfessor(int id)
    {
        if(await _professorRepository.ExcluirProfessorAsync(id))
        {
            TempData["Mensagem"] = $"Professor excluído com sucesso!";
            TempData["Tipo"] = "success";
        }
        else
        {
            TempData["Mensagem"] = $"Erro ao excluir professor.";
            TempData["Tipo"] = "danger";
        }
        return RedirectToAction("Index");
    }
}