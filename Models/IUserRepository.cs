namespace StaycationHotel.Models
{
    public interface IUserRepository
    {
        Task<Response> AddUserAsync(User user);

        Task<User> LoginUser(string email, string password);
    }
}
