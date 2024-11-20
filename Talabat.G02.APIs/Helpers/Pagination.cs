using TalabatG02.APIs.Dtos;

namespace TalabatG02.APIs.Helpers
{
    public class Pagination<T>
    {
        public int PagIndex { get; set; }
        public int PagSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }

        public Pagination(int pageIndex, int pageSize,int count, IReadOnlyList<T> data)
        {
            PagIndex = pageIndex;
            PagSize = pageSize;
            Data = data;
            Count = count;
        }
    }
}
