namespace LRU.CacheWithLRU{
    public class Program{
        static void Main(string[] args)
        {
            LRUCache cache = new LRUCache(5);
            
            cache.Add(1,1);
            cache.Add(2,2);
            cache.Add(3,3);
            cache.Add(4,4);
            cache.Add(5,5);

            cache.Display();
            Console.WriteLine(new String('=',50));

            cache.Get(1);
            cache.Get(3);
            cache.Get(1);
            cache.Get(2);

            cache.Display();
            Console.WriteLine(new String('=',50));

            cache.Add(6,6);
            Console.WriteLine(new String('=',50));

            cache.Display();



        }
    }
}