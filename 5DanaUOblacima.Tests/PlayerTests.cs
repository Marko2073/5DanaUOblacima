using _5DanaUOblacima.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5DanaUOblacima.Tests
{
    public class PlayerTests
    {
        private ApplicationDBContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;
            return new ApplicationDBContext(options);
        }

        [Fact]
        public void CanCreatePlayer()
        {
            var context = GetDbContext();
            var player = new Player { Nickname = "TestPlayer" };

            context.Players.Add(player);
            context.SaveChanges();

            var players = context.Players.ToList();
            Assert.Single(players); 
            Assert.Equal("TestPlayer", players.First().Nickname);
        }

        [Fact]
        public void CannotCreateDuplicatePlayer()
        {
            var context = GetDbContext();

            var player1 = new Player { Nickname = "DuplicatePlayer" };
            var player2 = new Player { Nickname = "DuplicatePlayer" };

           
            context.Players.Add(player1);
            context.SaveChanges();

           
            var exception = Assert.Throws<DbUpdateException>(() =>
            {
                context.Players.Add(player2);
                context.SaveChanges();
            });

            
            Assert.Contains("duplicate", exception.Message.ToLower());
        }



    }

}
