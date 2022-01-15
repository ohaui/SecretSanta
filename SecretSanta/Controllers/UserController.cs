using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;
using SecretSanta.Models;

namespace SecretSanta.Controllers;

public class UserController : BaseApiController
{
    private readonly SantaContext _context;

    public UserController(SantaContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    [Route("Register")]
    public async Task<IResult> Register(string firstName, string secondName, string wishes)
    {
        var user = new Receiver
        {
            FirstName = firstName,
            LastName = secondName,
            Wishes = wishes
        };

        await _context.Receivers.AddAsync(user);
        
        _context.SaveChangesAsync();

        return Results.Ok();
    }

    [HttpGet]
    [Route("GetReceiver")]
    public async Task<Receiver> GetReceiver(int id)
    {
        var user = await _context.Receivers.FirstOrDefaultAsync(x => x.Id == id);

        return user;
    }
    
    [HttpGet]
    [Route("GetReceivers")]
    public async Task<Receiver[]> GetReceivers()
    {
        var users = _context.Receivers.ToArrayAsync().Result;
        return users;
    }
    
    [HttpGet]
    [Route("GetSanta")]
    public async Task<Santa> GetSanta(int id)
    {
        var santa = await _context.Santas.FirstOrDefaultAsync(x => x.Id == id);
        return santa;
    }

    [HttpGet]
    [Route("GetSantas")]
    public async Task<Santa[]> GetSantas()
    {
        var santas = _context.Santas.ToArrayAsync().Result;
        return santas;
    }
}