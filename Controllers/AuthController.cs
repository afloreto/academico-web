using Academico.Models;
using Academico.Repositories;
using Microsoft.AspNetCore.Mvc;
using Academico.Filters;
namespace Academico.Controllers;

[IgnoreAuthFilter]
public class AuthController : Controller

{
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string email, string senha)
    {
        var usuario = await _usuarioRepository.BuscarPorEmailAsync(email);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash))
        {
            ViewBag.Erro = "E-mail ou senha incorretos.";
            return View();
        }

        HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
        HttpContext.Session.SetString("UsuarioEmail", usuario.Email);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    public IActionResult Registro() => View();

    [HttpPost]
    public async Task<IActionResult> Registro(string nome, string email, string senha)
    {
        var existente = await _usuarioRepository.BuscarPorEmailAsync(email);
        if (existente != null)
        {
            ViewBag.Erro = "E-mail já cadastrado.";
            return View();
        }

        var usuario = new Usuario
        {
            Nome = nome,
            Email = email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha)
        };

        await _usuarioRepository.CriarAsync(usuario);
        return RedirectToAction("Login");
    }
}