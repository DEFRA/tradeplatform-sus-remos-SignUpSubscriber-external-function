// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using System.Linq.Expressions;
using System.Reflection;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Validators;

public abstract class ValidatorTestBase<TModel>
{
    protected static ModelSetter Set<T>(Expression<Func<TModel, T>> expr, T value)
    {
        var prop = (PropertyInfo)((MemberExpression)expr.Body).Member;
        return new(prop, value);
    }

    public readonly record struct ErrorDetails(string Property, string Error);
    public readonly record struct ModelSetter(PropertyInfo Property, object? Value)
    {
        public void ApplyTo(TModel header)
        {
            Property.SetValue(header, Value);
        }
    }
}