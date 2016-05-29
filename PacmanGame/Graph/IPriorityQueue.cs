namespace PacmanGame.Graph
{
    /// <summary>
    /// Represent an generic interface for priority queue of any type.
    /// </summary>
    /// <typeparam name="T">A type of element in the queue.</typeparam>
    public interface IPriorityQueue<T>
    {
        /// <summary>
        /// Gets the information whether the queue is empty or not.
        /// </summary>
        bool IsEmpty { get; }
        /// <summary>
        /// Inserts the item to the queue.
        /// </summary>
        /// <param name="item">An element which should be inserted into the queue.</param>
        void Insert(T item);
        /// <summary>
        /// Deletes first item in the queue and returns it.
        /// </summary>
        /// <returns>Returns the deleted element.</returns>
        T DeleteFirst();
        /// <summary>
        /// Informs whether the specified item is in the queue.
        /// </summary>
        /// <param name="item">The item which should be tested.</param>
        /// <returns>True if the queue contains the specified item, otherwise false.</returns>
        bool Contains(T item);
    }
}