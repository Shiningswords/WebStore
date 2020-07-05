using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebStore.Infrastructure.MiddleWare
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _Next;

        public ErrorHandlingMiddleware(RequestDelegate Next)
        {
            _Next = Next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _Next(context);
        }
    }
}
