
namespace ATMLLD{
    public class User{
        public string name{get; private set;}
        public int userId { get; private set; }
        private Card card;

        public User(string name, int userId, Card card)
        {
            this.name = name;
            this.userId = userId;
            this.card = card;
        }

        public void DisplayUserInfo()
        {
            Console.WriteLine($"User: {name}, ID: {userId}");
        }

        
    }
}