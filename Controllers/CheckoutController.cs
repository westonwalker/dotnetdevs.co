﻿using AutoMapper;
using dotnetdevs.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace dotnetdevs.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly CompanyService _companyService;
		private readonly UserService _userService;

		public CheckoutController(ILogger<HomeController> logger, CompanyService companyService, UserService userService)
		{
			_logger = logger;
			_companyService = companyService;
			_userService = userService;
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Create()
		{
			var user = await _userService.GetAuthenticatedUser(this.User);
			var existingCompany = await _companyService.GetByUserId(user.Id);
			if (existingCompany == null)
			{
				return RedirectToAction("Create", "Companies");
			}
			if (existingCompany.IsSubscribed)
			{
				return RedirectToAction("Edit", "Companies");
			}

			var paymentGuid = Guid.NewGuid().ToString();
			existingCompany.PaymentGuid= paymentGuid;
			await _companyService.Update(existingCompany);

			var priceId = Environment.GetEnvironmentVariable("STRIPE_PRICE_ID");
			var domain = Environment.GetEnvironmentVariable("APP_URL");

			var options = new SessionCreateOptions
			{
				LineItems = new List<SessionLineItemOptions>
				{
				  new SessionLineItemOptions
				  {
					Price = priceId,
					Quantity = 1,
				  },
				},
				Mode = "subscription",
				SuccessUrl = domain + $"/checkout/success/{paymentGuid}",
				CancelUrl = domain + "/hire",
			};
			var service = new SessionService();
			Session session = service.Create(options);

			return Redirect(session.Url);
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Success(string id)
		{
			var user = await _userService.GetAuthenticatedUser(this.User);
			var existingCompany = await _companyService.GetByUserId(user.Id);
			if (existingCompany == null)
			{
				return RedirectToAction("Create", "Companies");
			}
			if (existingCompany.PaymentGuid != id)
			{
				return NotFound();
			}
			existingCompany.IsSubscribed = true;
			await _companyService.Update(existingCompany);
			return View();
		}
	}
}
