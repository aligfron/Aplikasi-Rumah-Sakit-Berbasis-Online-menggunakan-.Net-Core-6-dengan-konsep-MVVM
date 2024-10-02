namespace HealthCare340B.Web.AddOns
{
    public class Pagination<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalData { get; set; }
        public int StartItem { get; private set; }
        public int EndItem { get; private set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        // Tambahkan properti untuk memeriksa apakah halaman kosong
        public bool IsEmpty => Count == 0;

        public Pagination(List<T> pageData, int totalData, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(totalData / (double)pageSize);
            TotalData = totalData;

            StartItem = ((pageIndex - 1) * pageSize) + 1;
            EndItem = Math.Min(StartItem + pageData.Count - 1, totalData);
            AddRange(pageData);
        }

        // Modifikasi metode Create untuk menghindari halaman kosong
        public static Pagination<T> Create(List<T> sourceData, int pageIndex, int pageSize)
        {
            int totalData = sourceData.Count;

            // Pastikan pageIndex tidak lebih besar dari jumlah total halaman
            int totalPages = (int)Math.Ceiling(totalData / (double)pageSize);
            if (pageIndex > totalPages)
            {
                // Jika pageIndex terlalu besar, arahkan ke halaman terakhir yang tersedia
                pageIndex = totalPages > 0 ? totalPages : 1;
            }

            List<T> pageData = sourceData.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new Pagination<T>(pageData, totalData, pageIndex, pageSize);
        }
    }
}
