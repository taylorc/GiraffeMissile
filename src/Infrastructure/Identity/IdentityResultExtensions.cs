﻿using System.Linq;
using GiraffeMissile.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace GiraffeMissile.Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }
    }
}