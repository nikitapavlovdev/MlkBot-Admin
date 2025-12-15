using Microsoft.EntityFrameworkCore;
using MlkAdmin._1_Domain.Entities;

namespace MlkAdmin._3_Infrastructure.DataBase;

public class MlkAdminDbContext(DbContextOptions<MlkAdminDbContext> options) : DbContext(options)
{
    public DbSet<GuildMember> GuildMembers { get; set; }
    public DbSet<GuildVoiceChannel> VChannels { get; set; }
    public DbSet<GuildTextChannel> TChannels { get; set; }
    public DbSet<GuildMessage> GuildMessages { get; set; }
    public DbSet<GuildVoiceSession> GuildVoiceSessions { get; set; }

}
