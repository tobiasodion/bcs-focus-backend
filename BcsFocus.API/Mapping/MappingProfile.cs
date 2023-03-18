using AutoMapper;
using BcsFocus.API.DTO;
using BcsFocus.API.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Module, GetModulesResponse>();
    }
}