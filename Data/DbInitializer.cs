using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            modelBuilder.Entity<UsersModel>().HasData(
                   new UsersModel() {
                    Id = 1,
                    UserName = "fszymanowski0",
                    FirstName = "Fawnia",
                    LastName = "Szymanowski",
                    Email = "fszymanowski0@com.com",
                    PhoneNumber = "0335487991",
                    Password = "74a14ea74c47ecdf30f940974dc9dc20"

                   },
                   new UsersModel() {
                       Id =2,
                       UserName = "apeacocke1",
                       FirstName = "Fawnia",
                       LastName = "Alexandros",
                       Email = "apeacocke1@google.ca",
                       PhoneNumber = "0354579415",
                       Password = "c52c635d98738ff357b13d9e4368aff6"
                   },
                   new UsersModel()
                   {
                       Id = 3,
                       UserName = "cpancoast2",
                       FirstName = "Cazzie",
                       LastName = "Pancoast",
                       Email = "cpancoast2@wsj.com",
                       PhoneNumber = "0354596415",
                       Password = "7f45928bce3ba52d77dee0cf1a8bbfdf"
                   }
    

            );
            modelBuilder.Entity<SkillModel>().HasData(
                    new SkillModel()
                    {
                        SkillId = 1,
                        SkillName = "JAVA",
                        SkillDescription = "Basic knowlegde about JAVA Language, How to build JAVA Program on console."
                    },
                    new SkillModel()
                    {
                        SkillId = 2,
                        SkillName = "SpringMVC",
                        SkillDescription = "Base on JAVA knowlegdge to build an Website service by SPRING Framework."
                    },
                    new SkillModel()
                    {
                        SkillId = 3,
                        SkillName = "C++",
                        SkillDescription = "Basic input/output on console."
                    },
                    new SkillModel()
                    {
                        SkillId = 4,
                        SkillName = "Algorithm",
                        SkillDescription = "Knowledge some of popular algorithm. Like: Sort, Search, Recursive,..."
                    }
                );
        }
    }
}
