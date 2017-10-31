using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NNR.UIEntity.VM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NNR.UIEntity.Model;

namespace NNR.WEB.Controllers
{
    public class AccountController : BaseController
    {
        //
        // GET: /Default/
        [Route("Login")]
        public ActionResult Login()
        {
            return PartialView("_Login", new LoginVM());
        }


        //
        // GET: /Default/
        [Route("UserLogin")]
        [HttpPost]
        public async Task<ActionResult> UserLogin(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    // New code:
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.PostAsync("Token", new StringContent("grant_type=password&username=" + loginVM.Email + "&password=" +
                        loginVM.Password)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string str = await response.Content.ReadAsStringAsync();
                        JObject obj = (JObject)JsonConvert.DeserializeObject(str);

                        OAuthModel _oAuth = new OAuthModel();
                        _oAuth.LstRoles = new List<string>();
                        foreach (JProperty jprop in obj.Properties())
                        {
                            if (jprop.Name == "access_token")
                            {
                                _oAuth.AuthToken = jprop.Value.ToString();
                                continue;
                            }
                            if (jprop.Name == "Email")
                            {
                                _oAuth.Email = jprop.Value.ToString();
                                continue;
                            }
                            if (jprop.Name == "UName")
                            {
                                _oAuth.UserName = jprop.Value.ToString();
                                continue;
                            }
                            _oAuth.LstRoles.Add(jprop.Value.ToString());

                        }
                        this.HttpContext.Session["OAuthObj"] = _oAuth;
                        return RedirectToAction("PhysicianView", "Physician");
                    }
                    else
                    {
                       // input.Message = "Invalid username or password";
                    }
                }
            }
            return PartialView("_Login", loginVM);
        }

        //
        // GET: /Default/
        [Route("CreateUser")]
        [HttpGet]
        public ActionResult CreateUser()
        {
            return PartialView("../Partial/_CreateUser", new CreateUserVM());
        }
        //
        // GET: /Default/
          [Route("CreateNewUser")]
        [HttpPost]
        public ActionResult CreateNewUser(CreateUserVM cUserUM)
        {
            HttpResponseMessage response = this.PostAsJsonAsync("Account/Register", cUserUM).Result;

            return PartialView("../Partial/_CreateUser", new CreateUserVM());
        }

	}
}