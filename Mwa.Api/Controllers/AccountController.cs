using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mwa.Api.Security;
using Mwa.Domain.Comand.Inputs;
using Mwa.Domain.Comand.Results;
using Mwa.Domain.Entities;
using Mwa.Domain.Repositories;
using Mwa.Domain.ValueObjects;
using Mwa.Infra.Transactions;
using Mwa.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Mwa.Api.Controllers
{
    public class AccountController : BaseController
    {

        private readonly ICustumerRepository _repository;
        private  GetCustomerCommandResult _customer;

        private readonly TokenOptions _tokenOptions;
        private readonly JsonSerializerSettings _serializerSettings;
        

        public AccountController(IOptions<TokenOptions> jwtOptions, IUoW uow, ICustumerRepository repository)
        : base(uow)
        {
            _repository = repository;
            _tokenOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_tokenOptions);

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("v1/authenticate")]
        public async Task<IActionResult> Post([FromForm] AuthenticateUserComand command)
        {
            if (command == null)
                return await Response(null, new List<Notification> { new Notification("User", "Usuário ou senha inválidos")});

            var identity = await GetClaims(command);
            if (identity == null)
                return await Response(null, new List<Notification> { new Notification("User", "Usuário ou senha inválidos") });

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, command.UserName),
                new Claim(JwtRegisteredClaimNames.Email, command.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, command.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, await _tokenOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_tokenOptions.IssueAt).ToString(), ClaimValueTypes.Integer64),
                identity.FindFirst("ModernStore")
            };

            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: claims.AsEnumerable(),
                notBefore: _tokenOptions.NotBefore,
                expires: _tokenOptions.Expiration,
                signingCredentials: _tokenOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                token = encodedJwt,
                expires = (int)_tokenOptions.ValidFor.TotalSeconds,
                user = new
                {
                    id = _customer.Id,
                    name = _customer.Name.ToString(),
                    email = _customer.Email,
                    username = _customer.UserName
                }
            };

            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }



        private static void ThrowIfInvalidOptions(TokenOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("O período deve ser maior que zero", nameof(TokenOptions.ValidFor));

            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(TokenOptions.SigningCredentials));

            if (options.JtiGenerator == null)
                throw new ArgumentNullException(nameof(TokenOptions.JtiGenerator));
        }

        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);


        private Task<ClaimsIdentity> GetClaims(AuthenticateUserComand command)
        {
            
           
           var customer = _repository.Get(command.UserName);
           var pass = Encripty.EncryptPassword(command.password).ToString().Substring(0,12);
           
            if(customer == null)
                return Task.FromResult<ClaimsIdentity>(null);

            if(!(customer.UserName == command.UserName && customer.Password == pass))
                return Task.FromResult<ClaimsIdentity>(null);
            

                _customer = customer;

            return Task.FromResult(new ClaimsIdentity(
                new GenericIdentity(customer.UserName, "Token"),
                new[]{
                    new Claim("TES", "User")
                }
            ));
        

        }
    }
}