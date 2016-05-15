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
        public static List<string> getPrimaryKey(Type cls)
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

        public static Dictionary<string, PropertyInfo> getPrimaryTypes(Type cls)
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

        public List<string> getPrimaryKey()
        {
            return getPrimaryKey(this.GetType());
        }

        public Dictionary<string, PropertyInfo> getPrimaryTypes()
        {
            return getPrimaryTypes(this.GetType());
        }

        public Dictionary<string, object> getPKValues()
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
                return SegurancaDBContext.instance().Entry(this);
            }
        }

        public object oldValues(string propertyName)
        {
            return this.Entry.OriginalValues[propertyName];
        }

        public T oldValues<T>(string propertyName)
        {
            return (T)this.Entry.OriginalValues[propertyName];
        }

        public Boolean hasChanged(string propertyName)
        {
            return !this.Entry.CurrentValues[propertyName].Equals(this.Entry.OriginalValues[propertyName]);
        }

    }
}
