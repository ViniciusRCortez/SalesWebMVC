using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Migrations;
using SalesWebMVC.Models;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMVCContext _context;

        public SalesRecordService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? min, DateTime? max)
        {
            var result = from obj in _context.SalesRecords select obj; //Seleciona tds os SalesRecord
            if (min.HasValue)
            {
                result = result.Where(x => x.Date >= min.Value);
            }
            if (max.HasValue)
            {
                result = result.Where(x => x.Date <= max.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
            .ToListAsync();
        }

        public async Task<Dictionary<Department, List<SalesRecord>>> FindByDateGroupingAsync(DateTime? min, DateTime? max)
        {
            var salesRecords = await FindByDateAsync(min, max);
            var departments = _context.Department.ToList();
            var grouping = new Dictionary<Department, List<SalesRecord>>();

            departments.ForEach(d => {
                var sales = salesRecords.Where(sr => sr.Seller.Department == d).ToList();
                grouping.Add(d, sales);
            });

            return grouping;
        }
    }
}
