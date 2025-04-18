
namespace CustomHeap{
    
    //T must inherit from IComparable interface 
    // (note that its not saying customheap inherits form Icomparable interface, its saying T Must do that)

    //and must have an implementation of CompareTo method
    //If you're using:
        /*
        int, string, double — they already implement IComparable<T>.

        In case, Your own class like Student — you'd need to implement it:

        public class Student : IComparable<Student> {
            public int Score;
            
            public int CompareTo(Student other) { //this belong to class Student
                return this.Score.CompareTo(other.Score); //score is int so , we used its compareto here//any compare logic that returns -1,0 or 1 works here
            }
        }
        */
    public class CustomHeap<T> where T : IComparable<T>
    {
        private List<T> data;
        private bool isMinHeap;

        public CustomHeap(bool isMinHeap = true)
        {
            this.data = new List<T>();
            this.isMinHeap = isMinHeap;
        }

        public int Count => data.Count;

        public void Add(T item)
        {
            //first add to end
            data.Add(item);
            //then find it's place in the datalist
            HeapifyUp(data.Count - 1);
        }

        public T Peek()
        {
            if (data.Count == 0) throw new InvalidOperationException("Heap is empty.");
            return data[0];
        }

        public T Remove()
        {
            if (data.Count == 0) throw new InvalidOperationException("Heap is empty.");

            //return the first element in list
            //now intuition is hwo do we fix the tree
            /*
            Options:
                Remove root and shift everything up → ❌ Very inefficient (O(n)).

                Leave a hole at the top? → ❌ Breaks the structure.

                Pick a random node to replace the root? → ❌ You can’t guarantee that the heap remains valid

            Realization: The root is just a placeholder.
            You only care that the heap property is restored:

            Every parent is ≤ (or ≥) children.(based on minheap or maxheap)

            You need a replacement at the top.
            Now:

            Who’s the easiest person to move?

            Answer:

            The last element. Why?

            It's at the end — List.RemoveAt(end) is O(1).

            It has no children, so moving it won’t mess up anything below.

            It’s easily accessible.


            */


            T root = data[0];
            data[0] = data[data.Count - 1];
            data.RemoveAt(data.Count - 1);

            HeapifyDown(0);
            return root;
        }

        private void HeapifyUp(int index)
        {
            int parent = (index - 1) / 2;
            while (index > 0 && Compare(data[index], data[parent]) < 0)
            {
                Swap(index, parent);
                index = parent;
                parent = (index - 1) / 2;
            }
        }

        private void HeapifyDown(int index)
        {
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;
            int smallestOrLargest = index;

            if (leftChild < data.Count && Compare(data[leftChild], data[smallestOrLargest]) < 0)
            {
                smallestOrLargest = leftChild;
            }

            if (rightChild < data.Count && Compare(data[rightChild], data[smallestOrLargest]) < 0)
            {
                smallestOrLargest = rightChild;
            }

            if (smallestOrLargest != index)
            {
                Swap(index, smallestOrLargest);
                HeapifyDown(smallestOrLargest);
            }
        }

        private int Compare(T a, T b)
        {
            return isMinHeap ? a.CompareTo(b) : b.CompareTo(a); 
            //thats why Each T type must have a compareto mehtod implemented
            /*
                If you're using:

                int, string, double — they already implement IComparable<T>.

                Your own class like Student — you'd need to implement it

            */
        }

        private void Swap(int i, int j)
        {
            T temp = data[i];
            data[i] = data[j];
            data[j] = temp;
        }

        #region <For display purpose>
        public void Display()
        {
            Console.WriteLine($"Heap Size : {Count}");
            Console.WriteLine(string.Join(", ", data));
        }

        public List<T> GetItems(){
            return data;
        }
        
        #endregion
    } 


}