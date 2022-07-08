using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.ViewModels;
namespace BookStore.Models.Models
{
    public class RoleModel
    {
        public RoleModel() { }
        public RoleModel(RoleModel role)
        {
            Id=role.Id;
            Name=role.Name;
        }
        public string Id { get; set; }
        public string Name { get; set; } = null;
    }
}
