using Microsoft.AspNetCore.Mvc;
using Academico.Repositories;
using Academico.Models;

namespace Academico.Controllers
{
    public class GestaoController : Controller
    {
        private readonly IAlunoRepository _alunoRepository;

        public GestaoController(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public async Task<IActionResult> Alunos(string buscaNome, string buscaMatricula)
        {
            var alunos = await _alunoRepository.GetAllAsync(); 

            if (!string.IsNullOrEmpty(buscaNome))
            {
                alunos = alunos.Where(a => a.Nome.Contains(buscaNome, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(buscaMatricula))
            {
                alunos = alunos.Where(a => a.Matricula.Contains(buscaMatricula));
            }

            return View(alunos);
        }
    }
}