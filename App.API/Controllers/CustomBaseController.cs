using App.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomBaseController : ControllerBase
	{
		[NonAction]
		public IActionResult CreateActionResult<T>(ServiceResult<T> serviceResult)
		{
			return serviceResult.Status switch
			{
				HttpStatusCode.NoContent => NoContent(),
				HttpStatusCode.Created => Created(serviceResult.UrlAsCreated, serviceResult),
				_ => new ObjectResult(serviceResult) { StatusCode = serviceResult.Status.GetHashCode() }
			};
		}

		[NonAction]
		public IActionResult CreateActionResult(ServiceResult serviceResult)
		{
			return serviceResult.Status switch
			{
				HttpStatusCode.NoContent => new ObjectResult(null) { StatusCode = serviceResult.Status.GetHashCode() },
				_ => new ObjectResult(serviceResult) { StatusCode = serviceResult.Status.GetHashCode() }
			};
		}
	}
}
