using webNETmcc75.Contexts;
using webNETmcc75.Models;
using webNETmcc75.Repositories.Interface;
using webNETmcc75.ViewModels;

namespace webNETmcc75.Repositories
{
    public class AccountRepository : IRepository<int, Account>
    {
        private readonly MyContext context;
        private readonly EmployeeRepository employeeRepository;
        private readonly UniversityRepository universityRepository;
        public AccountRepository(MyContext context, EmployeeRepository employeeRepository, UniversityRepository universityRepository) 
        {
            this.context = context;
            this.employeeRepository = employeeRepository;
            this.universityRepository= universityRepository;
        }
        public int Delete(int key)
        {
            throw new NotImplementedException();
        }

        public List<Account> GetAll()
        {
            return context.Accounts.ToList() ?? null;
        }

        public Account GetById(int key)
        {
            throw new NotImplementedException();
        }

        public int Insert(Account entity)
        {
            throw new NotImplementedException();
        }

        public int Update(Account entity)
        {
            throw new NotImplementedException();
        }

        public int Register(RegisterVM registerVM)
        {
            int result = 0;
            University university = new University
            {
                Name = registerVM.UniversityName
            };
            if (context.Universities.Any(u => u.Name == university.Name))
            {
                university.Id = context.Universities.FirstOrDefault(u => u.Name == university.Name).Id;
            }
            else
            {
                context.Universities.Add(university);
                context.SaveChanges();
            }

            Education education = new Education
            {
                Major = registerVM.Major,
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                UniversityId = university.Id
            };
            context.Educations.Add(education);
            context.SaveChanges();

            Employee employee = new Employee
            {
                Nik = registerVM.Nik,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                BirthDate = registerVM.BirthDate,
                Gender = (Models.GenderEnum)registerVM.Gender,
                HireingDate = registerVM.HireingDate,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber,
            };
            context.Employees.Add(employee);
            context.SaveChanges();

            Account account = new Account
            {
                EmployeeNik = registerVM.Nik,
                Password = registerVM.Password
            };
            context.Accounts.Add(account);
            context.SaveChanges();

            AccountRole accountRole = new AccountRole
            {
                AccountNik = registerVM.Nik,
                RoleId = 2
            };

            context.AccountRoles.Add(accountRole);
            context.SaveChanges();

            Profiling profiling = new Profiling
            {
                EmployeeId = registerVM.Nik,
                EducationId = education.Id
            };
            context.Profilings.Add(profiling);
            context.SaveChanges();
            return result;
        }

        public bool Login(LoginVM loginVM)
        {
            var result = context.Employees.Join(
                context.Accounts,
                e => e.Nik,
                a => a.EmployeeNik,
           (e, a) => new LoginVM
           {
               Email = e.Email,
               Password = a.Password,
           });
            return result.Any(e => e.Email == loginVM.Email && e.Password == loginVM.Password);
        }
    }
}
