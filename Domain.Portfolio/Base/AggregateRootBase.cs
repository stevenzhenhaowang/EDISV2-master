using System;
using Domain.Portfolio.Interfaces;

namespace Domain.Portfolio.Base
{
    public abstract class AggregateRootBase : DomainBase
    {
        /// <summary>
        ///     Repository needs to be injected.
        /// </summary>
        protected IRepository _repository;

        protected AggregateRootBase(IRepository repo)
        {
            _repository = repo;
        }

        /// <summary>
        ///     This Id represents the corresponding identification value of each entity.
        ///     E.g. Equity will have its Id equals to its ticker;
        /// </summary>
        public string Id { get; set; }

        ///// <summary>
        ///// Effective date for this entity
        ///// </summary>
        //public DateTime ToDate { get; set; }

        //public void SetProfileEndDate(DateTime date)
        //{
        //    this.ToDate = date;
        //}




    }
}