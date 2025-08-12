using Microsoft.EntityFrameworkCore;
using URLShortener.Models;
using URLShortener.Constants;

namespace URLShortener.Data
{
	public class URLDbContext : DbContext
	{
		public URLDbContext(DbContextOptions<URLDbContext> options)
		   : base(options)
		{
		}

		public DbSet<URL> URLs { get; set; }
	}
}
