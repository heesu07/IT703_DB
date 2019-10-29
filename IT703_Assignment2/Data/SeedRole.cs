
using IT703_Assignment2.Areas.Identity.Data;
using IT703_Assignment2.Data;
using IT703_Assignment2.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Data
{
    public class SeedRole
    {

        public static async Task Initialize(ApplicationDbContext context,
                               UserManager<AccountUser> userManager,
                               RoleManager<AccountRole> roleManager)
        {

            context.Database.EnsureCreated();


            String adminId1 = "";
            // String adminId2 = "";

            string role1 = "Manager";
            string desc1 = "This is the Manager role";

            string role2 = "Reception";
            string desc2 = "This is the Reception role";

            string role3 = "Housekeeper";
            string desc3 = "This is the Housekeeper role";

            string role4 = "Staff";
            string desc4 = "This is the staff role";




            string password = "Password1!";

            if (await roleManager.FindByNameAsync(role1) == null)
            {
                await roleManager.CreateAsync(new AccountRole(role1, desc1));
            }
            if (await roleManager.FindByNameAsync(role2) == null)
            {
                await roleManager.CreateAsync(new AccountRole(role2, desc2));
            }
            if (await roleManager.FindByNameAsync(role3) == null)
            {
                await roleManager.CreateAsync(new AccountRole(role3, desc3));
            }
            if (await roleManager.FindByNameAsync(role4) == null)
            {
                await roleManager.CreateAsync(new AccountRole(role4, desc4));
            }


            if (await userManager.FindByNameAsync("manager@manager.com") == null)
            {
                var user = new AccountUser
                {
                    UserName = "manager@manager.com",
                    Email = "manager@manager.com",
                    PhoneNumber = "6902341234",
                    //Group = "Admin",
                    CreationDate = DateTime.Now


                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }
               // adminId1 = user.Id;
            }

            if (await userManager.FindByNameAsync("Reception@Reception.com") == null)
            {
                var user = new AccountUser
                {
                    UserName = "Reception@Reception.com",
                    Email = "Reception@Reception.com",
                    PhoneNumber = "88322392",
                    //Group = "Admin",
                    CreationDate = DateTime.Now


                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role2);
                }
                // adminId1 = user.Id;
            }
            if (await userManager.FindByNameAsync("Housekeeper@Housekeeper.com") == null)
            {
                var user = new AccountUser
                {
                    UserName = "Housekeeper@Housekeeper.com",
                    Email = "Housekeeper@Housekeeper.com",
                    PhoneNumber = "123123123",
                    //Group = "Admin",
                    CreationDate = DateTime.Now


                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role3);
                }
                // adminId1 = user.Id;
            }

            if (context.Rooms.Count() == 0)
            {

              var rooms = new Room[]
              {
      
              new Room {RoomType =roomType.Single, RoomNum="R01",Status = Status.VacantClean, Description="Single" },
              new Room {RoomType =roomType.Single, RoomNum="R02",Status = Status.VacantClean, Description="Single" },
              new Room {RoomType =roomType.Single, RoomNum="R03",Status = Status.VacantClean, Description="Single" },

               new Room {RoomType =roomType.TwoBedRooms, RoomNum="R04",Status = Status.VacantClean, Description="TwoBedRooms" },
              new Room {RoomType =roomType.TwoBedRooms, RoomNum="R05",Status = Status.VacantClean, Description="TwoBedRooms" },
              new Room {RoomType =roomType.TwoBedRooms, RoomNum="R06",Status = Status.VacantClean, Description="TwoBedRooms" },

               new Room {RoomType =roomType.Superior, RoomNum="R07",Status = Status.VacantClean, Description="Superior" },
              new Room {RoomType =roomType.Superior, RoomNum="R08",Status = Status.VacantClean, Description="Superior" },
              new Room {RoomType =roomType.Superior, RoomNum="R09",Status = Status.VacantClean, Description="Superior" },

               };

                foreach (Room r in rooms)
                {
                    context.Rooms.Add(r);
                }
            }
            context.SaveChanges();


        }
    }
}
