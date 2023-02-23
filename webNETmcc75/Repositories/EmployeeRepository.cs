using Microsoft.EntityFrameworkCore;
using webNETmcc75.Contexts;
using webNETmcc75.Models;
using webNETmcc75.Repositories.Interface;
using webNETmcc75.ViewModels;

namespace webNETmcc75.Repositories;

public class EmployeeRepository : IRepository<string, Employee>
{
    private readonly MyContext context;
    public EmployeeRepository(MyContext context)
    {
        this.context = context;
    }


    public int Delete(string key)
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

    public List<Employee> GetAll()
    {
        return context.Employees.ToList() ?? null;
    }

    public Employee GetById(string key)
    {
        return context.Employees.Find(key) ?? null;
    }

    public int Insert(Employee entity)
    {
        int result = 0;
        context.Add(entity);
        result = context.SaveChanges();
        return result;
    }

    public int Update(Employee entity)
    {
        int result = 0;
        context.Entry(entity).State = EntityState.Modified;
        result = context.SaveChanges();
        return result;
    }

    public List<EmployeeVM>GetAllEmployees()
    {
        var result = (from e in GetAll()
                      select new EmployeeVM
                      {
                          Nik = e.Nik,
                          FirstName = e.FirstName,
                          LastName = e.LastName,
                          BirthDate = e.BirthDate,
                          Gender = (ViewModels.GenderEnum)e.Gender,
                          HireingDate = e.HireingDate,
                          Email = e.Email,
                          PhoneNumber = e.PhoneNumber
                      }).ToList();
        return result;
    }

    public EmployeeVM GetByIdEmployee(string key)
    {
        var Employee = GetById(key);
        var result = new EmployeeVM
        {
            Nik = Employee.Nik,
            FirstName = Employee.FirstName,
            LastName = Employee.LastName,
            BirthDate = Employee.BirthDate,
            Gender = (ViewModels.GenderEnum)Employee.Gender,
            HireingDate = Employee.HireingDate,
            Email = Employee.Email,
            PhoneNumber = Employee.PhoneNumber,
        };
        return result;
    }
}


