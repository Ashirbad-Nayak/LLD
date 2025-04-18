# 🧾 LRU Cache Implementation in C#

This README documents the implementation details of a custom **Least Recently Used (LRU) Cache** built using a combination of a **HashMap (Dictionary)** and a **Doubly Linked List** in C#.

---

## 🚀 Purpose
The LRU cache is a data structure that:
- Stores key-value pairs
- Keeps the most recently accessed items at the front
- Removes the **least recently used** item when the cache exceeds its capacity

---

## 📦 Example Usage
```csharp
var lru = new LRUCache(3);
lru.Add(1, 10);
lru.Add(2, 20);
lru.Add(3, 30);
lru.Get(1);
lru.Add(4, 40); // Evicts key 2
lru.Display();
```

Expected Output:
```
Key : 4, Value : 40
Key : 1, Value : 10
Key : 3, Value : 30

```

---

## 🧠 Time Complexity
- `Get()` – O(1)
- `Add()` – O(1)
- `Remove()` / `AddToHead()` – O(1)