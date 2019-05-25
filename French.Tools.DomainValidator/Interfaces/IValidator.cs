namespace French.Tools.DomainValidator.Interfaces
{
    public interface IValidator<in TEntity>
    {
        ValidationResult Validar(TEntity entity);
    }
}
