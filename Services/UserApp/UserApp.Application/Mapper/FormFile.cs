using System;
using Google.Protobuf;
using Microsoft.AspNetCore.Http;

namespace UserApp.Application.Mapper;

public class FormFile : IFormFile
{
    private readonly ByteString _fileBytes;

    public FormFile(ByteString fileBytes, string fileName, string contentType)
    {
        _fileBytes = fileBytes;
        FileName = fileName;
        ContentType = contentType;
    }

    public Stream OpenReadStream()
    {
        return new MemoryStream(_fileBytes.ToByteArray());
    }

    public void CopyTo(Stream target)
    {
        using (var stream = OpenReadStream())
        {
            stream.CopyTo(target);
        }
    }

    public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
    {
        using (var stream = OpenReadStream())
        {
            await stream.CopyToAsync(target, cancellationToken);
        }
    }

    public string ContentType { get; }
    public string ContentDisposition { get; set; }
    public IHeaderDictionary Headers { get; set; }
    public long Length => _fileBytes.Length;
    public string Name { get; set; }
    public string FileName { get; }
}