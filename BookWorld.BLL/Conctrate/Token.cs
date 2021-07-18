using BookWorld.BLL.Abstract;
using BookWorld.Entity.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BookWorld.BLL.Conctrate
{
    public class Token : IToken
    {
        private readonly string _key = "Karabiberim vur kadehlere hadi içelim, içelim her gece zevki sefa, doldu gönmlüme. Hadi içelim, acıların yerine...";
        public string Generate(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    //Token içerisinde aşağıdaki bilgiler tutulacak.
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Surname, user.Surname),
                    new Claim(ClaimTypes.Email, user.Username),
                }),
                Expires = DateTime.Now.AddMinutes(15), //Tokenımın yaşam süresi 15 dk olsun

                //key ve keyin şifreleme algoritması
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public Users ReadPayload(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            //gelen tokenı decode ettik
            var jwtSecurityToken = handler.ReadJwtToken(token);
            return new Users
            {
                //tokendaki payloadın içerisinde bulunan değerleri alıp Users nesnesinin içerisine atıyoruz.
                Id = Convert.ToInt32(jwtSecurityToken.Payload.FirstOrDefault(x => x.Key == "nameid").Value),
                Name = jwtSecurityToken.Payload.FirstOrDefault(x => x.Key == "unique_name").Value.ToString(),
                Surname = jwtSecurityToken.Payload.FirstOrDefault(x => x.Key == "family_name").Value.ToString(),
                Username = jwtSecurityToken.Payload.FirstOrDefault(x => x.Key == "email").Value.ToString()
            };
        }
    }
}
