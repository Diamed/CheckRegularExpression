using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CheckRegularExpression
{
	internal class ResultBox
	{
		private TextBox _tbResult;

		internal ResultBox(TextBox tb)
		{
			_tbResult = tb;
		}

		internal void Clear()
		{
			_tbResult.Clear();
		}

		internal void AddLine(string line)
		{
			_tbResult.Text += string.Concat(line, "\n");
		}
	}
}
