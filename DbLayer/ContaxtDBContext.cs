
using Microsoft.EntityFrameworkCore;
using Models;
namespace DbLayer
{
    public class ContaxtDBContext : DbContext
    {
        //public DbSet<MyRequest>  myRequests { get; set; }

        //public DbSet<MyResponse>  myResponses { get; set; }
        public DbSet<Quest> quests { get; set; }
        public DbSet<User> users { get; set; }

        public DbSet<Title_Ques> titles { get; set; }

        public DbSet<Rezult> rezults { get; set; }



        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<MyRequest>()
        //    .HasOne(mr => mr.AuthUser)
        //    .WithMany()
        //    .HasForeignKey(mr => mr.Id)
        //    .OnDelete(DeleteBehavior.SetNull); // Встановити зовнішній ключ на null при видаленні

        //    modelBuilder.Entity<MyResponse>()
        //        .HasMany(mr => mr.quests)
        //        .WithOne()
        //        .HasForeignKey(q => q.Id)
        //        .OnDelete(DeleteBehavior.SetNull); // Встановити зовнішній ключ на null при видаленні

        //    base.OnModelCreating(modelBuilder);
        //}

        public ContaxtDBContext(DbContextOptions <ContaxtDBContext> options):base (options) 
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            Так_можно_називати_перемінні();
        }

        private void Так_можно_називати_перемінні()
        {
            
           ////ВАШІ ЛОГІН ТА ПАРОЛЬ.
            var user1 = new User {  Login = "1",  Password ="1", IsAdmin = false };
            var user2 = new User {  Login = "2",  Password ="2", IsAdmin = true};

            users.AddRange(user1, user2);


            //var ques = new Quest
            //{
            //    Vopros = "Скільки пальців на руці?",
            //    Quests1 = "Три",
            //    Quests2 = "чотири",
            //    Quests3 = "шість",
            //    Quests4 = "п'ять",
            //    right = "4"
            //};

            //quests.Add(ques);

            //var Titles = new Title_Ques
            //{
            //    Title = "Біологія",
            //    Quest1 = new List<Quest> {ques}
            //};

            //titles.Add(Titles);

            SaveChanges(); 
        }


    }
}
