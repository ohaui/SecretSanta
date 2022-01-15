using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;
using SecretSanta.Models;

namespace SecretSanta.Controllers;

public class InteractionController : BaseApiController
{
    private readonly SantaContext _context;
    private Receiver[] Receivers;

    public InteractionController(SantaContext context)
    {
        _context = context;
        Receivers = _context.Receivers.ToArrayAsync().Result;
    }
    
    [HttpGet]
    public async Task<IResult> Shuffle()
    {
        foreach (var receiver in Receivers)
        {
            var userToGetSanta = Receivers.FirstOrDefault(x => !x.HasSanta && x != receiver);
            var santa = new Santa
            {
                FirstName = receiver.FirstName,
                LastName = receiver.LastName,
                Receiver = userToGetSanta
            };

            userToGetSanta.HasSanta = true;
            
            await _context.Santas.AddAsync(santa);
            await _context.SaveChangesAsync();
        }

        return Results.Ok();
    }

    [HttpPost]
    public async Task<IResult> Deshuffle()
    {
        for (int i = 1; i < Receivers.Length + 1; i++)
        {
            var user = await _context.Receivers.FirstOrDefaultAsync(x => x.Id == i);
            user.HasSanta = false;
        }

        for (int i = 1; i < Receivers.Length + 1; i++)
        {
            var user = await _context.Santas.FirstOrDefaultAsync(x => x.Id == i);
            user.Receiver = null;
        }
        await _context.SaveChangesAsync();
        
        return Results.Ok();
    }
}