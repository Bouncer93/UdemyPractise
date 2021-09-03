using BLL.Request;
using DLL.Models;
using DLL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utilities.Exceptions;

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
        Task<bool> IsIdExist(int id);
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
           
            _unitOfWork = unitOfWork;
        }

        public async Task<Department> Insert(DepartmentInsertRequestViewModel request)
        {
            var department = new Department();
            department.Code = request.Code;
            department.Name = request.Name;
           
             await _unitOfWork.DepartmentRepository.CreateAsync(department);
            if(await _unitOfWork.SaveCompletedAsync())
            {
                return department;
            }
            throw new ApplicationValidationException(message: "Problem occured while inserting department");
        }

        public async Task<Department> Delete(string code)
        {
            var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x=>x.Code==code);

            if (department == null)
            {
                throw new ApplicationValidationException($"{code} for the department does not exist");
            }
            _unitOfWork.DepartmentRepository.Delete(department);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return department;
            }
            throw new ApplicationValidationException(message: "Problem occured while deleating a  department");

        }

        public async Task<List<Department>> GetAll()
        {
            return await _unitOfWork.DepartmentRepository.GetList();

        }

        public async Task<Department> Update(string code, Department department)
        {

           

            var Adepartment = await _unitOfWork.DepartmentRepository.FindSingleAsync(x=>x.Code==code);
            if (Adepartment == null)
            {
                throw new ApplicationValidationException(message: "Department not found");
            }

           

            if(!string.IsNullOrWhiteSpace(department.Code))
            {
                var exisitng = await _unitOfWork.DepartmentRepository.FindSingleAsync(x=>x.Code==department.Code);
                if(exisitng!=null)
                {
                    throw new ApplicationValidationException("You are updating a department which already exists");
                }

                Adepartment.Code = department.Code;

            }



            if (!string.IsNullOrWhiteSpace(department.Name))
            {
                var exisitng = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.Name == department.Name);
                if (exisitng != null)
                {
                    throw new ApplicationValidationException("You are updating a department which already exists");
                }

                Adepartment.Name = department.Name;

            }

            _unitOfWork.DepartmentRepository.Update(department);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return department;
            }
            throw new ApplicationValidationException(message: "Problem occured while updating a  department");

        }

        public async Task<Department> Read(string code)
        {
            var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x=>x.Code==code);
            if (department == null)
            {
                throw new ApplicationValidationException("The department could not be found");
            }
            else
                return department;

        }

        public async Task<bool> IsNameExists(string name)
        {
            var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x=>x.Name==name);
            if (department == null)
            {
                return true;
            }
            return false;
        }

        public  async Task<bool> IsCodeExists(string code)
        {
            var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.Code == code);
            if (department == null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsIdExist(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.FindSingleAsync(x => x.DepartmentId == id);
            if (department == null)
            {
                return true;
            }
            return false;

        }
    }
}
