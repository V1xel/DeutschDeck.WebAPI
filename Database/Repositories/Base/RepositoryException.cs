using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace GLA.Database.Repositories
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string source, string message) : base(message)
        {
            Source = source;
        }

        public static RepositoryException Map(DbUpdateException ex)
        {
            var dbExpection = ex.InnerException as DbException;
            if (dbExpection != null && dbExpection.SqlState == "23505")
            {
                return new RepositoryException("email", "already exist");
            }

            return new RepositoryException("internal", ex.Message);
        }
    }
}
