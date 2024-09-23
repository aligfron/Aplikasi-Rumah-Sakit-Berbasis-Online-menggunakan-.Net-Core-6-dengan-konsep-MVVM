namespace HealthCare340B.Web.AddOns
{
    public class Pagination<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalData { get; set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public Pagination(List<T> pageData, int totalData, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(totalData / (double)pageSize);
            TotalData = totalData;
            AddRange(pageData);
        }

        public static Pagination<T> Create(List<T> sourceData, int pageIndex, int pageSize)
        {
            List<T> pageData = sourceData.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new Pagination<T>(pageData, sourceData.Count(), pageIndex, pageSize);
        }
    }
}
