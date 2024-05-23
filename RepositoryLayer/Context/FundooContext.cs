using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RepositoryLayer.Context
{
    //create a table product(id, pname, brand)
   // add and fetch data from the table
    public class FundooContext :DbContext
    {
        public FundooContext(DbContextOptions options) :base(options) 
        { 
        
        }
        public DbSet<UserEntity> UserTable { get; set; }
        public DbSet<Product> Products {  get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<labelEntity> Labels { get; set; }
        public DbSet<Collaborator> Collaborator {  get; set; }
    }
   
}
