using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dinner.Models {
    public class EFUserRepository: IUserRepository {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<User> Users {
            get { return context.Users; }
        }
    }
}