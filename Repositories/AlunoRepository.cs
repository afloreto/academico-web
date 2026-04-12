using Academico.Models;
using Microsoft.EntityFrameworkCore;

namespace Academico.Repositories;

public interface IAlunoRepository
{
    Task CriarAlunoAsync(Aluno aluno);
    Task<List<Aluno>> ListarAlunosAsync();
}

public class AlunoRepository : IAlunoRepository
{
    readonly AcademicoContext _context;

    public AlunoRepository(AcademicoContext context)
    {
        _context = context;
    }

    public async Task CriarAlunoAsync(Aluno aluno)
    {
        aluno.Matricula = $"2026{new Random().Next(0, 99):D2}";
        await _context.AddAsync(aluno);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Aluno>> ListarAlunosAsync()
    {
        return await _context.Aluno.ToListAsync();
    }
}