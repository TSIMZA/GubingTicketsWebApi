using GubingTickets.DataAccessLayer.Implementations;
using GubingTickets.DataAccessLayer.Utils.ConnectionFactory;
using GubingTickets.Models.ApiModels.Requests;
using GubingTickets.Models.ApiModels.Responses.Base;
using GubingTickets.Web.ApplicationLayer.BusinessLogic.Implementations;
using GubingTickets.Web.ApplicationLayer.BusinessLogic.Interfaces;
using GubingTickets.WebApi.NetFramework.Controllers.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace GubingTickets.WebApi.NetFramework.Controllers
{

    public class UserAuthController : BaseController
    {
        public UserAuthController() : base()
        {
        }

        //[HttpGet]
        //[Route("GenerateJwt")]
        //public async Task<HttpResponseMessage> GenerateJwt(string username, string password)
        //{
        //    return await RequestHandler(async () =>
        //    {
        //        return await Task.Run(() =>
        //        {
        //            string key = "my_secret_key_12345"; //Secret key which will be used later during validation    
        //                string issuer = "http://ticketsapi.gubinglifestyle.co.za/";  //normally this will be your site URL    

        //                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        //            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //                //Create a List of Claims, Keep claims name short    
        //                List<Claim> permClaims = new List<Claim>();
        //            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        //            permClaims.Add(new Claim("valid", "1"));
        //            permClaims.Add(new Claim("userid", "1"));
        //            permClaims.Add(new Claim("name", "bilal"));

        //                //Create Security Token object by giving required parameters    
        //                JwtSecurityToken token = new JwtSecurityToken(issuer, //Issure    
        //                            issuer,  //Audience    
        //                            permClaims,
        //                            expires: DateTime.Now.AddDays(1),
        //                            signingCredentials: credentials);
        //            string jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
        //            return new BaseResponse<string>
        //            {
        //                Success = true,
        //                Data = jwt_token,
        //            };
        //        });
        //    });
        //}

    }
}