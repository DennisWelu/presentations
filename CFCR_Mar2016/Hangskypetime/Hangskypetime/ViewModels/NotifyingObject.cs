using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Hangskypetime
{
    /// <summary>
    /// Implements a dynamic property list behavior (similar to DependendyObject) and adds
    /// the notification implementation of INotifyPropertyChanged.
    /// </summary>
    public class NotifyingObject : INotifyPropertyChanged
    {
        static string PropertyName<T>(Expression<Func<T>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
                throw new ArgumentException("expression must be a property expression");

            return memberExpression.Member.Name;
        }

        // Avoid accessing the PropertyMap directly to promote thread-safe access through this base class
        protected Dictionary<string, object> PropertyMap = new Dictionary<string, object>();
        readonly object _padlock = new object();

        /// <summary>
        /// Indexer property, gets or sets the field value with the specified key/field name.
        /// </summary>
        public object this[string key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                Set(key, value);
            }
        }

        /// <summary>
        /// Get the current list of all values in the object out.
        /// </summary>
        //        public List<object> Values
        //        {
        //            get { return PropertyMap.Values.ToList(); }
        //        }

        protected int PropertyCount
        {
            get
            {
                lock (_padlock)
                {
                    return PropertyMap.Count;
                }
            }
        }

        protected bool ContainsKey(string propName)
        {
            lock (_padlock)
            {
                return PropertyMap.ContainsKey(propName);
            }
        }

        protected object Get(string propName)
        {
            lock (_padlock)
            {
                if (PropertyMap.ContainsKey(propName))
                    return PropertyMap[propName];
            }

            return null;
        }

        protected T Get<T>(string propName)
        {
            lock (_padlock)
            {
                if (!PropertyMap.ContainsKey(propName))
                    SetNoNotify(propName, default(T));

                return (T)PropertyMap[propName];
            }
        }

        protected T Get<T>(string propName, T defaultValue)
        {
            lock (_padlock)
            {
                if (!PropertyMap.ContainsKey(propName))
                    SetNoNotify(propName, defaultValue);

                return (T)PropertyMap[propName];
            }
        }

        protected T Get<T>(string propName, Func<T> defaultValue)
        {
            lock (_padlock)
            {
                if (!PropertyMap.ContainsKey(propName))
                    SetNoNotify(propName, defaultValue());

                return (T)PropertyMap[propName];
            }
        }

        protected T Get<T>(Expression<Func<T>> expression)
        {
            return Get<T>(PropertyName(expression));
        }

        protected T Get<T>(Expression<Func<T>> expression, T defaultValue)
        {
            return Get(PropertyName(expression), defaultValue);
        }

        protected T Get<T>(Expression<Func<T>> expression, Func<T> defaultValue)
        {
            return Get(PropertyName(expression), defaultValue);
        }

        protected void Set(string propName, object value)
        {
            var raise = false;

            lock (_padlock)
            {
                if (!PropertyMap.ContainsKey(propName))
                {
                    PropertyMap.Add(propName, value);
                    raise = true;
                }
                else if (!Equals(PropertyMap[propName], value))
                {
                    PropertyMap[propName] = value;
                    raise = true;
                }
            }

            if (raise)
                RaisePropertyChanged(propName);
        }

        protected void Set<T>(Expression<Func<T>> expression, T value)
        {
            Set(PropertyName(expression), value);
        }

        protected void SetNoNotify(string propName, object value)
        {
            lock (_padlock)
            {
                if (!PropertyMap.ContainsKey(propName))
                {
                    PropertyMap.Add(propName, value);
                }
                else if (!Equals(PropertyMap[propName], value))
                {
                    PropertyMap[propName] = value;
                }
            }
        }

        protected void SetNoNotify<T>(Expression<Func<T>> expression, T value)
        {
            SetNoNotify(PropertyName(expression), value);
        }

        /// <summary>
        /// Copies all the values of this notifying object to the target notifying object.
        /// </summary>
        public void CopyTo(NotifyingObject target)
        {
            foreach (var key in PropertyMap.Keys)
                target.Set(key, Get(key));
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
