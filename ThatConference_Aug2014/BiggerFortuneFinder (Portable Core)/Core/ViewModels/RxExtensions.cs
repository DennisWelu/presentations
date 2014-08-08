using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reflection;

namespace FortuneFinder.Core.ViewModels
{
    /// <summary>
    /// These utils based on blog post at http://blogs.infosupport.com/data-binding-in-code-using-reactive-extensions/
    /// </summary>
    public static class RxExtensions
    {
        public static PropertyInfo ToPropertyInfo<TTarget, TValue>(this Expression<Func<TTarget, TValue>> expression)
        {
            // Get the body of the expression
            Expression body = expression.Body;
            if (body.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException("Property expression must be of the form 'x => x.SomeProperty'", "expression");
            }

            // Cast the expression to the appropriate type
            MemberExpression memberExpression = (MemberExpression)body;
            return memberExpression.Member as PropertyInfo;
        }

        public static IObservable<TValue> ObservableForProperty<TSource, TValue>(this TSource source, Expression<Func<TSource, TValue>> propertyExpression)
            where TSource : INotifyPropertyChanged
        {
            // Get the information for the property
            var property = propertyExpression.ToPropertyInfo();
            if (property == null)
            {
                // Expression does not indicate a property
                throw new ArgumentException("Property expression must point to a valid property", "propertyExpression");
            }

            // Convert the PropertyChanged event to an Observable
            var eventObservable = Observable.FromEventPattern<PropertyChangedEventArgs>(source, "PropertyChanged");

            // Filter the event and return it
            return eventObservable.Where(e => e.EventArgs.PropertyName == property.Name)
                .Select(e => (TValue)property.GetValue(source, null));
        }
    }
}
