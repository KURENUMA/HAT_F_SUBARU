using HAT_F_api.CustomModels;
using HAT_F_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Diagnostics;

namespace HAT_F_api.Controllers
{
    [DebuggerStepThrough]
    public class ApiLogicRunner
    {
        public static async Task<ActionResult<ApiResponse<T>>> RunAsync<T>(Func<Task<T>> logic)
        {
            try
            {
                var result = await logic();
                return new ApiOkResponse<T>(result);
            }
            catch (DbUpdateConcurrencyException)
            {                
                string message = "他のユーザーが先に更新したデータがあるため、更新できませんでした。";
                return new ApiErrorResponse<T>(ApiResultType.ApiDbUpdateConcurrencyError, message);
            }
            catch (HatFApiServiceException ex)
            {
                return new ApiErrorResponse<T>(ApiResultType.ServerInternalError, ex.Message);
            }
        }

        public static async Task<ActionResult<ApiResponse>> RunAsync(Func<Task> logic)
        {
            try
            {
                await logic();
                return new ApiOkResponse();
            }
            catch (DbUpdateConcurrencyException)
            {
                string message = "他のユーザーが先に更新したデータがあるため、更新できませんでした。";
                return new ApiErrorResponse(ApiResultType.ApiDbUpdateConcurrencyError, message);
            }
            catch (HatFApiServiceException ex)
            {
                return new ApiErrorResponse(ApiResultType.ServerInternalError, ex.Message);
            }
        }

    }
}
