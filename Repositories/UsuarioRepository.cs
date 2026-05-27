using Academico.Models;
using Microsoft.EntityFrameworkCore;

namespace Academico.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario?> BuscarPorEmailAsync(string email);
    Task CriarAsync(Usuario usuario);
}

public class UsuarioRepository : IUsuarioRepository
{
    readonly AcademicoContext _context;

    public UsuarioRepository(AcademicoContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> BuscarPorEmailAsync(string email)
    {
        return await _context.Usuario
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task CriarAsync(Usuario usuario)
    {
        await _context.Usuario.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }
}