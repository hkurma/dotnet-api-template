using AutoMapper;
using DotNet.Template.Application.DTOs;
using DotNet.Template.Domain.Entities;

namespace DotNet.Template.Application.Mappings;

public class TodoMappingProfile : Profile
{
    public TodoMappingProfile()
    {
        CreateMap<TodoItem, TodoItemDto>();
        CreateMap<CreateTodoItemDto, TodoItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => DotNet.Template.Domain.Enums.TodoStatus.Pending))
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore());
    }
}
