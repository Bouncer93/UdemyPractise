
using DLL.DBContext;
using DLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace DLL.Repository
{
    public interface IDepartmentRepository
    {
        Task<Department> Insert(Department department);
        Task<List<Department>> GetAll();
        Task<Department> Read(string code);
        Task<Department> Update(string code, Department department);
        Task<Department> Delete(string code);
        Task<Department> FindByNameAsync( string name);
        Task<Department> FindByCodeAsync(string code );
    }

    public class DepartmentRepository : IDepartmentRepository
    {

        public DepartmentRepository(ApplicationDbContext context )
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public async Task<Department> Insert(Department department)
        {

           await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> Delete(string code)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d=>d.Code==code);
             _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<List<Department>> GetAll()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> Update(string code , Department department)
        {
            var d = await _context.Departments.FirstOrDefaultAsync(d => d.Code == code);
            d.Name = department.Name;
            _context.Departments.Update(d);
            await _context.SaveChangesAsync();
            return d;
        }

        public async Task<Department> Read(string code)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Code == code);
           
            return department;
        }

        public async Task<Department> FindByNameAsync(string name)
        {
            return await _context.Departments.FirstOrDefaultAsync(d => d.Name == name);
        }

        public async Task<Department> FindByCodeAsync(string code )
        {
            return await _context.Departments.FirstOrDefaultAsync(d => d.Code == code);
        }
    }
}
