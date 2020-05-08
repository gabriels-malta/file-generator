using System.Collections.Generic;

namespace FileGenerator
{
    public interface ITemplate<TEntity>
    {
        string GetHeader();
        string GetContent(IEnumerable<TEntity> entities);
        string GetContent(TEntity entity);
    }
}
