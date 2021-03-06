﻿using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace CheckRegularExpression
{
	public partial class MainWindow : Window
	{
		private ResultBox _resultBox;
		private HttpClient httpClient;
		public MainWindow()
		{
			InitializeComponent();

			Dispatcher.UnhandledException += Dispatcher_UnhandledException;

			_resultBox = new ResultBox(TbResultText);
            httpClient = new HttpClient();
		}

		private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			MessageBox.Show(e.Exception.Message, "Ошибка");
		}

		private void BtnCheck_ClickAsync(object sender, RoutedEventArgs e)
		{
			FillResultsAsync();
		}

		private void TbUrl_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				FillResultsAsync();
			}
		}

		private async void FillResultsAsync()
		{
			string responseBodyAsText;

			_resultBox.Clear();
			Uri uri = new Uri(TbUrl.Text);

			HttpResponseMessage response = await httpClient.GetAsync(uri.ToString());
			response.EnsureSuccessStatusCode();

			byte[] bytes = await response.Content.ReadAsByteArrayAsync();
			responseBodyAsText = Encoding.Default.GetString(bytes);
			Regex reg = new Regex(TbPattern.Text);
			MatchCollection matches = reg.Matches(responseBodyAsText);

			foreach (var m in matches)
			{
				_resultBox.AddLine(m.ToString());
			}
		}
	}
}
