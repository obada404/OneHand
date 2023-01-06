using OneHandTraining.DTO;
using OneHandTraining.model;
using Profile = AutoMapper.Profile;


public class ProfileMapper:Profile
{
    public ProfileMapper()
    {
        CreateMap<Article, ArticleRequest>();
        CreateMap<UserRequest, UserOld>();

    }
}

