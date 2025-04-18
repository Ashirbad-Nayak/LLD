
namespace LRU.CacheWithLRU{
    public class LRUCache {
    private int capacity;
    private Dictionary<int, ListNode> cache;
    private ListNode head;
    private ListNode tail;


    public LRUCache(int capacity) {
        this.capacity = capacity;
        this.cache = new Dictionary<int, ListNode>();
        this.head = new ListNode(0, 0);
        this.tail = new ListNode(0, 0);
        this.head.next = this.tail;
        this.tail.prev = this.head;
    }


    public int Get(int key) {
        if (cache.TryGetValue(key, out var node)) {
            Remove(node);
            AddToHead(node);

            Console.WriteLine($"Cache Hit. Key: {key}, value: {node.value}");
            return node.value;
        }
        Console.WriteLine("Cache Miss. No such Key exists in Cache.");
        return -1;
    }


    public void Add(int key, int value) {
        if (cache.TryGetValue(key, out var node)) {
            node.value = value;
            Remove(node);
            AddToHead(node);

            Console.WriteLine($"key found.Updating with value: {value}");
        } else {
            if (cache.Count >= capacity) {
                var lruNode = tail.prev;
                Remove(lruNode);
                cache.Remove(lruNode.key);

            Console.WriteLine($"Cache capacity reached.Removing least recently used node with key - {lruNode.key}, value - {lruNode.value}");
            }
            var newNode = new ListNode(key, value);
            AddToHead(newNode);
            cache[key] = newNode;
            Console.WriteLine($"Added key: {key} with value: {value} to cache");
        }
    }

    public void Display(){
        ListNode node = head.next;
       while(node != null && node.next != null){//ignore tail at the end
        Console.WriteLine($"Key : {node.key}, Value : {node.value}");
        node =node.next;
       }
    }


    private void Remove(ListNode node) {
        node.prev.next = node.next;
        node.next.prev = node.prev;
    }


    private void AddToHead(ListNode node) {
        node.prev = head;
        node.next = head.next;
        head.next.prev = node;
        head.next = node;
    }


    private class ListNode {
        public int key;
        public int value;
        public ListNode prev;
        public ListNode next;


        public ListNode(int key, int value) {
            this.key = key;
            this.value = value;
        }
    }
}

}