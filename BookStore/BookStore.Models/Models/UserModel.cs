using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public class UserModel
    {
        public UserModel() { }
        public UserModel(UserModel user)
        {
            Id= user.Id;
            Firstname= user.Firstname;
            Lastname= user.Lastname;
            Email= user.Email;
            roleId= user.roleId;
        }
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int roleId { get; set; }

    }
}
