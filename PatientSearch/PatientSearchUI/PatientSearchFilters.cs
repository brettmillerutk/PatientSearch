using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PatientSearch.Models;
using PatientSearch.Services;
using Prism.Mvvm;


namespace PatientSearch.PatientSearchUI
{
    class PatientSearchFilters : BindableBase
    {
        public string LastNameFilter
        {
            get { return _lastNameFilter; }
            set
            {
                SetProperty(ref _lastNameFilter, value);
            }
        }
        public string FirstNameFilter
        {
            get { return _firstNameFilter; }
            set { SetProperty(ref _firstNameFilter, value); }
        }
        public DateTime? DobFilter
        {
            get { return _dobFilter; }
            set { SetProperty(ref _dobFilter, value); }
        }
        public string MrNumberFilter
        {
            get { return _mrNumberFilter; }
            set { SetProperty(ref _mrNumberFilter, value); }
        }

        public void ClearAllFilters()
        {
            this.LastNameFilter = null;
            this.FirstNameFilter = null;
            this.MrNumberFilter = null;
            this.DobFilter = null;
        }

        public List<string> GetActiveFilters()
        {
            List<string> activeFilters = new List<string>();
            
            foreach(PropertyInfo prop in this.GetType().GetProperties())
            {
                if (prop.GetValue(this) != null)
                {
                    activeFilters.Add(prop.Name);
                }
            }

            return activeFilters;
        }

        private Expression ToUpperExpression(Expression input)
        {
            var _input = input;
            MethodInfo _methodInfo = typeof(string).GetMethod("ToUpper", System.Type.EmptyTypes);
            Expression _output = Expression.Call(_input, _methodInfo);
            return _output;
        }

        private Expression StartsWithExpression(Expression leftInput, Expression rightInput)
        {
            var _leftInput = leftInput;
            var _rightInput = rightInput;
            MethodInfo _methodInfo = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
            Expression _output = Expression.Call(_leftInput, _methodInfo, _rightInput);
            return _output;
        }

        private MethodCallExpression WhereCallAppender (IQueryable queryableData, Expression input, Expression precursor, ParameterExpression pe)
        {
            var _input = input;
            var _pe=pe;
            var _precursor = precursor;
            var _queryableData = queryableData;
            Type _dataType = _queryableData.ElementType;
            var _parameter = _pe;
            var delegateType = typeof(Func<,>).MakeGenericType(_dataType, typeof(bool));
            dynamic lambda = Expression.Lambda(delegateType, _input, _parameter);
            

            if (_precursor == null)
            {
                _precursor = _queryableData.Expression;
            }
            

            MethodCallExpression whereCallExpression = Expression.Call(
                typeof(Queryable),
                "Where",
                new Type[] { _dataType},
               _precursor,
                Expression.Lambda<Func<Patient, bool>>(_input, new ParameterExpression[] { _pe }));

            return whereCallExpression;
        }

        public MethodCallExpression BuildFilterExpression (IQueryable queryableData)
        {
            Expression _precursor = null;
            var _queryableData = queryableData;
            var _activeFilters = GetActiveFilters();
            var _parameter = Expression.Parameter(_queryableData.ElementType);
            string mod = string.Empty;
            var _constparam = Expression.Parameter(this.GetType(), "PatientSearchFilters");
            

            foreach (string filter in _activeFilters)
            {
                mod = filter.Replace("Filter", "");
                var _constparam2 = Expression.Constant(this.GetType().GetProperty(filter).GetValue(this,null));
                _precursor = WhereCallAppender(_queryableData, StartsWithExpression(ToUpperExpression(Expression.Property(_parameter, mod)), ToUpperExpression(_constparam2)), _precursor,_parameter);
            }

            return (MethodCallExpression)_precursor;
        }

        private string _lastNameFilter;
        private string _firstNameFilter;
        private DateTime? _dobFilter = null;
        private string _mrNumberFilter;
    }
}
