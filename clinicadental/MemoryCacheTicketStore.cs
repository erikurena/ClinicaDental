using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace clinicadental
{
    public class MemoryCacheTicketStore : ITicketStore
    {
        private const string KeyPrefix = "AuthSessionStore-";
        private readonly IMemoryCache _cache;

        public MemoryCacheTicketStore(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            var key = KeyPrefix + Guid.NewGuid().ToString();
            await RenewAsync(key, ticket);
            return key;
        }

        public async Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) // Igual que el tiempo de vida de la sesión
            };
            _cache.Set(key, ticket, options);
            await Task.CompletedTask;
        }

        public async Task<AuthenticationTicket> RetrieveAsync(string key)
        {
            _cache.TryGetValue(key, out AuthenticationTicket ticket);
            return await Task.FromResult(ticket);
        }

        public async Task RemoveAsync(string key)
        {
            _cache.Remove(key);
            await Task.CompletedTask;
        }
    }
}