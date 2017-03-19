using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lightmap.Querying
{
    internal class SqliteQueryTranslator : ExpressionVisitor
    {
        internal StringBuilder queryBuilder;

        internal string Translate(Expression expression)
        {
            this.queryBuilder = new StringBuilder();
            this.Visit(expression);
            return this.queryBuilder.ToString();
        }

        public override Expression Visit(Expression node)
        {
            return base.Visit(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            base.VisitConstant(node);

            // TODO: This just exist for testing and is not safe. Needs to be stripped out and cached for SqlCommand setup.
            this.queryBuilder.Append("'" + node.Value + "'");
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType != typeof(IQueryable) && node.Method.Name != nameof(Enumerable.Where))
            {
                throw new NotSupportedException($"The {node.Method.Name} method is not supported.");
            }

            ConstantExpression desiredTable = node.Arguments.FirstOrDefault(arg => arg is ConstantExpression) as ConstantExpression;
            if (desiredTable == null)
            {
                throw new NotSupportedException("You must provide a Type that represents a Table.");
            }

            LightmapQuery query = desiredTable.Value as LightmapQuery;
            if (query == null)
            {
                throw new NotSupportedException("The IQueryable given to the provider was not a LightmapQuery");
            }

            this.queryBuilder.Append($"SELECT * FROM {query.ElementType.Name}");

            foreach (Expression exp in node.Arguments.Where(arg => arg != desiredTable))
            {
                this.Visit(exp);
            }

            return node;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            base.VisitUnary(node);
            switch(((LambdaExpression)node.Operand).Body.NodeType)
            {
                case ExpressionType.Equal:
                    this.queryBuilder.Append("= ");
                    break;
            }

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            this.queryBuilder.Append(" WHERE ");
            this.queryBuilder.Append(node.Member.Name + " ");

            return base.VisitMember(node);
        }

        private static Expression StripQuotes(Expression expression)
        {
            while (expression.NodeType == ExpressionType.Quote)
            {
                expression = ((UnaryExpression)expression).Operand;
            }

            return expression;
        }
    }

    public abstract class LightmapProvider : IQueryProvider
    {
        public IQueryable CreateQuery(Expression expression)
        {
            return (IQueryable)Activator.CreateInstance(typeof(LightmapQuery<>)
                .MakeGenericType(TypeCache.GetGenericParameter(expression.GetType(), t => true)), new object[] { this, expression });
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => new LightmapQuery<TElement>(this, expression);

        object IQueryProvider.Execute(Expression expression)
        {
            return this.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return (TResult)this.Execute(expression);
        }

        public abstract string GetQueryText(Expression expression);

        public abstract object Execute(Expression expression);
    }

    public class SqliteProvider2 : LightmapProvider
    {
        public override object Execute(Expression expression)
        {
            var x = new SqliteQueryTranslator().Translate(expression);
            return Activator.CreateInstance(typeof(List<>).MakeGenericType(TypeCache.GetGenericParameter(expression.Type, t => true)));
        }

        public override string GetQueryText(Expression expression)
        {
            throw new NotImplementedException();
        }
    }

    public class LightmapQuery : IQueryable
    {
        public LightmapQuery(Type queryingType, IQueryProvider provider)
        {
            this.ElementType = queryingType;
            this.Provider = provider;
            this.Expression = Expression.Constant(this);
        }

        public LightmapQuery(Type queryingType, IQueryProvider provider, Expression expression)
        {
            this.ElementType = queryingType;
            this.Provider = provider;
            this.Expression = expression;
        }

        public Type ElementType { get; }

        public Expression Expression { get; }

        public IQueryProvider Provider { get; }

        public IEnumerator GetEnumerator()
        {
            return (Provider.Execute<IEnumerable>(Expression)).GetEnumerator();
        }
    }

    public class LightmapQuery<TTable> : LightmapQuery, IOrderedQueryable<TTable>
    {
        public LightmapQuery(IQueryProvider provider) : base(typeof(TTable), provider)
        {
        }

        public LightmapQuery(IQueryProvider provider, Expression expression) : base(typeof(TTable), provider, expression)
        {
            if (!typeof(IQueryable<TTable>).IsAssignableFrom(expression.Type))
            {
                throw new Exception();
            }
        }

        public new IEnumerator<TTable> GetEnumerator()
        {
            return (this.Provider.Execute<IEnumerable<TTable>>(Expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (Provider.Execute<IEnumerable>(Expression)).GetEnumerator();
        }
    }
}
