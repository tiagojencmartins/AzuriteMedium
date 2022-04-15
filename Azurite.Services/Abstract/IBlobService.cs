using Azure.Storage.Blobs.Models;

public interface IBlobService
{
    Task<bool> CreateBlobFileAsync(string filename, byte[] data);
    
    Task<Stream?> GetBlobFileAsync(string filename);
    
    Task<bool> DeleteBlobFileAsync(string filename);

    int GetBlobCount();
}