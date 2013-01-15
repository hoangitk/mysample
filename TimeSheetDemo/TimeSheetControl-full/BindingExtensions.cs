using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;

namespace Cadena.WinForms
{
    public static class BindingExtensions
    {
        public static void Bind<T, U>(this IBindableComponent component,
            Binding binding, Func<T, U> transform)
        {
            binding.Parse += (s, e) => e.Value = transform((T)e.Value);
            binding.Format += (s, e) => e.Value = transform((T)e.Value);
            component.DataBindings.Add(binding);
        }

        public static void Bind(this IBindableComponent component, 
            Binding binding)
        {
            component.DataBindings.Add(binding);
        }

        public static void Bind<T, U>(this IBindableComponent component,
            string propertyName, object dataSource,
            string dataMember, Func<T, U> transform)
        {
            Bind(component, new Binding(propertyName, dataSource, dataMember), transform);
        }

        public static void Bind(this IBindableComponent component,
            string propertyName, object dataSource,
            string dataMember)
        {
            Bind(component, new Binding(propertyName, dataSource, dataMember));
        }

        public static void Bind<C, V>(this IBindableComponent component,
            Expression<Func<C, object>> propertyNameExp, V dataSource,
            Expression<Func<V, object>> dataMemberExp)
        {
            var exp1 = GetMemberInfo(propertyNameExp);

            if (exp1 == null)
                throw new ArgumentException("Lambda expression for PropertyName is not correct");

            var propertyName = exp1.Member.Name;

            var exp2 = GetMemberInfo(dataMemberExp);

            if(exp2 == null)
                throw new ArgumentException("Lambda expression for DataMember is not correct");

            var dataMember = exp2.Member.Name;

            Bind(component, propertyName, dataSource, dataMember);
        }

        public static MemberExpression GetMemberInfo(Expression exp)
        {
            var lambdaExp = exp as LambdaExpression;

            if (lambdaExp == null)
                throw new ArgumentNullException("Lambda expression syntax is not correct");

            MemberExpression memberExp = null;

            if (lambdaExp.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExp = lambdaExp.Body as MemberExpression;
            }
            else if (lambdaExp.Body.NodeType == ExpressionType.Convert)
            {
                memberExp = ((UnaryExpression)lambdaExp.Body).Operand as MemberExpression;
            }
            
            return memberExp;
        }

        public static string GetPropertyName(MemberExpression memberExp)
        {
            if(memberExp == null)
                throw new ArgumentException("Lambda expression for PropertyName is not correct");

            return memberExp.Member.Name;
        }

        public static string GetPropertyName(Expression exp)
        {
            return GetPropertyName(GetMemberInfo(exp));
        }

        public static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var exp = action.Body as MemberExpression;
            if (exp == null)
                throw new ArgumentException("Lambda expression for PropertyName is not correct");

            var propertyName = exp.Member.Name;

            return propertyName;
        }
    }
}
