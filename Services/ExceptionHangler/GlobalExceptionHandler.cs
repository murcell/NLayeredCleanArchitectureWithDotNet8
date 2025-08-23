﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace App.Services.ExceptionHangler
{
	public class GlobalExceptionHandler : IExceptionHandler
	{
		public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
		{
			var errorAsDto = ServiceResult.Fail(exception.Message, System.Net.HttpStatusCode.InternalServerError);

			httpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;

			httpContext.Response.ContentType = "application/json";

			await httpContext.Response.WriteAsJsonAsync(errorAsDto, cancellationToken);
				//.ContinueWith(t => true, cancellationToken); 

			return true; // burası true dönerse en son buraya gelicek
		}
	}
}
