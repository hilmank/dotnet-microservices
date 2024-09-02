using System;
using AutoMapper;
using Google.Protobuf;
using Microsoft.AspNetCore.Http;

namespace UserApp.Application.Mapper;

public class ByteStringToIFormFileConverter : IValueConverter<ByteString, IFormFile>
{
    private readonly string _fileName;
    private readonly string _contentType;

    public ByteStringToIFormFileConverter(string fileName, string contentType)
    {
        _fileName = fileName;
        _contentType = contentType;
    }

    public IFormFile Convert(ByteString sourceMember, ResolutionContext context)
    {
        if (sourceMember == null || sourceMember.Length == 0)
        {
            return null!;
        }

        return new FormFile(sourceMember, _fileName, _contentType);
    }
}