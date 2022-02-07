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

    // video description https://youtu.be/BFpYiPhuyJk
    //where(t=>t.Id == int.parse("1");
    //where(t=>t.property == [type].Parse(value))
    public static IQueryable<TSource> EqualParseMethod<TSource>(this IQueryable<TSource> source,
        string propertyName,
        string value)
    {
        //t
        ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "t");

        //t.Property
        MemberExpression propertyExpression = Expression.Property(parameterExpression, propertyName);

        //[type].Parse(value)
        PropertyInfo properyInfo = propertyExpression.Member as PropertyInfo;

        MethodInfo parseMethod = properyInfo.PropertyType.GetMethod("Parse", new[] { typeof(string) });

        ConstantExpression constantExpression
            = Expression.Constant(value);

        MethodCallExpression parseCall = Expression.Call(parseMethod, constantExpression);

        //t.Property == [type].Parse(value)
        BinaryExpression filterExpression = Expression.Equal(propertyExpression, parseCall);


        //(t=>t.Property == [type].Parse(value)
        Expression<Func<TSource, bool>> filterPredicate = Expression.Lambda<Func<TSource, bool>>(filterExpression, parameterExpression);

        return source.Where(filterPredicate);
    }

    // video description https://youtu.be/BFpYiPhuyJk
    public static IQueryable<TSource> EqualParseMethod<TSource>(this IQueryable<TSource> source,
        ICollection<(string PropertyName, string PropertyValue)> filters)
    {
        ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "t");

        BinaryExpression filterExpression = null;

        foreach (var (propertyName, propertyValue) in filters)
        {
            MemberExpression propertyExpression = Expression.Property(parameterExpression, propertyName);

            PropertyInfo properyInfo = propertyExpression.Member as PropertyInfo;

            MethodInfo parseMethod = properyInfo.PropertyType.GetMethod("Parse", new[] { typeof(string) });

            ConstantExpression constantExpression
                = Expression.Constant(propertyValue);

            MethodCallExpression parseCall = Expression.Call(parseMethod, constantExpression);

            filterExpression = filterExpression switch
            {
                null => Expression.Equal(propertyExpression, parseCall),
                _ => Expression.And(filterExpression, Expression.Equal(propertyExpression, parseCall))
            };
        }

        Expression<Func<TSource, bool>> filterPredicate = Expression.Lambda<Func<TSource, bool>>(filterExpression, parameterExpression);

        return source.Where(filterPredicate);
    }
}
