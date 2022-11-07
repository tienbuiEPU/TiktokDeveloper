using System.Collections.Generic;

namespace Api.Common.Interfaces.Repository
{
    public interface IPagedResult<TDataResult>
    {
        List<TDataResult> Results { get; set; }
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        int PageSize { get; set; }
        int RowCount { get; set; }
    }
}
