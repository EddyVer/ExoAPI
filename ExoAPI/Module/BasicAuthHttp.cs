﻿//using Microsoft.AspNetCore.Hosting.Server;
//using System.Net;
//using System.Net.Http.Headers;
//using System.Security.Principal;
//using System.Text;

//namespace ExoAPI.Module
//{
//    public class BasicAuthHttp : AuthorizationFilterAttribute
//    {

//        public override void OnAuthorization(HttpActionContext actionContext)
//        {
//            if (actionContext.Request.Headers.Authorization != null)
//            {
//                var authToken = actionContext.Request.Headers
//                    .Authorization.Parameter;

//                // decoding authToken we get decode value in 'Username:Password' format  
//                var decodeauthToken = System.Text.Encoding.UTF8.GetString(
//                    Convert.FromBase64String(authToken));

//                // spliting decodeauthToken using ':'   
//                var arrUserNameandPassword = decodeauthToken.Split(':');

//                // at 0th postion of array we get username and at 1st we get password  
//                if (IsAuthorizedUser(arrUserNameandPassword[0], arrUserNameandPassword[1]))
//                {
//                    // setting current principle  
//                    Thread.CurrentPrincipal = new GenericPrincipal(
//                           new GenericIdentity(arrUserNameandPassword[0]), null);
//                }
//                else
//                {
//                    actionContext.Response = actionContext.Request
//                        .CreateResponse(HttpStatusCode.Unauthorized);
//                }
//            }
//            else
//            {
//                actionContext.Response = actionContext.Request
//                    .CreateResponse(HttpStatusCode.Unauthorized);
//            }
//        }

//    }
//}
