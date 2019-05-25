using Credpay.Tools.DomainValidator.Interfaces;

namespace Credpay.Tools.DomainValidator
{
    public class Rule<TEntity> : IRule<TEntity>
    {
        private readonly ISpecification<TEntity> specificationRule;

        public string MensagemErro { get; private set; }

        public Rule(ISpecification<TEntity> rule, string mensagemErro)
        {
            this.specificationRule = rule;
            this.MensagemErro = mensagemErro;
        }

        public bool Validar(TEntity entity)
        {
            return this.specificationRule.IsSatisfiedBy(entity);
        }
    }
}
