namespace Model
{
    public class ListResponse<T>
    {
        public List<T> Data { get; set; }
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}