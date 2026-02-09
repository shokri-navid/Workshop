using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace TestWebApplication.Abstraction.Application;

public static class DynamicExpressionExtensions
{
    public static Expression<Func<T, bool>> GenerateFilterExpression<T>(this AbstractQuery query)
    {
        var tempBody = "";
        if (string.IsNullOrWhiteSpace(query.Filter?.FilterBody))
        {
            return DynamicExpressionParser
                .ParseLambda<T, bool>(new ParsingConfig(), true, "1 = 1", null);
        }

        tempBody = query.Filter.FilterBody.Replace("_", ".");

        foreach (var key in query.FilterPropertyMapping().Keys)
        {
            if (tempBody.Contains(key))
            {
                var mappedProperty = query.FilterPropertyMapping()[key];

                if (mappedProperty.Contains("@p"))
                {
                    var property = Regex.Matches(tempBody.Substring(tempBody.IndexOf(key)), "\\@\\d")
                        .FirstOrDefault();
                    if (property == null)
                    {
                        throw new InvalidFilterCriteriaException("Filter properties are not Valid");
                    }

                    mappedProperty = mappedProperty.Replace("@p", property.Value);
                    tempBody = Regex.Replace(tempBody, $"\\(*{key}(.*?)\\@\\d\\)*", mappedProperty);
                }
                else
                {
                    tempBody = tempBody.Replace(key, mappedProperty);
                }
            }
        }

        return DynamicExpressionParser
            .ParseLambda<T, bool>(new ParsingConfig(), true, tempBody, query.Filter.Args);
    }
}