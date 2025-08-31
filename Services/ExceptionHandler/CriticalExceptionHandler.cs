﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.ExceptionHandler
{
	public class CriticalExceptionHandler() : IExceptionHandler
	{
		public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
		{
			if (exception is CriticalException)
			{
				Console.WriteLine($"Hata ile ilgili sms gönderildi.");
			}

			return ValueTask.FromResult(false); // eğer varsa diğer handler'lara geçilsin
		}
	}
}
