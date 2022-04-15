using Azure.Storage.Blobs;

public class BlobService : IBlobService
{
    private const string ContainerName = "ExampleContainer";

    private readonly BlobContainerClient _blobClient;

    public BlobService(BlobContainerClient blobClient)
    {
        _blobClient = blobClient;
        _blobClient.CreateIfNotExists();
    }

    public async Task<bool> CreateBlobFileAsync(string filename, byte[] data)
    {
        if(GetBlob(filename).Exists())
        {
            throw new Exception("Blob already exists");
        }
        
        try
        {
            await using var memoryStream = new MemoryStream(data, false);
            var response = await _blobClient.UploadBlobAsync(filename, memoryStream);
        }
        catch(Exception exception)
        {
            // do something

            return false;
        }

        return true;
    }
    
    public async Task<Stream?> GetBlobFileAsync(string filename)
    {
        var blob = GetBlob(filename);

        if(!blob.Exists())
        {
            return null;
        }

        var blobContent = await blob.DownloadContentAsync();

        return blobContent.Value.Content.ToStream();
    }
    
    public async Task<bool> DeleteBlobFileAsync(string filename)
    {
        return await _blobClient.DeleteBlobIfExistsAsync(filename);
    }

    public int GetBlobCount()
    {
        return _blobClient.GetBlobs().Count();
    }

    private BlobClient GetBlob(string filename)
    {
        return _blobClient.GetBlobClient(filename);
    }
}