using System;
using System.ComponentModel.DataAnnotations;

namespace CursedPathWebApp.Models
{
    // This is the model for the JSON data that we send to the client
    // for each message. We are not sending back the actual "Message" object
    // to the client because we are not wanting to send back all of the data
    // about the ApplicationUser who authored the message. This allows us to
    // return only the UserName of the ApplicationUser who wrote the message.
    public class MessageViewModel
    {
        public MessageViewModel() { }
        public MessageViewModel(Post message)
        {
            Content = message.Message;
            Author = message.Username;
            Timestamp = message.DatePosted;
        }
        [Required]
        public string Content { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Timestamp { get; set; }
    }
}