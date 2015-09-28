namespace Domain.Portfolio.Base
{
    public abstract class EntityBase : DomainBase
    {
        /// <summary>
        ///     This Id represents the corresponding identification value of each entity.
        ///     E.g. Equity will have its Id equals to its ticker;
        /// </summary>
        public string Id { get; set; }
    }
}