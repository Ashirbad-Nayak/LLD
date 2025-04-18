# Custom Binary Heap Implementation in C# - README

## 📌 Overview
This project demonstrates a custom implementation of a **binary heap** in C#. The binary heap is implemented as a generic class, supporting any data type `T` that implements `IComparable<T>`. This allows the heap to maintain elements in a defined order and be used as a **min-heap** or **max-heap**, depending on the comparison logic.

---

## ❓ Why Is It Called a Binary Heap?
- **Binary**: It's based on a **binary tree**, where each node has at most **two children**.
- **Heap**: It follows the **heap property**:
  - In a **min-heap**, each parent is less than or equal to its children.
  - In a **max-heap**, each parent is greater than or equal to its children.
- It's also a **complete binary tree**, meaning it's filled level-by-level from left to right.
- The structure is backed by an **array/list** to optimize access and manipulation.

---

## 🚀 Key Features
- Generic support (`T` where `T : IComparable<T>`)
- Dynamic resizing
- Efficient `Add`, `Peek`, and `Remove` operations in O(log n)
- Internally backed by a List for array-style performance

---

## 🧠 Intuition Behind Heap Operations

### Why Move the Last Element to the Top During Removal?
- When removing the root (top) element, instead of shifting everything, we replace it with the **last element** (which is easy to remove in O(1)).
- Then, we **bubble it down** using `HeapifyDown()` to restore heap order.
- This keeps the time complexity at O(log n).

### Visual Walkthrough of Root Removal

#### Initial Min-Heap:
```
Tree:        Array:
    5         [5, 10, 15, 20, 30, 40, 50]
   / \ 
 10  15
/ \   / \
20 30 40 50
```

#### Step-by-step Removal:
1. Remove 5 (root), move 50 to root
2. Heapify down: swap 50 ↔ 10
3. Heapify down again: swap 50 ↔ 20

#### Final:
```
Tree:        Array:
   10         [10, 20, 15, 50, 30, 40]
  /  \ 
 20   15
/ \   /
50 30 40
```

---

## 🔍 Generics and `IComparable<T>`

The line:
```csharp
public class CustomHeap<T> where T : IComparable<T>
```
means:
- This heap works with any type `T` that **knows how to compare itself**.
- The heap can safely call `CompareTo()` to maintain order.

### Example with Custom `Student` Class:
```csharp
public class Student : IComparable<Student> {
    public string Name;
    public int Score;

    public int CompareTo(Student other) {
        return this.Score.CompareTo(other.Score);
    }
}
```
This lets you create:
```csharp
var studentHeap = new CustomHeap<Student>();
```
And insert students by their score.

---

## 🛠️ Common Operations
- `Add(T item)`: Add an item and bubble it up
- `Peek()`: Look at the top item
- `Remove()`: Remove top, move last to top, and bubble down

---

## 📦 Example Usage
```csharp
var heap = new CustomHeap<int>();
heap.Add(5);
heap.Add(2);
heap.Add(7);
Console.WriteLine(heap.Remove()); // 2
Console.WriteLine(heap.Remove()); // 5
```