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
using System.Windows.Shapes;

namespace AccountBookSystem
{
	/// <summary>
	/// OptionWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class OptionWindow : Window
	{
		/// <summary>
		/// 変更したかを示す変数
		/// </summary>
		private bool isChanged = false;
		// ファイル選択ダイアログボックス
		private Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
		private string filename;

		public OptionWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (isChanged == false)
			{
				this.Close();
			}
			else
			{
				var result = MessageBox.Show(
					"変更せずに閉じますか？",
					"オプション変更確認",
					MessageBoxButton.YesNo,
					MessageBoxImage.Question);
				if (result == MessageBoxResult.Yes)
				{
					this.Close();
				}
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			if (isChanged == false)
			{
				this.Close();
			}
			else
			{
				// 保存して終了
				this.Close();
			}
		}

		private void fileSelect_Click(object sender, RoutedEventArgs e)
		{
			// 構成
			ofd.FileName = "Document"; // デフォルト ファイル名
			ofd.DefaultExt = ".txt"; // デフォルト 拡張子
			ofd.Filter = "Text documents (.txt)|*.txt"; // 拡張子フィルタ

			// ダイアログ表示
			Nullable<bool> result = ofd.ShowDialog();

			// 結果を判定
			if (result == true)
			{
				// ファイル名
				filename = ofd.FileName;
				this.filePath.Text = filename;
				// 変更フラグ変更
				isChanged = true;
			}
		}

		
	}
}
