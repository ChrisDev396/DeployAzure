using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

[ApiController]
[Route("[controller]")]
//[EnableRateLimiting]
//[Authorize]
public class UsuarioController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsuarioController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Usuario>> Get()
    {
        var usuarios = _context.Usuarios.ToList();
        if (usuarios is null)
        {
            return Ok();
        }
        return usuarios;
    }

    [HttpGet("suport")]
    public ActionResult Get2()
    {
        var html = @"
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset=""UTF-8"">
            <title>Herti Suport</title>
        </head>
        <body>
            <h1>Need help?</h1>
            <p>Contact hertisup@gmail.com</p>
        </body>
        </html>";

        return Content(html, "text/html");
    }


    [HttpGet("{id:alpha}", Name = "ObterUsuario")]
    public ActionResult<Usuario> Get(string id)
    {
        var usuario = _context.Usuarios.FirstOrDefault(p => p.NomeUsuario == id);
        if (usuario is null)
        {
            return NotFound("Usuário não encontrado.");
        }

        string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ')[1];
        var emailClaim = token != null ? new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.FirstOrDefault(c => c.Type == "email") : null;

        if (emailClaim != null)
            {
                string email = emailClaim.Value;
                if (email != usuario.Email)
                {
                    return BadRequest("Voce não tem acesso a essa conta. " + usuario.Email);
                }
            }
        
        return usuario;
    }

    [HttpPost]
    public ActionResult Post(Usuario usuario)
    {
        if (usuario is null)
        {
            return BadRequest(usuario.NomeUsuario);
        }

        if (_context.Usuarios.Any(u => u.NomeUsuario == usuario.NomeUsuario))
        {
            ModelState.AddModelError("Nome", "O nome de usuário já está em uso.");
            return BadRequest(ModelState);
        }

        string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ')[1];
        var emailClaim = token != null ? new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.FirstOrDefault(c => c.Type == "email") : null;

        if (_context.Usuarios.Any(u => u.Email == emailClaim.Value))
        {
            ModelState.AddModelError("Erro", "Erro no servidor");
            return BadRequest(ModelState);
        }

        usuario.Email = emailClaim.Value;
        usuario.DataCriacao = DateTime.Now;
        usuario.Partidas = 0;
        usuario.Vitorias = 0;
        usuario.Pontuacao = 0;

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterUsuario",
        new { id = usuario.NomeUsuario }, usuario);
        //return Ok("Success");
    }

    

}

