using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
//[Authorize]
public class UsuarioController : ControllerBase
{
	private readonly AppDbContext _context;

	public UsuarioController(AppDbContext context)
	{
		_context = context;
	}

    //[HttpGet]
    //public ActionResult<IEnumerable<Usuario>> Get()
    //{
    //    var usuarios = _context.Usuarios.ToList();
    //    if (usuarios is null)
    //    {
    //        return Ok();
    //    }
    //    //return usuarios;
    //    return Ok("Sucesso");
    //}
    [HttpGet]
    public ActionResult Get()
    {
        return Content("Success");
    }


    [HttpGet("{id:int}", Name = "ObterUsuario")]
    public ActionResult<Usuario> Get(int id)
    {
        var usuario = _context.Usuarios.FirstOrDefault(p => p.UsuarioId == id);
        if (usuario is null)
        {
            return NotFound("Usuário não encontrado.");
        }

        string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

        if (token != null)
        {
            if (token.StartsWith("Bearer "))
            {
                token = token.Substring("Bearer ".Length);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var emailClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "email");
                        

            if (emailClaim != null)
            {
                string email = emailClaim.Value;
                if (email != usuario.Email)
                {
                    return BadRequest("Voce nao tem acesso a essa conta. "+usuario.Email); 
                }
                
               
            }
        }
        return usuario;
    }

    [HttpPost]
    public ActionResult Post(Usuario usuario)
    {
        if (usuario is null)
        {
            return BadRequest(usuario.UsuarioId);
        }

        if (_context.Usuarios.Any(u => u.Nome == usuario.Nome))
        {
            ModelState.AddModelError("Nome", "O nome de usuário já está em uso.");
            return BadRequest(ModelState);
        }

        string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

        if (token.StartsWith("Bearer "))
        {
            token = token.Substring("Bearer ".Length);
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);

        var emailClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "email").Value;

        if (_context.Usuarios.Any(u => u.Email == emailClaim))
        {
            ModelState.AddModelError("Email", "O email de usuário já está em uso.");
            return BadRequest(ModelState);
        }


        usuario.Email = emailClaim;
        usuario.DataCriacao = DateTime.Now;
        
        
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterUsuario",
            new { id = usuario.UsuarioId }, usuario);
    }


}

