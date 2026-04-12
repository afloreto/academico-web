using Academico.Models;
using Microsoft.EntityFrameworkCore;

namespace Academico.Repositories;

public interface IProfessorRepository
{
    Task CriarProfessorAsync(Professor professor);
    Task<List<Professor>> ListarProfessoresAsync();
}

public class ProfessorRepository : IProfessorRepository
{
    readonly AcademicoContext _context;

    public ProfessorRepository(AcademicoContext context)
    {
        _context = context;
    }

    public async Task CriarProfessorAsync(Professor professor)
    {
        professor.Siape = $"2026{new Random().Next(0, 99):D2}";
        await _context.AddAsync(professor);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Professor>> ListarProfessoresAsync()
    {
        return await _context.Professor.ToListAsync();
    }
}