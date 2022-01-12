using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;
using SecretSanta.Models;

namespace SecretSanta.Controllers;

public class InteractionController : BaseApiController
{
    private readonly SantaContext _context;
    private readonly ILogger<InteractionController> _logger;
    private Receiver[] Receivers;

    public InteractionController(SantaContext context, ILogger<InteractionController> logger)
    {
        _context = context;
        _logger = logger;
        Receivers = _context.Receivers.ToArrayAsync().Result;
    }
    
    [HttpGet]
    public async Task<StatusCodeResult> Shuffle()
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

        return Ok();
    }

    [HttpPost]
    public async Task<StatusCodeResult> RevertShuffle()
    {
        for (int i = 1; i < Receivers.Length + 1; i++)
        {
            _context.Receivers.FirstOrDefault(x => x.Id == i).HasSanta = false;
        }

        for (int i = 1; i < Receivers.Length + 1; i++)
        {
            _context.Santas.FirstOrDefault(x => x.Id == i).Receiver = null;
        }
        await _context.SaveChangesAsync();
        
        return Ok();
    }
}