using WebStore.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.DAL.Context;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebStore.Domain.Entities.Identity;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebAPI.Identity.Users)]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly UserStore<User, Role, WebStoreDb> _UserStore;
        public UsersApiController(WebStoreDb db)
        {
            _UserStore = new UserStore<User, Role, WebStoreDb>(db);
        }
    }
}
