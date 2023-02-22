using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using webNETmcc75.Contexts;
using webNETmcc75.Models;
using webNETmcc75.Repositories.Interface;
using webNETmcc75.ViewModels;

namespace webNETmcc75.Repositories
{
    public class EducationRepository : IRepository<int, Education>
    {
        private readonly MyContext context;
        private readonly UniversityRepository universityRepository;
        public EducationRepository(MyContext context, UniversityRepository universityRepository) 
        { 
            this.context = context;
            this.universityRepository = universityRepository;
        }
        public int Delete(int key)
        {
            int result = 0;
            var education = GetById(key);
            if (education == null)
            {
                return result;
            }
            context.Remove(education);
            result = context.SaveChanges();

            return result;
        }

        public List<Education> GetAll()
        {
            return context.Educations.ToList()?? null;
        }

        public Education GetById(int key)
        {
            return context.Educations.Find(key) ?? null;
        }

        public int Insert(Education entity)
        {
            int result = 0;
            context.Add(entity);
            result = context.SaveChanges();

            return result;
        }

        public int Update(Education entity)
        {
            int result = 0;
            context.Entry(entity).State = EntityState.Modified;
            result = context.SaveChanges();
            return result;
        }
        public List<EducationUniversityVM> GetAllEducationUniversities()
        {
            var result = (from e in GetAll()
                          join u in universityRepository.GetAll()
                          on e.UniversityId equals u.Id
                          select new EducationUniversityVM
                          {
                              Id = e.Id,
                              Degree = e.Degree,
                              GPA = e.GPA,
                              Major = e.Major,
                              UniversityName = u.Name
                          }).ToList();
            return result;
        }

        public EducationUniversityVM GetByIdlEducationUniversities(int key)
        {
            var education = GetById(key);

            var result = new EducationUniversityVM
            {
                Id = education.Id,
                Degree = education.Degree,
                GPA = education.GPA,
                Major = education.Major,
                UniversityName = context.Universities.Find(education.UniversityId).Name
            };  
            return result;
        }

    
    }
}
