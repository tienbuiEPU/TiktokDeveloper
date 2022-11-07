using Api.Common.Interfaces.Repository;
using System.Collections.Generic;

namespace Api.Common.ViewModels.Paging
{
    public class PagedResult<TDataResult> : IPagedResult<TDataResult>
    {
        public List<TDataResult> Results { get; set; } = new List<TDataResult>();
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
    }
}
