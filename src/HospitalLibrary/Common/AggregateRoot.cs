namespace HospitalLibrary.Common
{
    public abstract class AggregateRoot<T>
    {
        public T Id { get; protected set; }
        
        protected AggregateRoot(T id)
        {
            Id = id;
        }
    }
}