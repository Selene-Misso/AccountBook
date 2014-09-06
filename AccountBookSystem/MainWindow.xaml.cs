using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AccountBookSystem
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		private OptionWindow ow = null;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Quit_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (ow != null)
			{
				ow.Close();
			}
			ow = new OptionWindow();
			ow.Show();
		}
	}
}
