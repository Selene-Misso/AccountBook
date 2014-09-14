using System;
using System.Collections.Generic;
using System.Globalization;
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
	/// ReceiptJournal.xaml の相互作用ロジック
	/// </summary>
	public partial class ReceiptJournal : Window
	{
		public ReceiptJournal()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void receiptJournal_Loaded(object sender, RoutedEventArgs e)
		{
			// 現在日時を取得
			DateTime today = DateTime.Today;
			
			// 月
			this.monthNum.SelectedIndex = today.Month - 1;
			
			// 年 - 和暦変換
			JapaneseCalendar jcal = new JapaneseCalendar();
			string[] 元号名 = { "明治", "大正", "昭和", "平成" };
			// コンボボックスのリストに入れる
			foreach(string wareki in 元号名){
				this.warekiSelect.Items.Add(wareki);
			}
			// 選択
			this.warekiSelect.SelectedIndex = jcal.GetEra(today) - 1;
			
			// 和暦の年数
			this.warekiNum.Text = jcal.GetYear(today).ToString();
		}


	}
}
