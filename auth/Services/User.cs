namespace Pluralsight.AspNetCore.Auth.Web.Services
{
    public class User
    {
        public User(string username)
        {
            Username = username;
        }

        public string Username { get;}
    }
}