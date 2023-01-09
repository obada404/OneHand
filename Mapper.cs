using OneHandTraining.DTO;
using OneHandTraining.model;
using Profile = AutoMapper.Profile;


namespace OneHandTraining;

public class ArticleMapper:Profile
{
    public ArticleMapper()
    {
        CreateMap<Article, ArticleRequest>();
        CreateMap<ArticleRequest, Article>();
     //   CreateMap<List<ArticleRequest>, List<Article>>();

    }
}

public class UserMapper:Profile
{
    public UserMapper()
    {
        CreateMap<UserRegistrationRequest, UserOld>().ForMember(des=>des.Password,
            opt=>
                opt.MapFrom(src=>src.Password));
        CreateMap<UserRequest, UserOld>().ReverseMap();
        CreateMap<emailUserRequest, UserOld>();
        CreateMap<UserOld, Profile>();
 

    }
}
