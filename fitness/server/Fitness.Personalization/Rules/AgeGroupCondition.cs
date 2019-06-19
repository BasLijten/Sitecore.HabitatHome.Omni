﻿using Sitecore.Analytics;
using Sitecore.Analytics.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.HabitatHome.Fitness.Collection.Services;
using Sitecore.HabitatHome.Fitness.Personalization.Utils;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;
using System.Web.Mvc;

namespace Sitecore.HabitatHome.Fitness.Personalization.Rules
{
    public class AgeGroupCondition<T> : OperatorCondition<T> where T : RuleContext
    {
        public string AgeGroupProfileKeyId { get; set; }

        protected override bool Execute(T ruleContext)
        {
            if (!Tracker.Current.IsActive)
            {
                return false;
            }

            var service = DependencyResolver.Current.GetService<IDemographicsService>();

            if (service == null)
            {
                Log.Error("AgeGroupCondition failed. IDemographicsService is not available", this);
                return false;
            }

            var profileKeyName = ProfileExtensions.GetProfileKeyName(AgeGroupProfileKeyId);
            var ageGroup = service.GetAgeGroup();
            var result = profileKeyName.Equals(ageGroup, System.StringComparison.InvariantCultureIgnoreCase);
            Log.Debug($"{this.GetType().Name}: {profileKeyName}.Equals('{ageGroup}', System.StringComparison.InvariantCultureIgnoreCase) = {result}");
            return result;
        }
    }
}