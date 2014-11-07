using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dinner.Models {
    public interface IUserRepository {
        IEnumerable<User> Users { get; }
        User DeleteUser(int userId);
    }

    public class User {
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
