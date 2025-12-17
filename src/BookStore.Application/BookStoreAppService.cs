using BookStore.Localization;
using Volo.Abp.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace BookStore;

/* Inherit your application services from this class.
 */
[AllowAnonymous]
public abstract class BookStoreAppService : ApplicationService
{
    protected BookStoreAppService()
    {
        LocalizationResource = typeof(BookStoreResource);
    }
}
