using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Seguranca.Entidades.model
{
    public class CustomModel
    {
        public static List<string> GetPrimaryKey(Type cls)
        {
            List<string> result = new List<string>();
            System.Object[] attributes;
            foreach (PropertyInfo pI in cls.GetProperties())
            {
                attributes = pI.GetCustomAttributes(true);
                foreach (object attribute in attributes)
                {
                    if (attribute is KeyAttribute)
                        result.Add(pI.Name);
                }
            }
            return result;
        }

        public static Dictionary<string, PropertyInfo> GetPrimaryTypes(Type cls)
        {
            Dictionary<string, PropertyInfo> result = new Dictionary<string, PropertyInfo>();
            System.Object[] attributes;
            foreach (PropertyInfo pI in cls.GetProperties())
            {
                attributes = pI.GetCustomAttributes(true);
                foreach (object attribute in attributes)
                {
                    if (attribute is KeyAttribute)
                        result.Add(pI.Name, pI);
                }
            }
            return result;
        }

        public List<string> GetPrimaryKey()
        {
            return GetPrimaryKey(this.GetType());
        }

        public Dictionary<string, PropertyInfo> GetPrimaryTypes()
        {
            return GetPrimaryTypes(this.GetType());
        }

        public Dictionary<string, object> GetPKValues()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            System.Object[] attributes;
            foreach (PropertyInfo pI in this.GetType().GetProperties())
            {
                attributes = pI.GetCustomAttributes(true);
                foreach (object attribute in attributes)
                {
                    if (attribute is KeyAttribute)
                        result[pI.Name] = pI.GetValue(this, null);
                }
            }
            return result;
        }

        public DbEntityEntry Entry
        {
            get
            {
                return SegurancaDBContext.Instance().Entry(this);
            }
        }

        public object OldValues(string propertyName)
        {
            return this.Entry.OriginalValues[propertyName];
        }

        public T OldValues<T>(string propertyName)
        {
            return (T)this.Entry.OriginalValues[propertyName];
        }

        public Boolean HasChanged(string propertyName)
        {
            return !this.Entry.CurrentValues[propertyName].Equals(this.Entry.OriginalValues[propertyName]);
        }

    }
}
