using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            string connectionString = "Server=localhost;Port=3306;Database=dbApiSdk31;Uid=root;Pwd=root;";
            var optionBuilder = new DbContextOptionsBuilder<MyContext>();
            optionBuilder.UseMySql(connectionString);
            return new MyContext(optionBuilder.Options);

        }


    }
}
