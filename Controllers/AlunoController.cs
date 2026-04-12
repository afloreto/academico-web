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
        List<Aluno> aluno1 = new List<Aluno>()
        {
            new Aluno()
            {
                Nome = "Arnaldo",
                Cpf = "12345678910",
                Curso = "Tecnologia em Análise e Desenvolvimento de Sistemas",
                Matricula = "20250988890",
                DataNascimento = new DateOnly(1988, 09, 02)
            },
            new Aluno()
            {
                Nome = "Zé Coméia",
                Cpf = "09876543211",
                Curso = "Tecnologia em Análise e Desenvolvimento de Sistemas",
                Matricula = "20250988899",
                DataNascimento = new DateOnly(2000, 09, 02)
            },
            new Aluno()
            {
                Nome = "Siene dos Santos Rosa",
                Cpf = "07084593152",
                Curso = "Tecnologia em Análise e Desenvolvimento de Sistemas",
                Matricula = "20241202412330034",
                DataNascimento = new DateOnly(2000, 07, 17)
            },
            new Aluno()
            {
                Nome = "Alex Geraldo de Alencar",
                Cpf = "12345678911",
                Curso = "Agronomia Integral",
                Matricula = "20251202512330009",
                DataNascimento = new DateOnly(1997, 11, 27)
            }
        };

        var dosBanco = await _alunoRepository.ListarAlunosAsync();
        aluno1.AddRange(dosBanco);

        return View(aluno1);
    }

    public IActionResult CriarAluno()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CriarAluno(Aluno aluno)
    {
        await _alunoRepository.CriarAlunoAsync(aluno);
        return RedirectToAction("Index");
    }
}