using AutoMapper;
using dotnetdevs.Models;
using dotnetdevs.Services;
using dotnetdevs.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetdevs.Controllers
{
	public class ConversationsController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly CompanyService _companyService;
		private readonly DeveloperService _developerService;
		private readonly ConversationService _conversationService;
		private readonly MessageService _messageService;
		private readonly UserService _userService;
		private readonly IMapper _mapper;

		public ConversationsController(ILogger<HomeController> logger, CompanyService companyService, UserService userService, DeveloperService developerService, ConversationService conversationService, MessageService messageService, IMapper mapper)
		{
			_logger = logger;
			_companyService = companyService;
			_userService = userService;
			_developerService = developerService;
			_conversationService = conversationService;
			_messageService = messageService;
			_mapper = mapper;
		}

		[HttpGet]
		[Authorize]
		[Route("conversations")]
		public async Task<IActionResult>  Index()
		{
			var user = await _userService.GetAuthenticatedUser(this.User);
			var company = await _companyService.GetByUserId(user.Id);
			var developer = await _developerService.GetByUserId(user.Id);
			var developerConversations = new List<Conversation>();
			var companyConversations = new List<Conversation>();

			if (developer != null) {
				developerConversations = await _conversationService.GetAllByDeveloper(developer.ID);
			}

			if (company != null) {
				companyConversations = await _conversationService.GetAllByCompany(company.ID);
			}

			var model = new ConversationIndex() {
				HasCompanyProfile = company != null? true : false,
				HasDeveloperProfile = developer != null? true: false,
				Developer = developer,
				Company = company,
				DeveloperConversations = developerConversations,
				CompanyConversations = companyConversations
			};

			return View(model);
		}

		[HttpGet]
		[Authorize]
		[Route("conversations/show/{id}")]
		public async Task<IActionResult> Show(int id)
		{
			var user = await _userService.GetAuthenticatedUser(this.User);
			var conversation = await _conversationService.Get(id);
			var sender = "";
			if (user.Id != conversation.Company.UserID && user.Id != conversation.Developer.UserID)
			{
				return StatusCode(401);
			}
			if (conversation == null)
			{
				return NotFound();
			}

			if (user.Id == conversation.Company.UserID) {
				// mark developer messages to company as read
				await _messageService.UpdateMessagesToRead(conversation.ID, "DEVELOPER");
				sender = "COMPANY";
			}
			if (user.Id == conversation.Developer.UserID) {
				// mark company messages to developer as read
				await _messageService.UpdateMessagesToRead(conversation.ID, "COMPANY");
				sender = "DEVELOPER";
			}
			var messages = await _messageService.GetConversationMessages(conversation.ID);

			ConversationShow model = new ConversationShow() {
				User = user,
				Conversation = conversation,
				Messages = messages,
				UserIsCompany = user.Id == conversation.Company.UserID ? true : false,
				Sender = sender,
				DeveloperId = conversation.Developer.ID,
				CompanyId = conversation.Company.ID
			};
			return View(model);
		}

		[HttpGet]
		[Authorize]
		[Route("developers/{id}/conversations/create")]
		public async Task<IActionResult> Create(int id)
		{
			var user = await _userService.GetAuthenticatedUser(this.User);
			var company = await _companyService.GetByUserId(user.Id);
			if (company == null)
			{
				return RedirectToAction("Create", "Companies");
			}
			if (!company.IsSubscribed)
			{
				return RedirectToAction("Pricing", "Home");
			}
			var developer = await _developerService.Get(id);
			if (developer == null)
			{
				return NotFound();
			}

			var existingConversation = await _conversationService.Get(developer.ID, company.ID);
			if (existingConversation != null)
			{
				return RedirectToAction("Show", "Conversations", new { id = existingConversation.ID });
			}

			var model = new ConversationCreate()
			{
				DeveloperId = developer.ID,
				Developer = developer,
				Text = ""
			};
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Store(ConversationCreate conversation)
		{
			var user = await _userService.GetAuthenticatedUser(this.User);
			var company = await _companyService.GetByUserId(user.Id);
			if (company == null)
			{
				return RedirectToAction("Create", "Companies");
			}
			if (!company.IsSubscribed)
			{
				return RedirectToAction("Pricing", "Home");
			}
			var developer = await _developerService.Get(conversation.DeveloperId);
			if (developer == null)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				Conversation newConversation = new Conversation();
				newConversation.DeveloperID = developer.ID;
				newConversation.CompanyID = company.ID;
				newConversation.CreatedDate = DateTime.Now;

				newConversation = await _conversationService.Store(newConversation);

				// save message
				Message newMessage = new Message();
				newMessage.ConversationID = newConversation.ID;
				newMessage.Sender = "COMPANY";
				newMessage.HasBeenRead = false;
				newMessage.Text = conversation.Text;
				newMessage.CreatedDate = DateTime.Now;

				newMessage = await _messageService.Store(newMessage);
				return RedirectToAction("Show", "Conversations", new { id = newConversation.ID });
			}

			conversation.Developer = developer;
			return View("Create", conversation);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		[Route("conversations/{id}/store-message")]
		public async Task<IActionResult> StoreMessage(int id)
		{
			return RedirectToAction("Show", "Conversations", new { id = id });
		}
	}
}
