using MTGProxyTutor.Contracts.Models.App;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Collections.Generic;
using System.IO;

namespace MTGProxyTutor.BusinessLogic.PDF
{
	public static class PDFHelper
	{
		const double mult = 0.8;
		const int PDFCardWidth = (int)(223 * mult);
		const int PDFCardHeight = (int)(310 * mult);
		const int marginTop = 60;
		const int marginBottom = 10;
		const int marginLeft = 60;
		const int marginRight = 10;

		public static void SavePDF(List<CardWrapper> cards, string filename)
		{
			var doc = new PdfDocument();
			PdfPage page;
			XGraphics xgr = null;

			var colNum = -1;
			var rowNum = 0;
			var pageNum = -1;

			foreach (var card in cards)
			{
				for (int i = 0; i < card.Quantity; i++)
				{
					colNum++;
					if (colNum == 3)
					{
						colNum = 0;
						rowNum++;
					}

					if (rowNum == 3 || pageNum == -1)
					{
						rowNum = 0;
						pageNum++;
						page = doc.AddPage();
						var size = PageSizeConverter.ToSize(PageSize.A4);
						page.Width = size.Width;
						page.Height = size.Height;
						page.Orientation = PageOrientation.Portrait;
						page.TrimMargins.Top = marginTop;
						page.TrimMargins.Right = marginRight;
						page.TrimMargins.Bottom = marginBottom;
						page.TrimMargins.Left = marginLeft;
						xgr = XGraphics.FromPdfPage(doc.Pages[pageNum]);
					}

					var rect = new XRect(colNum * PDFCardWidth, rowNum * PDFCardHeight, PDFCardWidth, PDFCardHeight);
					var image = XImage.FromStream(new MemoryStream(card.Card.Image));
					xgr.DrawImage(image, rect);
				}
			}

			doc.Save(filename);
			doc.Close();
		}
	}
}
