﻿using System.Net;
using Sitecore.LayoutService.Mvc.Security;
using System.Web.Mvc;
using System;
using Sitecore.HabitatHome.Fitness.Collection.Model;
using Sitecore.HabitatHome.Fitness.Collection.Services;
using Sitecore.HabitatHome.Fitness.Collection.Filters;
using Sitecore.Diagnostics;

namespace Sitecore.HabitatHome.Fitness.Collection.Controllers
{
    [RequireSscApiKey]
    [ImpersonateApiKeyUser]
    [EnableApiKeyCors]
    [SuppressFormsAuthenticationRedirect]
    public class HabitatFitnessDemographicsController : Controller
    {
        private IDemographicsService service;

        public HabitatFitnessDemographicsController([NotNull]IDemographicsService service)
        {
            this.service = service;
        }

        [ActionName("facet")]
        [HttpPost]
        [CancelCurrentPage]
        public ActionResult UpdateFacet([System.Web.Http.FromBody]DemographicsPayload data)
        {
            try
            {
                service.UpdateFacet(data);
            }
            catch (Exception ex)
            {
                Log.Error($"Error while running UpdateFacet API", ex, this);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [ActionName("profile")]
        [HttpPost]
        [CancelCurrentPage]
        public ActionResult UpdateProfile([System.Web.Http.FromBody]DemographicsPayload data)
        {
            try
            {
                service.UpdateProfile(data);
            }
            catch (Exception ex)
            {
                Log.Error($"Error while running UpdateProfile API", ex, this);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}