using System.Collections.Generic;

namespace CompanyEmployee.Helper.Pagination
{
    public class Paginate<T>
    {
        public Paginate(List<T> datas, int currentPage, int totalPage)
        {
            int startPage = currentPage - 2;
            int endPage = currentPage + 2;
            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPage)
            {
                endPage = totalPage;
                if (endPage > totalPage)
                {
                    startPage = endPage - 4;
                }
            }
            Datas = datas;
            CurrentPage = currentPage;
            TotalPage = totalPage;
            StartPage = startPage;
            EndPage = endPage;
        }

        public List<T> Datas { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public bool HasPrevious
        {
            get
            {
                return CurrentPage > 1;
            }
        }
        public bool HasNext
        {
            get
            {
                return CurrentPage < TotalPage;
            }
        }
    }
}
