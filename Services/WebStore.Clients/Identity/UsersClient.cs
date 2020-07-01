using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.DTO.Identity;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces.Services.Identity;

namespace WebStore.Clients.Identity
{
    public class UsersClient : BaseClient, IUsersClient
    {
        public UsersClient(IConfiguration Configuration) : base(Configuration, WebAPI.Identity.Users) { }
    }
}
