using BLL.Request;
using DLL.Models;
using DLL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
   public interface IDepartmentService
    {
        Task<Department> Insert(DepartmentInsertRequestViewModel request);
        Task<List<Department>> GetAll();
        Task<Department> Read(string code);
        Task<Department> Update(string code, Department department);
        Task<Department> Delete(string code);

        Task<bool> IsNameExists(string name);
        Task<bool> IsCodeExists(string code);
    }

    public class DepartmentService : IDepartmentService
    {
        private  readonly DLL.Repository.IDepartmentRepository _departmentRepository;
        public DepartmentService(DLL.Repository.IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Department> Insert(DepartmentInsertRequestViewModel request)
        {
            var department = new Department();
            department.Code = request.Code;
            department.Name = request.Name;
           
            return await _departmentRepository.Insert(department);
        }

        public async Task<Department> Delete(string code)
        {
            return await _departmentRepository.Delete(code);
        }

        public async Task<List<Department>> GetAll()
        {
            return await _departmentRepository.GetAll();

        }

        public async Task<Department> Update(string code, Department department)
        {
            return await _departmentRepository.Update(code,department);

        }

        public async Task<Department> Read(string code)
        {
            return await _departmentRepository.Read(code);

        }

        public async Task<bool> IsNameExists(string name)
        {
            var department =  await _departmentRepository.FindByNameAsync(name);
            if (department == null)
            {
                return true;
            }
            return false;
        }

        public  async Task<bool> IsCodeExists(string code)
        {
            var department = await _departmentRepository.FindByCodeAsync(code);
            if (department == null)
            {
                return true;
            }
            return false;
        }
    }
}
