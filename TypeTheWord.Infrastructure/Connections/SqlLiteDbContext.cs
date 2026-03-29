using Domain.Entities.Models.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using MongoDB.Driver.Core.Configuration;

namespace TypeTheWord.Infrastructure.Connections
{
    public class SqlLiteDbContext : DbContext
    {
        protected readonly string ConnString;
        public SqlLiteDbContext()
        {
            var libPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var folderPath = Path.Combine(libPath, "TypeTheWord", "Data");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            
            var dbPath = Path.Combine(folderPath, "WordSets.db");
            ConnString = $"Data Source={dbPath}";

            System.Diagnostics.Debug.WriteLine($"DATABASE LOCATION: {dbPath}");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnString);
        }

        public DbSet<WordSet> WordSets { get; set; }
    }
}
