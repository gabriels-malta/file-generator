using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FileGenerator.Internal
{
    internal class TemplateFactory<TEntity>
    {
        private readonly PropertyInfo[] _propertyInfos;

        public TemplateFactory()
        {
            _propertyInfos = typeof(TEntity).GetProperties();
        }

        public string GetContent(IEnumerable<TEntity> entities)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(Header);
            entities.ToList().ForEach(item =>
            {
                builder.AppendLine(Line(item));
            });
            return builder.ToString();
        }

        public string Header => string.Join(";", _propertyInfos.Select(x => x.Name));
        
        public string Line(TEntity entity)
        {
            ICollection<string> propValues = new List<string>();

            foreach (PropertyInfo info in _propertyInfos)
            {
                string strItem = Convert.ToString(info.GetValue(entity), CultureInfo.InvariantCulture);
                propValues.Add(strItem);
            }

            return string.Join(";", propValues);
        }

    }
}
