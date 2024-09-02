using System;
using Google.Protobuf;
using Microsoft.AspNetCore.Http;

namespace UserApp.Application.Mapper;

public class ByteStringModel
{
    public ByteString Base64String { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
}
public class FormFileModel
{
    public IFormFile FormFileProperty { get; set; }
}
