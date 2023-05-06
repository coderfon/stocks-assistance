using Microsoft.EntityFrameworkCore;
using StocksAssistance.Common.Enums;
using StocksAssistance.EF.Context;
using StocksAssistance.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAssistance.EF.Repositories
{

    public class CompanyRepository : IDataRepository<Company>
    {
        readonly StocksAssistanceDbContext context;

        public CompanyRepository(StocksAssistanceDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Company> GetAll()
        {
            return context.Companies.AsQueryable();
        }

        public async Task<Company> Get(int id)
        {
            return await context.Companies.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Company> Get(string symbol, CompanyAttributeType symbolProvider)
        {
            return await context.Companies
                .FirstOrDefaultAsync(c => c.Attributes.Any(a => a.Type == symbolProvider && a.Value == symbol));
        }

        public Company GetSync(string symbol, CompanyAttributeType symbolProvider)
        {
            return context.Companies
                .FirstOrDefault(c => c.Attributes.Any(a => a.Type == symbolProvider && a.Value == symbol));
        }

        public async Task Add(Company entity, bool save = true)
        {
            context.Companies.Add(entity);
            if (save) await SaveChanges();
        }

        public async Task Update(Company restaurant, bool save = true)
        {
            context.Companies.Update(restaurant);
            if (save) await SaveChanges();
        }

        public async Task Delete(Company restaurant, bool save = true)
        {
            context.Companies.Remove(restaurant);
            if (save) await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
