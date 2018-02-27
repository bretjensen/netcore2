using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pluralsight.AspNetCore.Auth.Web.Services
{
  public class DummyUserService : IUserService
  {
    private IDictionary<string, (string passwordHash, User user)> _users = 
        new Dictionary<string, (string passwordHash, User user)>(); 
    public DummyUserService(IDictionary<string, string> users)
    {
      foreach (var user in users)
      {
        _users.Add(user.Key.ToLower(), 
          (BCrypt.Net.BCrypt.HashPassword(user.Value), new User(user.Key)));
      }
    }

    public Task<bool> ValidateCredentials(string username, string password, out User user)
    {
       user = null;
       var key = username.ToLower();
       if (_users.ContainsKey(key))
       {
         var hash = _users[key].passwordHash;
         if(BCrypt.Net.BCrypt.Verify(password, hash))
         {
           user = _users[key].user;
           return Task.FromResult(true);
         }
       }

       return Task.FromResult(false);
    }

  }
}