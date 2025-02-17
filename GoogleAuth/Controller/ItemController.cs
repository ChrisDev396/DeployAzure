﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
//[Authorize]
public class ItemController : ControllerBase
{
    private readonly AppDbContext _context;

    public ItemController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Item>> Get()
    {
        var item = _context.Itens.ToList();
        if (item is null)
        {
            return NotFound();
        }
        return item;
    }

    [HttpGet("{id:alpha}", Name = "ObterItem")]
    public ActionResult<Item> Get(string shild)
    {
        var item = _context.Itens.FirstOrDefault(p => p.ShildNome == shild);
        if (item is null)
        {
            return NotFound("Item não encontrado.");
        }
        return item;
    }

    [HttpPost]
    public ActionResult Post(Item item)
    {
        if (item is null)
        {
            return BadRequest();
        }
        _context.Itens.Add(item);
        _context.SaveChanges();
        return new CreatedAtRouteResult("ObterItem",
            new { id = item.Email }, item);
    }

}

