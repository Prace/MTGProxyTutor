namespace MTGProxyTutor.BusinessLogic.PDF
{
    internal class PDFCoordinate
    {
        public PDFCoordinate()
        { }

        public PDFCoordinate(int pageNumber, int rowNumber, int colNumber)
        {
            PageNumber = pageNumber;
            RowNumber = rowNumber;
            ColNumber = colNumber;
        }

        public int PageNumber { get; set; }
        public int RowNumber { get; set; }
        public int ColNumber { get; set; }

        public PDFCoordinate Clone()
        {
            return new PDFCoordinate(PageNumber, RowNumber, ColNumber);
        }
    }
}