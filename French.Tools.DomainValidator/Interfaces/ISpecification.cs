namespace French.Tools.DomainValidator.Interfaces
{
    public interface ISpecification<in T>
    {
        bool IsSatisfiedBy(T entidade);
    }
}
