using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using SGS.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class SGSDBContextFactory : IDesignTimeDbContextFactory<SGSDb>
    {
    SGSDb IDesignTimeDbContextFactory<SGSDb>.CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SGSDb>();
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=SistemaSolicitudesDB;Trusted_Connection=True;TrustServerCertificate=True;");

        return new SGSDb(optionsBuilder.Options);
    }
}

