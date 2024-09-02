using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using Google.Protobuf;
using Infrastructure.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Win32.SafeHandles;
using UserApp.Application.Commands;
using UserApp.Application.Protos;
using UserApp.Core.Entities;
namespace UserApp.Application.Mapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ByteStringModel, FormFileModel>()
                    .ForMember(dest => dest.FormFileProperty, opt =>
                        opt.MapFrom(src => new ByteStringToIFormFileConverter(src.FileName, src.ContentType)
                            .Convert(src.Base64String, null!)));
        //mapping request to command

        CreateMap<UserCreateRequest, UserCreateCommand>();
        CreateMap<UserUpdateRequest, UserUpdateCommand>();
        ;
        //mappinng from command to entity
        CreateMap<UserCreateCommand, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Ulid.NewUlid().ToString()))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password.EncryptString()))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
            ;
        ;
        CreateMap<UserUpdateCommand, User>()
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now));
        ;

        //        const string dateFormat = "yyyy-MM-dd";
        CreateMap<User, UserModel>()
            .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName ?? string.Empty))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName ?? string.Empty))
            .ForMember(dest => dest.MobileNumber, opt => opt.MapFrom(src => src.MobileNumber ?? string.Empty))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber ?? string.Empty))
            .ForMember(dest => dest.OrgId, opt => opt.MapFrom(src => src.OrgId ?? string.Empty))
            //.ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy ?? string.Empty))
            //.ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate ?? default(DateTime)  ))
            .ForMember(dest => dest.LastLogin, opt => opt.MapFrom(src => src.LastLogin ?? default(DateTime)))
            //.ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => SetFullname(src)))
            //.ReverseMap()
            //.ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.ParseExact(src.CreatedDate, dateFormat, CultureInfo.InvariantCulture)))
            ;

        ;
    }
    private static string SetFullname(User user)
    {
        string fullname = user.FirstName;
        if (!string.IsNullOrEmpty(user.MiddleName))
            fullname += " " + user.MiddleName;
        if (!string.IsNullOrEmpty(user.LastName))
            fullname += " " + user.LastName;
        return fullname;
    }
}
