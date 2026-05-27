using Academico.Models;
using Microsoft.EntityFrameworkCore;

namespace Academico.Repositories;

public interface IProfessorRepository
{
    Task<bool> CriarProfessorAsync(Professor professor);
    Task<List<Professor>> ListarProfessoresAsync();
    Task<Professor?> BuscarPorIdAsync(int id);
    Task<bool> AtualizarProfessorAsync(Professor professor);
    Task<bool> ExcluirProfessorAsync(int id);
}

public class ProfessorRepository : IProfessorRepository
{
    readonly AcademicoContext _context;

    public ProfessorRepository(AcademicoContext context)
    {
        _context = context;
    }

    public async Task<bool> CriarProfessorAsync(Professor professor)
    {
        try
        {
            professor.Siape = $"2026{new Random().Next(0, 99):D2}";
            await _context.AddAsync(professor);
            await _context.SaveChangesAsync();
            return true;
        }
        catch { return false; }
    }

    public async Task<List<Professor>> ListarProfessoresAsync()
    {
        return await _context.Professor.ToListAsync();
    }

    public async Task<Professor?> BuscarPorIdAsync(int id)
    {
        return await _context.Professor.FindAsync(id);
    }

    public async Task<bool> AtualizarProfessorAsync(Professor professor)
    {
        try
        {
            _context.Professor.Update(professor);
            await _context.SaveChangesAsync();
            return true;
        }
        catch { return false; }
    }

    public async Task<bool> ExcluirProfessorAsync(int id)
    {
        try
        {
            var professor = await _context.Professor.FindAsync(id);
            if (professor == null) return false;
            _context.Professor.Remove(professor);
            await _context.SaveChangesAsync();
            return true;
        }
        catch { return false; }
    }
}