

namespace Dictionary_AKA_HashMMap.Dictionary{
    public class HashMap<K,V>
    {
        //Node only in the context of this hashmap class.Hence creating class sinside the class
        private class Node{
            public K key;
            public V value;
            public Node next;
            public Node(K key, V value){
                this.key = key;
                this.value = value;
            }
        }

        private int capacity;
        public int Count{get; private set;}
        private Node[] NodeArray;
        private const double loadFactorThreshold = 0.75;

        public HashMap(int initialSize){
            capacity = initialSize;
            NodeArray = new Node[capacity];
            Console.WriteLine("Capacity is "+ capacity);
        }


        public V Get(K key){
            int index = GetIndex(key);
            Node node = NodeArray[index];
            
            while(node != null){
                if (EqualityComparer<K>.Default.Equals(node.key, key)){
                    Console.WriteLine($"{key} : {node.value}");
                    return node.value;
                }
                node =node.next;
            }
            Console.WriteLine($"Such key - {key} does not exist.");
            return default; 
            
            //default is a keyword that gives you the default value for a given type.
            // int x = default;        // 0
            // bool b = default;       // false
            // string s = default;     // null
            // object o = default;     // null
            // DateTime dt = default;  // 01/01/0001 00:00:00
        }


        public void Put(K key, V value){
            if(((double)Count/capacity) >= loadFactorThreshold) 
                Resize();
            int index = GetIndex(key);
            Node node = NodeArray[index];
            while(node != null){
                if (EqualityComparer<K>.Default.Equals(node.key, key))
                    {
                        node.value = value;
                        Console.WriteLine($"Updated key : {key} with value : {value}");
                        return;
                    }
                node = node.next;
            }

            //node with key not found
            //create new node and add it to front 
            //so if already few nodes in same index 1-> 2-> 3 and new node is 4, now it becomes 4->1->2->3
            //So if no node at same index, it becomes 4->null

            Node newNode = new Node(key, value);
            newNode.next = NodeArray[index]; 

            //keep the head refrence at index
            NodeArray[index] = newNode;      
            
            Count++; //how many nodes in total so far      

            Console.WriteLine($"Added key : {key} with value : {value}");
        }

        public void Remove(K key){
            
            int index = GetIndex(key);
            Node node = NodeArray[index];
            Node prev =null;
            while(node != null){
                if (EqualityComparer<K>.Default.Equals(node.key, key))
                    {
                        if(prev != null) {
                            prev.next = node.next;
                        }
                        else{
                            NodeArray[index] = node.next;
                        }
                        Count--; //how many nodes in total so far   
                        Console.WriteLine($"Removed key : {key}");   
                        return;
                    }
                prev =node;
                node = node.next;
            }
            Console.WriteLine($"Such key - {key} does not exist.");

        }


        private void Resize(){
            //loadfactor threshold reached
            //doubling size
            Console.WriteLine("Threshold reached.Resizing...");
            capacity *= 2;

            Node[] newNodeArray = new Node[capacity];
            foreach(Node node in NodeArray){
                //each index can have a chain of nodes
                Node current = node;

                while(current != null){
                    Node next = current.next;
                    int index = GetIndex(current.key);
                    current.next = newNodeArray[index]; //current pointing to the head node present at the index //if null, current node next points to null
                    newNodeArray[index] = current; //now the node at the index points to current //basically current is the head node at the specific index in new array
                    current = next;
                }

            }

            NodeArray = newNodeArray;
            

        }



        //Get Index
        private int GetIndex(K key){
            //each object in C# can be converted to an int using gethashcode method
            //it can be negative , so Math.abs
            // we want a number within range of array size
            //so % capacity
            int index = Math.Abs(key?.GetHashCode() ?? 0 ) % capacity;
            return index;
        }

        public void DisplayAll(){
            for(int i=0;i<capacity;i++){
                Node node = NodeArray[i];
                Console.WriteLine();
                Console.WriteLine($"Nodes at index {i}: ");
                while(node != null){
                    Console.WriteLine($"{node.key} : {node.value}");
                    node =node.next;
                }
            }
        }


    }
}