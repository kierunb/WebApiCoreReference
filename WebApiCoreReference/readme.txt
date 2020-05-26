https://code-maze.com/automapper-net-core/



public UserProfile()
{
    CreateMap<User, UserViewModel>();
}

public UserProfile()
{
    CreateMap<User, UserViewModel>()
        .ForMember(dest =>
            dest.FName,
            opt => opt.MapFrom(src => src.FirstName))
        .ForMember(dest =>
            dest.LName,
            opt => opt.MapFrom(src => src.LastName))
        .ReverseMap();
}

public UserController(IMapper mapper)
{
    _mapper = mapper;
}