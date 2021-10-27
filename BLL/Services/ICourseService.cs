using BLL.Request;
using DLL.Models;
using DLL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Minio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Exceptions;

namespace BLL.Services
{
   public interface ICourseService
    {
        Task<Course> Insert(CourseInsertRequestViewModel request);
        Task<List<Course>> GetAll();
        Task<Course> Read(string code);
        Task<Course> Update(string code, Course course);
        Task<Course> Delete(string code);

        Task<bool> IsNameExists(string name);
        Task<bool> IsCodeExists(string code);
        Task<bool> IsIdExist(int id);
    }

    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly string _acesskey;
        private readonly string _secretkey;
        private readonly string _bucketname;
        private readonly string _imageserver;

        public CourseService(IUnitOfWork unitOfWork , IConfiguration configuration)
        {
           
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _acesskey = configuration.GetValue<string>("MediaServer:AccessKey");
            _secretkey = configuration.GetValue<string>("MediaServer:SecretKey");
            _bucketname = configuration.GetValue<string>("MediaServer:BucketName");
            _imageserver = configuration.GetValue<string>("MediaServer:ImageServer");
        }

        public async Task<Course> Insert(CourseInsertRequestViewModel request)
        {
            var course = new Course();
            course.Code = request.Code;
            course.Name = request.Name;
            course.Credit = request.Credit;
            course.ImageUrl = await ForImageUpload(request.CourseImage);
           
             await _unitOfWork.CourseRepository.CreateAsync(course);
            if(await _unitOfWork.SaveCompletedAsync())
            {
                course.ImageUrl = _configuration.GetValue<string>("MediaServer:ImageAccessUrl") + course.ImageUrl;
                return course;
            }
            throw new ApplicationValidationException(message: "Problem occured while inserting course");
        }

        private async Task<string> ForImageUpload(IFormFile file)
        {
            var client = new MinioClient(_imageserver, _acesskey, _secretkey, _bucketname);
            await SetupBucket(client, _bucketname);
            var extenstion = Path.GetExtension(file.FileName) ?? ".png";
            var filename = Guid.NewGuid().ToString() + extenstion;
            var imagePath = _configuration.GetValue<string>("MediaServer:LocalImageStorage");
            var path = Path.Combine(Directory.GetCurrentDirectory(), imagePath, filename);
            await using var bits = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(bits);
            bits.Close();
            await client.PutObjectAsync(bucketName: _bucketname, objectName: filename, path, "image/jpeg");
            File.Delete(path);
            return filename;
        }
        private static async Task SetupBucket(MinioClient client , string bucketname)
        {
            var found = await client.BucketExistsAsync(bucketname);
            if (!found)
            {
                await client.MakeBucketAsync(bucketname);
            }
        }

        public async Task<Course> Delete(string code)
        {
            var course = await _unitOfWork.CourseRepository.FindSingleAsync(x=>x.Code==code);

            if (course == null)
            {
                throw new ApplicationValidationException($"{code} for the course does not exist");
            }
            _unitOfWork.CourseRepository.Delete(course);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return course;
            }
            throw new ApplicationValidationException(message: "Problem occured while deleating a  course");

        }

        public async Task<List<Course>> GetAll()
        {
            var latest=  await _unitOfWork.CourseRepository.GetList();
            latest.Select(c =>
            {
                c.ImageUrl = _configuration.GetValue<string>("MediaServer:ImageAccessUrl") + c.ImageUrl;
                return c;
            }).ToList();

            return latest;

        }

        public async Task<Course> Update(string code, Course course)
        {

           

            var Acourse = await _unitOfWork.CourseRepository.FindSingleAsync(x=>x.Code==code);
            if (Acourse == null)
            {
                throw new ApplicationValidationException(message: "Course  not found");
            }

           

            if(!string.IsNullOrWhiteSpace(course.Code))
            {
                var exisitng = await _unitOfWork.CourseRepository.FindSingleAsync(x=>x.Code== course.Code);
                if(exisitng!=null)
                {
                    throw new ApplicationValidationException("You are updating a course which already exists");
                }

                Acourse.Code = course.Code;

            }



            if (!string.IsNullOrWhiteSpace(course.Name))
            {
                var exisitng = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.Name == course.Name);
                if (exisitng != null)
                {
                    throw new ApplicationValidationException("You are updating a course which already exists");
                }

                Acourse.Name = course.Name;

            }

            _unitOfWork.CourseRepository.Update(course);
            if (await _unitOfWork.SaveCompletedAsync())
            {
                return course;
            }
            throw new ApplicationValidationException(message: "Problem occured while updating a  course");

        }

        public async Task<Course> Read(string code)
        {
            var course = await _unitOfWork.CourseRepository.FindSingleAsync(x=>x.Code==code);
        
            if (course == null)
            {
                throw new ApplicationValidationException("The course could not be found");
            }
            else
            {
                course.ImageUrl = _configuration.GetValue<string>("MediaServer:ImageAccessUrl") + course.ImageUrl;
                return course;
            }
            

        }

        public async Task<bool> IsNameExists(string name)
        {
            var course = await _unitOfWork.CourseRepository.FindSingleAsync(x=>x.Name==name);
            if (course == null)
            {
                return true;
            }
            return false;
        }

        public  async Task<bool> IsCodeExists(string code)
        {
            var course = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.Code == code);
            if (course == null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsIdExist(int id)
        {
            var course = await _unitOfWork.CourseRepository.FindSingleAsync(x => x.CourseId == id);
            if (course == null)
            {
                return true;
            }
            return false;

        }
    }
}
