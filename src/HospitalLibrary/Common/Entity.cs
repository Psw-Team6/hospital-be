namespace HospitalLibrary.Common
{
    public abstract class Entity<T>
    {
        public T Id { get; protected set; }

        protected Entity(T id)
        {
            Id = id;
        }
        protected Entity()
        {
            
        }
    }
}