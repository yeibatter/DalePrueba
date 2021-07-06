using Microsoft.EntityFrameworkCore;

namespace Dale.BackEnd.Context
{
    public partial class DaleContext : DbContext
    {
        /// <summary>
        /// Constructor con stringDBConn
        /// </summary>
        /// <param name="connectionString"></param>
        public DaleContext(string connectionString) : base(GetOptions(connectionString))
        {
        }

        /// <summary>
        /// New Options
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }
}
