using Mapster;
using Users.Application.User.Add;

namespace Users.Application.Mappings;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddUserRequest, Domain.Entities.User>()
            .ConstructUsing(src => new Domain.Entities.User(
                Guid.NewGuid(), // Generate a new GUID for User ID
                src.FirstName,
                src.LastName,
                src.Email,
                src.Role
            ));
    }
}