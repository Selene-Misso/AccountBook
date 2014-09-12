using System;
using System.Collections.Generic;
using System.IO;
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
		// 設定ファイル名
		string path = "settings/options.ini";

		public OptionWindow()
		{
			InitializeComponent();
		}
		
		private void fileSelect_Click(object sender, RoutedEventArgs e)
		{
			// 構成
			ofd.InitialDirectory = System.IO.Path.GetDirectoryName(filePath.Text); // 起動ディレクトリ
			ofd.FileName = System.IO.Path.GetFileName(filePath.Text);
			ofd.DefaultExt = ".accdb"; // デフォルト 拡張子
			ofd.Filter = "データベース (.accdb)|*.accdb"; // 拡張子フィルタ

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

		private void optionWindow_Loaded(object sender, RoutedEventArgs e)
		{
			// 設定ファイル読み込み
			try
			{
				using( StreamReader sr = new StreamReader(path, Encoding.GetEncoding("utf-8")))
				{
					// 1行ずつ読み込み
					while (!sr.EndOfStream)
					{
						string line = sr.ReadLine();
						string[] values = line.Split('=');

						// 属性 Path
						if (values[0] == "Path")
						{
							filePath.Text = values[1].Substring(1,values[1].Length-2);
						}
						
					}

				sr.Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}

		}

		private void ok_Click(object sender, RoutedEventArgs e)
		{
			if (isChanged == false)
			{
				this.Close();
			}
			else
			{
				// 保存して終了
				save_Setting();
				this.Close();
			}
		}

		private void cancel_Click(object sender, RoutedEventArgs e)
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

		/// <summary>
		/// 設定ファイルを書き込む
		/// </summary>
		private void save_Setting()
		{
			try
			{
				using (StreamWriter sw = new StreamWriter(path,false, Encoding.GetEncoding("utf-8")))
				{
					sw.WriteLine("Path=\""+filePath.Text+"\"");
					sw.WriteLine("End=1");

					sw.Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
			}

		}
	}
}
