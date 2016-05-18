namespace PacmanGame.Graph
{
    public interface IPriorityQueue<T>
    {
        bool IsEmpty { get; }
        void Insert(T item);
        T DeleteFirst();
        bool Contains(T item);
    }
}