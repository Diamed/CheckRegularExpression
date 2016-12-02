using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows;

namespace CheckRegularExpression
{
	public partial class MainWindow : Window
	{
		HttpClient httpClient;
		public MainWindow()
		{
			InitializeComponent();

			Dispatcher.UnhandledException += Dispatcher_UnhandledException;

            httpClient = new HttpClient();
		}

		private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			MessageBox.Show(e.Exception.Message, "Ошибка");
			//return;
		}

		private void BtnCheck_ClickAsync(object sender, RoutedEventArgs e)
		{
			FillResults();
		}

		private void tbUrl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == System.Windows.Input.Key.Enter)
			{
				FillResults();
			}
		}

		private async void FillResults()
		{
			string responseBodyAsText;
			Uri uri = new Uri(tbUrl.Text);

			HttpResponseMessage response = await httpClient.GetAsync(uri.ToString());
			response.EnsureSuccessStatusCode();

			responseBodyAsText = await response.Content.ReadAsStringAsync();
			Regex reg = new Regex(tbPattern.Text);
			MatchCollection matches = reg.Matches(responseBodyAsText);

			foreach (var m in matches)
			{
				tbResultText.Text += m.ToString() + "\n";
			}
		}
	}
}
