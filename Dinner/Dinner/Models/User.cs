using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinner.Models {
    public interface IUserRepository {
        IEnumerable<User> Users { get; }
    }

    public class User {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
