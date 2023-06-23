namespace Core.Specifications
{
    public class SpecificationParams
    {
        private const int MaxPageSize = 20;
        public int PageIndex { get; set; } = 1; // Default to first page.
        private int _pageSize { get; set; } = 6;

        // Create get and set for PageSize
        public int PageSize 
        { 
            get => _pageSize;
            set {
                if (value > 0)
                {
                    _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
                }
            }
        }
        public string Sort { get; set; }
        
        private string _search;

        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}