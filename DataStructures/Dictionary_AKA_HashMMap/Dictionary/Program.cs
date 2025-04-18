namespace Dictionary_AKA_HashMMap.Dictionary{
    public class Program{
         static void Main(string[] args)
        {
            HashMap<string, string> hashmap1 = new HashMap<string, string> (5);
            hashmap1.Get("Apple");

            hashmap1.Put("Apple","Fruit - 1");
            hashmap1.Put("Banana","Fruit - 2");
            hashmap1.Put("Orange","Fruit - 3");
            hashmap1.Put("Pomegranate","Fruit - 4");
            hashmap1.Put("Guava","Fruit - 5");
            hashmap1.Put("Mango","Fruit - 6");
            hashmap1.Put("Dragon Fruit","Fruit - 7");

            hashmap1.Get("Apple");
            hashmap1.Get("Orange");

            hashmap1.Remove("Dragon Fruit");
            hashmap1.Remove("Orange");
            hashmap1.Get("Orange");

            Console.WriteLine("Hashmap size is "+hashmap1.Count);
            hashmap1.DisplayAll();
            
            Console.WriteLine(new String('=',50));
            
            HashMap<int, string> hashmap2 = new HashMap<int, string> (4);
            hashmap2.Get(1);

            hashmap2.Put(1,"Fruit - 1");
            hashmap2.Put(2,"Fruit - 2");

            hashmap2.Put(3,"Fruit - 3");
            hashmap2.Get(3);
            hashmap2.Put(3,"Fruit - 4"); //updating
            hashmap2.Get(3);

            hashmap2.Put(4,"Fruit - 5");
            hashmap2.Put(5,"Fruit - 6");

            hashmap2.Get(5);

            hashmap2.Remove(2);
            hashmap2.Remove(1);
            hashmap2.Get(4);

            Console.WriteLine("Hashmap size is "+hashmap2.Count);
            hashmap2.DisplayAll();


        }
    }
}