namespace HospitalLibrary.Common
{
    public abstract class AbstractAggregate<TRoot,TId> where TRoot : IAggregateRoot<TId>
    {
        public TRoot Root { get; protected set; }
        public TId Id { get; set; }
        
        protected AbstractAggregate(TRoot root)
        {
            Root = root;
        }

        public AbstractAggregate()
        {
            
        }
    }
}