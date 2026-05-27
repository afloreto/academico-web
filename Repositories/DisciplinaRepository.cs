using Academico.Models;
using Microsoft.EntityFrameworkCore;

namespace Academico.Repositories;

public interface IDisciplinaRepository
{
    Task<List<Disciplina>> ListarPorAlunoAsync(int alunoId);
    Task<Disciplina?> BuscarPorIdAsync(int id);
    Task<List<Disciplina>> ListarTodasAsync();
    Task AdicionarAsync(Disciplina disciplina);
    Task AtualizarAsync(Disciplina disciplina);
    Task ExcluirAsync(int id);
    
}

public class DisciplinaRepository : IDisciplinaRepository
{
    readonly AcademicoContext _context;

    public DisciplinaRepository(AcademicoContext context)
    {
        _context = context;
    }

    public async Task<List<Disciplina>> ListarPorAlunoAsync(int alunoId)
    {
        return await _context.Disciplina
            .Where(d => d.AlunoId == alunoId)
            .ToListAsync();
    }

    public async Task<Disciplina?> BuscarPorIdAsync(int id)
    {
        return await _context.Disciplina.FindAsync(id);
    }

    public async Task AdicionarAsync(Disciplina disciplina)
    {
        await _context.Disciplina.AddAsync(disciplina);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Disciplina disciplina)
    {
        _context.Disciplina.Update(disciplina);
        await _context.SaveChangesAsync();
    }

    public async Task ExcluirAsync(int id)
    {
        var disciplina = await _context.Disciplina.FindAsync(id);
        if (disciplina != null)
        {
            _context.Disciplina.Remove(disciplina);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Disciplina>> ListarTodasAsync()
{
    return await _context.Disciplina.Include(d => d.Aluno).ToListAsync();
}
}