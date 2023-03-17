using System.Collections.Generic;

namespace Talabat.Pl.Helper
{
    public class Pagination<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }  
        public IReadOnlyList<T> Data { get; set; }=new List<T>();   
    }
}
