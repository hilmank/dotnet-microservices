using MimeKit;
using MimeKit.Utils;

using UserApp.Application.Settings;

namespace UserApp.Infrastructure.Middlewares;

public static class EmailProperty
{
    public static string ReplaceFooterContent(BodyBuilder builder, string stringInput)
    {
        stringInput = stringInput.Replace("[Year]", DateTime.Now.Year.ToString());
        stringInput = stringInput.Replace("[OrganizationName]", OrganizationSettings.Name);
        stringInput = stringInput.Replace("[UrlOrganizationFacebook]", OrganizationSettings.Facebook);
        stringInput = stringInput.Replace("[UrlOrganizationTwitter]", OrganizationSettings.Twitter);
        stringInput = stringInput.Replace("[UrlOrganizationInstagram]", OrganizationSettings.Instagram);
        stringInput = stringInput.Replace("[UrlOrganizationTiktok]", OrganizationSettings.Tiktok);
        stringInput = stringInput.Replace("[UrlOrganizationYoutube]", OrganizationSettings.Youtube);
        stringInput = stringInput.Replace("[UrlOrganizationWhatsapp]", OrganizationSettings.Whatsapp);

        MimeEntity mimeEntity = builder.LinkedResources.Add($"{DirectorySettings.PathFileApp}/icons8-facebook-96.png");
        mimeEntity.ContentId = MimeUtils.GenerateMessageId();
        stringInput = stringInput.Replace("[IconFacebook]", mimeEntity.ContentId);

        mimeEntity = builder.LinkedResources.Add($"{DirectorySettings.PathFileApp}/icons8-twitterx-96.png");
        mimeEntity.ContentId = MimeUtils.GenerateMessageId();
        stringInput = stringInput.Replace("[IconTwitter]", mimeEntity.ContentId);

        mimeEntity = builder.LinkedResources.Add($"{DirectorySettings.PathFileApp}/icons8-instagram-96.png");
        mimeEntity.ContentId = MimeUtils.GenerateMessageId();
        stringInput = stringInput.Replace("[IconInstagram]", mimeEntity.ContentId);

        mimeEntity = builder.LinkedResources.Add($"{DirectorySettings.PathFileApp}/icons8-tiktok-96.png");
        mimeEntity.ContentId = MimeUtils.GenerateMessageId();
        stringInput = stringInput.Replace("[IconTiktok]", mimeEntity.ContentId);

        mimeEntity = builder.LinkedResources.Add($"{DirectorySettings.PathFileApp}/icons8-youtube-96.png");
        mimeEntity.ContentId = MimeUtils.GenerateMessageId();
        stringInput = stringInput.Replace("[IconYoutube]", mimeEntity.ContentId);

        mimeEntity = builder.LinkedResources.Add($"{DirectorySettings.PathFileApp}/icons8-whatsapp-96.png");
        mimeEntity.ContentId = MimeUtils.GenerateMessageId();
        stringInput = stringInput.Replace("[IconWhatsapp]", mimeEntity.ContentId);
        return stringInput;
    }
}
