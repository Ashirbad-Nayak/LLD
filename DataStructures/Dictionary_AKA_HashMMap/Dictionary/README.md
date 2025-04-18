# Custom HashMap Implementation in C#

- This README covers key concepts and design decisions around building a custom generic HashMap<K, V> in C# from scratch, including:

  - Generic types

  - Collision handling with chaining

  - Resizing logic

  - Key C# concepts like default, GetHashCode, and more

# 📌 Core Components

  - 🔹 HashMap<K, V>

  - A generic class to store key-value pairs using hashing.

  - 🔹 Node (Nested Class)

        `private class Node
        {
            public K Key;
            public V Value;
            public Node? Next;

            public Node(K key, V value)
            {
                Key = key;
                Value = value;
            }
        }`

- Node is defined inside the HashMap to keep it scoped.

 - It’s used only internally for chaining collisions.

- 🔄 Key Methods

    - ✅ Put(K key, V value)

        - Computes index via GetIndex(key)

        - Handles collisions by inserting the node at the head of the list for fast insertion (O(1))

        - Triggers resize when load factor exceeds a threshold

- ✅ Get(K key)

    - Computes index

    - Traverses the linked list at the index to find the node

- ✅ Remove(K key)

    - Removes the node with matching key if found

- 🧠 Hashing Logic

    - 📌 GetHashCode()

    - Built-in method that returns an int hash value for the key

    - May be negative — use Math.Abs()

- 📌 GetIndex(K key)

    - return Math.Abs(key?.GetHashCode() ?? 0) % capacity;

    - Maps any hash to a valid bucket index

- 📌 capacity

    - Total number of buckets

    - Used with modulo to ensure valid index

- 🧰 Why Insert at Head (Not Tail)?

    - Inserting at the head of the linked list is:

    - Faster (O(1))

    - Simpler (fewer edge cases)

    - Acceptable because order doesn’t matter in hash maps

- 🧰 Why Class Inside Class?

    - Node is only relevant to the HashMap

    - Helps encapsulation

    - Allows access to K and V generics without redeclaration

- 🛠️ Understanding default

- default(T) returns the default value for any type

    - Used when returning values from the map if the key isn't found:

    - return default(V);

- 📈 Resize Strategy

    - When size exceeds capacity * loadFactor (e.g., 0.75), rehash everything into a bigger bucket array.

## 📦 Final Notes
    - No external dependencies
    - 
    - O(1) insert/get in best case
    - 
    - Collision resolution via separate chaining
    - 
    - Works with any key/value type via generics