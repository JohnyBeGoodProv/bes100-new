using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Models
{
    public class GetBooksResponse
    {
        public IList<GetBooksResponseItem> Books { get; set; }
        public string GenreFilter { get; set; }
        public int NumberOfBooks { get; set; }
    }

    public class GetBooksResponseItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int NumberOfPages { get; set; }
    }
}
