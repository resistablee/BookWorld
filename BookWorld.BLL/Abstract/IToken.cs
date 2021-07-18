using BookWorld.Entity.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace BookWorld.BLL.Abstract
{
    public interface IToken
    {
        string Generate(Users user);
        Users ReadPayload(string token);
    }
}
