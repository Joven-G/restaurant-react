﻿using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Services.Interfaces;
using Identity.API.Admin.ExceptionHandling;
using Identity.API.Admin.Configuration.Constants;
using Skoruba.IdentityServer4.Admin.EntityFramework.DbContexts;
using Skoruba.IdentityServer4.Admin.EntityFramework.Identity.Entities.Identity;

namespace Identity.API.Admin.Controllers
{
    [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
    [TypeFilter(typeof(ControllerExceptionFilterAttribute))]
    public class IdentityController : BaseIdentityController<AdminDbContext, UserDto<int>, int, RoleDto<int>, int, int, int, UserIdentity, UserIdentityRole, int, UserIdentityUserClaim, UserIdentityUserRole, UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken>
    {
        public IdentityController(IIdentityService<AdminDbContext, UserDto<int>, int, RoleDto<int>, int, int, int, UserIdentity, UserIdentityRole, int, UserIdentityUserClaim, UserIdentityUserRole, UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken> identityService, ILogger<ConfigurationController> logger, IStringLocalizer<IdentityController> localizer)
            : base(identityService, logger, localizer)
        {
        }
    }
}