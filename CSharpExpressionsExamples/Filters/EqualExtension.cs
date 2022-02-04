using System.Linq.Expressions;
using System.Reflection;

namespace CSharpExpressionsExamples.Filters;

public static class EqualExtension
{
    // video description https://youtu.be/GgikmzRhHl0
    // ships.Where(t=>t.Name =="Missouri")
    public static IQueryable<TSource> EqualGenericMethod<TSource, TValue>(this IQueryable<TSource> source,
        string propertyName,
        TValue value)
    {
        // t
        ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "t");
        //t.Name
        MemberExpression propertyExpression = Expression.Property(parameterExpression, propertyName);
        // value
        ConstantExpression constValueExpression = Expression.Constant(value);

        // t.Name == value
        BinaryExpression filterExpression = Expression.Equal(propertyExpression, constValueExpression);

        //(t => t.Name == value)
        Expression<Func<TSource, bool>> filterPredicate = Expression.Lambda<Func<TSource, bool>>(filterExpression, parameterExpression);
 
        return source.Where(filterPredicate);
    }


    public static IQueryable<TSource> EqualParseMethod<TSource>(this IQueryable<TSource> source, string propertyName,
        string value)
    {
        // t
        ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "t");

        // t => t.propertyName
        Expression propertyExpression = Expression.Property(parameterExpression, propertyName);

        //Parse expression
        MemberExpression body = propertyExpression as MemberExpression;

        PropertyInfo propertyInfo = body.Member as PropertyInfo;

        //[type].Parse(String)
        MethodInfo parseMethod = propertyInfo.PropertyType.GetMethod("Parse", new[] { typeof(string) });

        // value 
        ConstantExpression constantValueExpression = Expression.Constant(value);

        // [type].Parse(value)
        MethodCallExpression parseCall = Expression.Call(parseMethod, constantValueExpression);

        // t.propertyName == [type].Parse(value)
        BinaryExpression filterExpression = Expression.Equal(propertyExpression, parseCall);

        // (t => t.propertyName == [type].Parse(value))
        Expression<Func<TSource, bool>> filterPredicate =
            Expression.Lambda<Func<TSource, bool>>(filterExpression, parameterExpression);

        return source.Where(filterPredicate);
    }
}
