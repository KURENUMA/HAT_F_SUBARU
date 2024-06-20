using Dma.DatasourceLoader.Creator;
using System.Collections.Generic;

namespace Dma.DatasourceLoader.Analyzer
{
    public interface IFilterAnalyzer
    {
        List<IFilterCreator> GetCreators();
    }
}