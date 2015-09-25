using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using WebAPIToolkit.Models;

namespace WebAPIToolkit.Authentication
{
    public class UserStore : IUserStore<User, int>, IUserPasswordStore<User, int>//, IUserLoginStore<User>
    {


        public UserStore()
        {
            
        }

        public void Dispose()
        {

        }

        public async Task CreateAsync(User user)
        {
            using (var db = new ModelContext())
            {
               // user.Id = Guid.NewGuid().ToString();
                db.Users.Add(user);
                await db.SaveChangesAsync();
            }



            //return Task.Run(async () =>
            //{
            //    // Validate the model
            //    var errors = user.Validate(CrudEnum.Create, true);
            //    if (errors.HasErrors())
            //    {
            //        throw new ValidationException(errors);
            //    }

            //    if (user.Profile != null && !string.IsNullOrEmpty(user.Profile.Email))
            //    {
            //        var alreadyExist = await FindByEmailAsync(user.Profile.Email);
            //        if (alreadyExist != null)
            //        {
            //            throw new ValidationException("email", "Un compte avec cet email existe déjà.");
            //        }
            //    }

            //    // Set Ids
            //    user.SetId();

            //    // Set Timestamp
            //    user.Timestamp = DateTime.Now.ToUniversalTime();

            //    // Add in Context
            //    await _collection.InsertOneAsync(user);
            //});
        }

        public async Task UpdateAsync(User user)
        {
            using (var db = new ModelContext())
            {
                db.Users.Attach(user);
                db.Entry(user).State = EntityState.Modified;

                //var original = await db.Users.FindAsync(user.Id);
                //if (original == null)
                //{
                //    throw new Exception($"User {user.Id} doest not exist");
                //}
                //db.Entry(original).CurrentValues.SetValues(user);

                await db.SaveChangesAsync();
            }


            //return Task.Run(async () =>
            //{
            //    User oldModel;
            //    //UoWRepositories.ExecuteThreadSafeWriteMethod(() =>
            //    //{

            //    var filter = Builders<User>.Filter.Eq(e => e.Id, user.Id);

            //    oldModel = await _collection.Find(filter).FirstOrDefaultAsync();

            //    if (oldModel == null || oldModel.Timestamp != user.Timestamp)
            //    {
            //        throw new ConcurrencyException(UserMessages.ConcurrencyExceptionUserMessage);
            //    }

            //    // Set Timestamp
            //    user.Timestamp = DateTime.Now.ToUniversalTime();

            //    var result = await _collection.ReplaceOneAsync(filter, user);

            //    if (!result.IsAcknowledged)
            //    {
            //        throw new DatabaseException(string.Format("Unable to update user {0} in collection 'User'.", user.Id));
            //    }
            //});



        }

        public async Task DeleteAsync(User user)
        {
            using (var db = new ModelContext())
            {
                db.Users.Attach(user);
                db.Entry(user).State = EntityState.Deleted;

                await db.SaveChangesAsync();
            }

            //return Task.Run(async () =>
            //{
            //    var filter = Builders<User>.Filter.Eq(e => e.Id, user.Id);
            //    var update = Builders<User>.Update
            //        .Set(m => m.IsDeleted, true).CurrentDate(m => m.Timestamp);

            //    var result = await _collection.UpdateOneAsync(filter, update);

            //    if (!result.IsAcknowledged)
            //    {
            //        throw new DatabaseException(
            //            string.Format(
            //                "Unable to delete user {0}.",
            //                user.Id));
            //    }
            //});
        }

        public async Task<User> FindByIdAsync(int userId)
        {
            using (var db = new ModelContext())
            {
                return await db.Users.FindAsync(userId);
            }

        }

        public async Task<User> FindByNameAsync(string userName)
        {
            using (var db = new ModelContext())
            {
                var users = from u in db.Users
                            where u.UserName == userName
                            select u;

                return await users.FirstOrDefaultAsync();
            }
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            var hasPassword = String.IsNullOrEmpty(user.PasswordHash);
            return Task.FromResult(hasPassword);
        }

        //public Task SetEmailAsync(User user, string email)
        //{
        //    user.Profile.Email = email;
        //    return Task.FromResult(0);
        //}

        //public Task<string> GetEmailAsync(User user)
        //{
        //    return Task.FromResult(user.Profile.Email);
        //}

        //public Task<bool> GetEmailConfirmedAsync(User user)
        //{
        //    return Task.FromResult(user.Profile.EmailConfirmed);
        //}

        //public Task SetEmailConfirmedAsync(User user, bool confirmed)
        //{
        //    user.Profile.EmailConfirmed = confirmed;
        //    return Task.FromResult(0);
        //}

        //public Task<User> FindByEmailAsync(string email)
        //{
        //    var filter = Builders<User>.Filter.Eq(e => e.Profile.Email, email);

        //    return _collection.Find(filter).FirstOrDefaultAsync();
        //}

        //public Task AddLoginAsync(User user, UserLoginInfo login)
        //{
        //    user.SocialLogin = login;
        //    return Task.FromResult(0);
        //}

        //public Task RemoveLoginAsync(User user, UserLoginInfo login)
        //{
        //    user.SocialLogin = null;
        //    return Task.FromResult(0);
        //}

        //public Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        //{
        //    IList<UserLoginInfo> list = new List<UserLoginInfo>();
        //    if (user.SocialLogin == null)
        //        return Task.FromResult(list);

        //    list.Add(user.SocialLogin);
        //    return Task.FromResult(list);
        //}

        //public Task<User> FindAsync(UserLoginInfo login)
        //{
        //    var filter = Builders<User>.Filter.Where(e => e.SocialLogin != null && e.SocialLogin.LoginProvider == login.LoginProvider && e.SocialLogin.ProviderKey == login.ProviderKey);

        //    return _collection.Find(filter).FirstOrDefaultAsync();
        //}
    }
}
