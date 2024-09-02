using System;
using Google.Protobuf;

namespace UserApp.Application.Validators;

public class FileTypeChecker
{
    private static readonly Dictionary<string, byte[]> fileSignatures = new Dictionary<string, byte[]>
    {
        { "image/png", new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } },
        { "image/jpeg", new byte[] { 0xFF, 0xD8, 0xFF } },
        { "application/pdf", new byte[] { 0x25, 0x50, 0x44, 0x46 } }
    };

    public static bool IsValidContentType(ByteString fileByteString, string expectedContentType)
    {
        if (fileSignatures.TryGetValue(expectedContentType, out byte[]? value))
        {
            var fileBytes = fileByteString.ToByteArray();
            byte[] signature = value;
            for (int i = 0; i < signature.Length; i++)
            {
                if (fileBytes.Length <= i || fileBytes[i] != signature[i])
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
}
