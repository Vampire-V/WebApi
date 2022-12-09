namespace WebApi.Models
{
    public abstract class QueryStringParameters
    {
        const int limit = 10;
        public int page { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > limit) ? limit : value;
            }
        }
    }
}