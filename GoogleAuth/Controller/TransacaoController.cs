using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

[ApiController]
[Route("[controller]")]
//[Authorize]
public class TransacaoController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransacaoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Transacao>> Get()
    {
        var transacoes = _context.Transacoes.ToList();
        if (transacoes is null)
        {
            return Ok();
        }
        return transacoes;
    }

    [HttpGet("{id:alpha}", Name = "ObterTransacao")]
    public ActionResult<Transacao> Get(string id)
    {
        var transacao = _context.Transacoes.FirstOrDefault(p => p.Email == id);

        if (transacao is null)
        {
            return NotFound("Usuário não encontrado.");
        }

        string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ')[1];
        var emailClaim = token != null ? new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.FirstOrDefault(c => c.Type == "email") : null;

        if (emailClaim != null)
        {
            string email = emailClaim.Value;
            if (email != transacao.Email)
            {
                return BadRequest("Voce não tem acesso a essa conta. " + transacao.Email);
            }
        }

        return transacao;
    }

    [HttpPost]
    public ActionResult Post(Transacao transacao)
    {
        if (transacao is null)
        {
            return BadRequest("Erro");
        }

        string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ')[1];
        var emailClaim = token != null ? new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.FirstOrDefault(c => c.Type == "email") : null;
        string email = emailClaim.Value;
        //if (emailClaim != null)
        //{
        //    string email = emailClaim.Value;
        //    if (email != transacao.Email)
        //    {
        //        return BadRequest("Voce não tem acesso a essa conta. " + transacao.Email);
        //    }

        //}
        transacao.Email = email;
        _context.Transacoes.Add(transacao);
        _context.SaveChanges();

        //return new CreatedAtRouteResult("ObterTransacao",
        //new { id = transacao.NomeUsuario }, transacao);
        return Ok("Success");
    }

    [HttpPut]
    public ActionResult Update(Transacao transacaoAtualizada)
    {
        if (transacaoAtualizada.NomeTitular is null)
        {
            return BadRequest("Erro: Transação inválida.");
        }

        var transacaoExistente = _context.Transacoes.FirstOrDefault(t => t.TransacaoId == transacaoAtualizada.TransacaoId);
        if (transacaoExistente == null)
        {
            return NotFound("Transação não encontrada.");
        }

        string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ')[1];
        var emailClaim = token != null ? new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.FirstOrDefault(c => c.Type == "email") : null;

        if (emailClaim != null)
        {
            string email = emailClaim.Value;
            if (email != transacaoExistente.Email)
            {
                return BadRequest("Você não tem permissão para atualizar esta transação.");
            }
        }

        transacaoExistente.NomeTitular = transacaoAtualizada.NomeTitular;

        _context.SaveChanges();

        return Ok("Transação atualizada com sucesso.");
    }
}

