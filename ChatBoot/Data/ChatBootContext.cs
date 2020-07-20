using Microsoft.EntityFrameworkCore;
using ChatBoot.Models;

namespace ChatBoot.Data {
    public class ChatBootContext : DbContext {
        public ChatBootContext (DbContextOptions<ChatBootContext> options) : base(options) {
            Database.EnsureCreated();
        }

        public DbSet<RespostaChat> RespostasChat { get; set; }
    }
}
