﻿using Microsoft.AspNetCore.Authorization;

namespace PocketSizedUniverseShared.RequirementHandlers;

public class UserRequirement : IAuthorizationRequirement
{
    public UserRequirement(UserRequirements requirements)
    {
        Requirements = requirements;
    }

    public UserRequirements Requirements { get; }
}
