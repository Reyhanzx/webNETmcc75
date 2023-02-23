using Microsoft.EntityFrameworkCore;
using webNETmcc75.Contexts;
using webNETmcc75.Models;
using webNETmcc75.Repositories.Interface;

namespace webNETmcc75.Repositories
{
    public class RoleRepository : IRepository<int, Role>
    {
        private readonly MyContext context;
        public RoleRepository(MyContext context) 
        { 
            this.context = context;
        }
        public int Delete(int key)
        {
            int result = 0;
            var university = GetById(key);
            if (university == null)
            {
                return result;
            }
            context.Remove(university);
            result = context.SaveChanges();

            return result;
        }

        public List<Role> GetAll()
        {
            return context.Roles.ToList() ?? null;
        }

        public Role GetById(int key)
        {
            return context.Roles.Find(key) ?? null;
        }

        public int Insert(Role entity)
        {
            int result = 0;
            context.Add(entity);
            result = context.SaveChanges();
            return result;
        }

        public int Update(Role entity)
        {
            int result = 0;
            context.Entry(entity).State = EntityState.Modified;
            result = context.SaveChanges();
            return result;
        }
    }
}
