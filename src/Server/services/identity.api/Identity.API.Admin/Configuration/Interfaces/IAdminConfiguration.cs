﻿namespace Identity.API.Admin.Configuration.Interfaces
{
    public interface IAdminConfiguration
    {
        string IdentityAdminRedirectUri { get; }

        string IdentityServerBaseUrl { get; }

        string IdentityAdminBaseUrl { get; }
    }
}