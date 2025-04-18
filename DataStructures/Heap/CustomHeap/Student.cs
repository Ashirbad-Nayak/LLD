
namespace CustomHeap{
    public class Student : IComparable<Student>
    {
        public string Name {get; private set;}
        public int Score {get; private set;}

        public Student(string name, int score){
            Name = name;
            Score = score;
        }

        public int CompareTo(Student other){
            return this.Score.CompareTo(other.Score);
        }

    }

}