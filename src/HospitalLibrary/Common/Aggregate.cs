namespace HospitalLibrary.Common
{
    public abstract class Aggregate<T, TId> where T : AggregateRoot<TId>
    {
        public T Root { get; protected set; }
        
        protected Aggregate(T root)
        {
            Root = root;
        }
    }
}