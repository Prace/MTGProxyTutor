using MTGProxyTutor.Contracts.Exceptions;
using MTGProxyTutor.Contracts.Models.App;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MTGProxyTutor
{
	public partial class FailedAlert : Form
	{
		private IEnumerable<Tuple<string, Exception>> _failedCards;

		public FailedAlert(IEnumerable<Tuple<string, Exception>> failedCards)
		{
			_failedCards = failedCards;
			InitializeComponent();
		}

		private void FailedAlert_Load(object sender, EventArgs e)
		{
			_failedCards.ToList().ForEach(fc =>
			{
				string reason;
				if (fc.Item2 is WebApiConsumerException)
				{
					var ex = fc.Item2 as WebApiConsumerException;
					reason = $"{ex.StatusCode} - {ex.Message}";
				}
				else 
				{
					reason = fc.Item2?.Message;
				}
				string[] row = { fc.Item1, reason };
				this.listView1.Items.Add(new ListViewItem(row));
			});
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
