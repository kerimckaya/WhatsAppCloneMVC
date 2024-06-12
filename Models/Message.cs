using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatsAppCloneMVC.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public int SenderId { get; set; }
        public virtual User Sender { get; set; }
        public int ReceiverId { get; set; }
        public virtual User Receiver { get; set; }
    }
}
