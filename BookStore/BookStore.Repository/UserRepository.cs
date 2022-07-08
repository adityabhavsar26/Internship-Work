using BookStore.Models.ViewModels;
using System;
using BookStore.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class UserRepository : BaseRepository
    {
        public User Login(LoginModel model)
        {
            User user = _context.Users.FirstOrDefault(c => c.Email.Equals(model.Email.ToLower()));
            if (user == null)
                return null;
            return _context.Users.FirstOrDefault(c => c.Email.Equals(model.Email) && c.Password.Equals(model.Password));
        }

        public User Register(RegisterModel model)
        {
            User user = new User()
            {
                Email = model.Email,
                Password = model.Password,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                roleId = model.roleId,
            };
            var entry = _context.Users.Add(user);
            _context.SaveChanges();
            return entry.Entity;
        }

        public List<User> GetUsers(int pageIndex, int pageSize, string keyword)
        {
            var users = _context.Users.AsQueryable();
            if(pageIndex > 0 )
            {
                if(string.IsNullOrEmpty(keyword) == false)
                {
                    users = users.Where(w => w.Firstname.ToLower().Contains(keyword) || w.Lastname.ToLower().Contains(keyword));
                }
                var userList = users.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return userList;
            }
            return null;
        }
        public ListResponse<Role> GetRoles(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Roles.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            int totalRecords = query.Count();
            List<Role> categories = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new ListResponse<Role>()
            {
                Results = categories,
                TotalRecords = totalRecords,
            };
        }
        public User GetUser(int id)
        {
            if(id > 0)
            {
                return _context.Users.Where(w => w.Id == id).FirstOrDefault();
            }
            return null ;
        }
        public bool UpdateUser(User model)
        {
            if(model.Id > 0)
            {
                _context.Update(model);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DeleteUser(User model)
        {
            if (model.Id > 0)
            {
                _context.Remove(model);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
