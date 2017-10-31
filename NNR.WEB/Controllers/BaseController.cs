using NNR.UIEntity.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NNR.WEB.Controllers
{
    public class BaseController : Controller
    {
        public OAuthModel OAuthModel
        {
            get
            {
                OAuthModel oauthModel = (OAuthModel)this.HttpContext.Session["OAuthObj"];
                return oauthModel;
            }
        }

        private HttpClient client;

        public HttpClient HTTPClient
        {
            get
            {
                client = new HttpClient();

                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"] + "api/");
                client.DefaultRequestHeaders.Accept.Clear();
                if (OAuthModel != null)
                    client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}",
                       OAuthModel.AuthToken));
                return client;
            }
        }
        public ActionResult RedirectToLogin()
        {
            if (OAuthModel == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return RedirectToAction("PhysicianView", "Physician");
            }
        }
        public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string url, T model)
        {
            var response = HTTPClient.PostAsJsonAsync(url, model).Result;
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            GetExceptionCode(response);
            return null;
        }
        private void GetExceptionCode(HttpResponseMessage response)
        {
            var serviceException = response.Content.ReadAsStringAsync();
            throw new HttpException((int)response.StatusCode, serviceException.Result);

        }
	}
}