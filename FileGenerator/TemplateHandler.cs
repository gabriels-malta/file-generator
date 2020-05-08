using FileGenerator.Internal;
using System.Collections.Generic;

namespace FileGenerator
{
    public class TemplateHandler<TEntity> : ITemplate<TEntity>
    {
        private readonly TemplateFactory<TEntity> factory;
        public TemplateHandler()
        {
            factory = new TemplateFactory<TEntity>();
        }

        public string GetContent(IEnumerable<TEntity> entities) => factory.GetContent(entities);

        public string GetContent(TEntity entity) => factory.Line(entity);

        public string GetHeader() => factory.Header;
    }
}
