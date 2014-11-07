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

        public User DeleteUser(int userId) {
            User dbEntry = context.Users.Find(userId);
            if (dbEntry != null) {
                context.Users.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveUser(User user) {
            if (user.UserId == 0) {
                context.Users.Add(user);
            } else {
                User dbEntry = context.Users.Find(user.UserId);
                if (dbEntry != null) {
                    dbEntry.Name = user.Name;
                    dbEntry.Email = user.Email;
                }
            }
            context.SaveChanges();
        }
    }
}