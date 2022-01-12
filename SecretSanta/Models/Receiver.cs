namespace SecretSanta.Models;

public class Receiver : User
{
    public string Wishes { get; set; }
    public bool HasSanta { get; set; } = false;

    public override string ToString()
    {
        return Id + " " + FirstName + " " + LastName + " " + Wishes + " " + HasSanta;
    }
}