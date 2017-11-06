using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using Microsoft.AspNetCore.SignalR.Server;
using CursedPathWebApp.Data;
using CursedPathWebApp.Models;
using CursedPathWebApp.Hubs;

namespace CursedPathWebApp.Controllers
{
    [Produces("application/json")]
    public class ChatController : ApiHubController<Broadcaster>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;

        public ChatController(UserManager<ApplicationUser> userManager, ApplicationDbContext ctx, IConnectionManager connectionManager)
        : base(connectionManager)
        {
            _userManager = userManager;
            _context = ctx;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> Get()
        {
            Post[] messages = await _context.Posts.ToArrayAsync();
            List<MessageViewModel> model = new List<MessageViewModel>();
            foreach (Post msg in messages)
            {
                model.Add(new MessageViewModel(msg));
            }
            return Json(model);
        }

        [HttpPost]
        [Route("[controller]")]
        public async Task<IActionResult> Post([FromBody]NewMessageViewModel message)
        {
            if (ModelState.IsValid)
            {
                // Get the current user
                var user = await GetCurrentUserAsync();
                if (user == null) return Forbid();

                // Create a new message to save to the database
                Post newMessage = new Post()
                {
                    Message = message.Content,
                    Username = user.UserName,
                    DatePosted = DateTime.Now
                };

                // Save the new message
                await _context.AddAsync(newMessage);
                await _context.SaveChangesAsync();

                MessageViewModel model = new MessageViewModel(newMessage);

                // Call the client method 'addChatMessage' on all clients in the
                // "MainChatroom" group.
                this.Clients.Group("MainChatroom").AddChatMessage(model);
                return new NoContentResult();
            }
            return BadRequest(ModelState);
        }
    }
}