namespace Hackathon.Interfaces
{
    public interface IHasher
    {
        string GetHash(string password);

        bool Verify(string passwordHash, string inputPassword);
    }
}
