using Academico.Models;
using Microsoft.EntityFrameworkCore;

namespace Academico.Repositories;

public interface IDisciplinaProfessorRepository
{
    Task<List<DisciplinaProfessor>> ListarPorProfessorAsync(int professorId);
    Task<List<DisciplinaProfessor>> ListarTodasAsync();
    Task<DisciplinaProfessor?> BuscarPorIdAsync(int id);
    Task AdicionarAsync(DisciplinaProfessor disciplina);
    Task AtualizarAsync(DisciplinaProfessor disciplina);
    Task ExcluirAsync(int id);
}

public class DisciplinaProfessorRepository : IDisciplinaProfessorRepository
{
    readonly AcademicoContext _context;

    public DisciplinaProfessorRepository(AcademicoContext context)
    {
        _context = context;
    }

    public async Task<List<DisciplinaProfessor>> ListarPorProfessorAsync(int professorId)
    {
        return await _context.DisciplinaProfessor
            .Where(d => d.ProfessorId == professorId)
            .ToListAsync();
    }

    public async Task<DisciplinaProfessor?> BuscarPorIdAsync(int id)
    {
        return await _context.DisciplinaProfessor.FindAsync(id);
    }

    public async Task AdicionarAsync(DisciplinaProfessor disciplina)
    {
        await _context.DisciplinaProfessor.AddAsync(disciplina);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(DisciplinaProfessor disciplina)
    {
        _context.DisciplinaProfessor.Update(disciplina);
        await _context.SaveChangesAsync();
    }

    public async Task ExcluirAsync(int id)
    {
        var disciplina = await _context.DisciplinaProfessor.FindAsync(id);
        if (disciplina != null)
        {
            _context.DisciplinaProfessor.Remove(disciplina);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<List<DisciplinaProfessor>> ListarTodasAsync()
{
    return await _context.DisciplinaProfessor.Include(d => d.Professor).ToListAsync();
}
}