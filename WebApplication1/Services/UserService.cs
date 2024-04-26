using MongoDB.Bson;
using MongoDB.Driver;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IMongoDatabase database)
        {
            _users = database.GetCollection<User>("Users");
        }

        public List<User> GetUsers()
        {
            return _users.Find(user => true).ToList();
        }

        public User GetUser(string id)
        {
            return _users.Find(user => user.Id == id).First();
        }

        public List<User> CreateUser(User user)
        {
            user.Id = ObjectId.GenerateNewId().ToString();
            _users.InsertOne(user);
            return GetUsers();
        }

        public List<User> UpdateUser(string id, User user)
        {
            _users.ReplaceOne(x=>x.Id==id,user);

            return GetUsers();
        } 

        public void DeleteUser(string id)
        {
            _users.DeleteOne(user => user.Id == id);
        }
    }
}
