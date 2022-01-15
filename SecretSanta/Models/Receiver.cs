namespace SecretSanta.Models;

public class Receiver : User
{
    public string Wishes { get; set; }
    public bool HasSanta { get; set; } = false;
}