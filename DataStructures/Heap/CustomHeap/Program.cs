
namespace CustomHeap{
    public class Program{
        static void Main(string[] args)
        {
            CustomHeap<int> minHeap = new CustomHeap<int>();
            Console.WriteLine("Adding to MinHeap...");
            Console.WriteLine(new String('=',50));

            minHeap.Add(5); 
            minHeap.Display();

            minHeap.Add(3); 
            minHeap.Display();

            minHeap.Add(4); 
            minHeap.Display();

            minHeap.Add(1); 
            minHeap.Display();

            minHeap.Add(2); 
            minHeap.Display();

            Console.WriteLine("Removing from MinHeap...");
            Console.WriteLine(new String('=',50));

            minHeap.Remove();
            minHeap.Display();

            minHeap.Remove();
            minHeap.Display();

            minHeap.Remove();
            minHeap.Display();

            minHeap.Remove();
            minHeap.Display();

            minHeap.Remove();
            minHeap.Display();

            CustomHeap<int> maxHeap = new CustomHeap<int>(false);
            maxHeap.Add(5); 
            maxHeap.Add(3); 
            maxHeap.Add(4); 
            maxHeap.Add(1); 
            maxHeap.Add(2); 

            maxHeap.Display();


            //Student
            Console.WriteLine(new String('=',50));

            CustomHeap<Student> minHeapStudent = new CustomHeap<Student>();
                minHeapStudent.Add(new Student("Student "+1, 98)); 
                minHeapStudent.Add(new Student("Student "+2, 89)); 
                minHeapStudent.Add(new Student("Student "+3, 73)); 
                minHeapStudent.Add(new Student("Student "+4, 78)); 
                minHeapStudent.Add(new Student("Student "+5, 99)); 

            List<Student> students = minHeapStudent.GetItems();
            Console.WriteLine($"Heap Size : {minHeapStudent.Count}");
            foreach (var student in students)
            {
                Console.WriteLine($"Name: {student.Name} , Score: {student.Score}");
            }

        }
    }
}