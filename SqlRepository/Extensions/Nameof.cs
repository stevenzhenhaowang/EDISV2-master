using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SqlRepository.Extensions
{
    public class Nameof<T>
    {
        public static string Property<TProp>(Expression<Func<T, TProp>> expression)
        {
            var body = expression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("'expression' should be a member expression");
            return body.Member.Name;
        }
    }
}
