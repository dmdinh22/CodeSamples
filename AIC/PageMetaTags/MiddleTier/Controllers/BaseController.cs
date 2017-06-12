using Aic.Web.Domain;
using Aic.Web.Models.ViewModels;
using Aic.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Aic.Web.Controllers
{
    public class BaseController : Controller
    {
        public new ViewResult View()
        {
            BaseViewModel model = GetViewModel<BaseViewModel>();
            DecorateViewModel<BaseViewModel>(model);
            return base.View(model);
        }

        public new ViewResult View(string viewString)
        {
            return base.View(viewString);
        }

        public ViewResult View(BaseViewModel model)
        {
            if (model != null)
            {
                DecorateViewModel<BaseViewModel>(model);
            }
            return base.View(model);
        }

        public ViewResult View(string viewString, BaseViewModel model)
        {
            if (model != null)
            {
                model = DecorateViewModel<BaseViewModel>(model);
            }
            return base.View(viewString, model);
        }

        protected T GetViewModel<T>() where T : BaseViewModel, new()
        {
            T model = new T();
            return model;
        }

        protected T DecorateViewModel<T>(T model) where T : BaseViewModel
        {
            string complexUserId = UserService.GetCurrentUserId();
            if (!string.IsNullOrEmpty(complexUserId))
            {
                //the below method checks if the user is logged in when it gets their UserId 
                HydrateUser(model);
                // Hydrate personsettings according to personId
                HydratePersonSettings(model);
            }
            // Hydrate metatags according to owner
            HydrateMetaTags(model);
            return model;
        }

        private void HydrateMetaTags<T>(T model) where T : BaseViewModel
        {
            List<PageMetaTags> metatag = new List<PageMetaTags>(); // instantiate the constructor to match the return type
            MetaTagsService metatagService = new MetaTagsService(); // instantiating the method because it's not static


            string currentUrl = Request.Url.AbsolutePath; // grab the currentURL to pass into service
            string baseUrl = Request.ApplicationPath.TrimEnd('/') + "/";            //string currentUrl = Request.Url.ToString(); // grab the currentURL to pass into service
            model.BaseUrl = Request.Url.GetLeftPart(UriPartial.Authority);

            if (currentUrl != baseUrl)
            {
                string[] currentUrlString = Request.Url.Segments;//gets the segments and sets it to an array
                string currentUrlFull = currentUrlString[1].ToLower(); // making path lowercase
                if (currentUrlFull.Contains('/'))
                {
                    currentUrl = currentUrlFull.Substring(0, currentUrlFull.Length - 1); // grab substring to remove the last "/"
                }
                else
                {
                    currentUrl = currentUrlFull;
                }
            }

            metatag = metatagService.PageMetaTags_SelectAllByOwnerName(currentUrl); // setting metatag to list result

            // Put an if condition here to check if this request is from the /job/options page
            if (Request.Url.AbsolutePath.ToLower().Contains("/jobs/option/"))
            {
                //Call to the Job Service to get Job Model
                JobService svc = new JobService();
                //Access Job Id from Base View Model
                Job j = svc.JobGetById(model.JobId);

                metatag[0].MetaTagValue = j.Title;
                metatag[2].MetaTagValue = Request.Url.AbsoluteUri;

                string jobDescription = Regex.Replace(j.Description, "<.*?>", string.Empty);
                jobDescription = Regex.Replace(jobDescription, "\n", " ");
                jobDescription = Regex.Replace(jobDescription, "&.*?;", " ");
                int i = Math.Min((jobDescription.Length - 1), 149); // finding the min between string length or index
                 metatag[4].MetaTagValue = jobDescription.Substring(0, i);
            }
            model.MetaTags = metatag;
        }

        private static void HydrateUser<T>(T model) where T : BaseViewModel
        {
            User user = new User();
            user = UserService.UserSelect();
            model.Id = user.Id;
            model.IsLoggedIn = true;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.EmailConfirmed = user.EmailConfirmed;
            model.Email = user.Email;
            model.PersonId = user.PersonId;
            model.Gender = user.Gender;
            model.UserRoles = user.UserRoles;
        }

    }
}

