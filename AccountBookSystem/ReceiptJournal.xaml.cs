using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
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
		private string path = @"C:\Users\Owner\Documents\Visual Studio 2013\Projects\AccountBookSystem\AccountBookSystem\bin\Debug\settings\database.accdb";
		private string strSql = "SELECT * FROM TestTable";
		private OleDbConnection oleCon;
		private OleDbCommand oleComd;

		public ReceiptJournal()
		{
			InitializeComponent();
			LoadDB();
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

		private void LoadDB()
		{
			oleCon = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";");
			oleComd = new OleDbCommand(strSql, oleCon);
			DataTable dt = new DataTable("dt_TestTable");
			
			// 列の定義
			dt.Columns.Add("dt_ID", typeof(int));
			dt.Columns.Add("dt_氏名", typeof(string));
			dt.Columns.Add("dt_読み", typeof(string));

			try
			{
				oleCon.Open();
				using (OleDbDataAdapter adapter = new OleDbDataAdapter())
				{
					adapter.SelectCommand = oleComd;

					// マッピング
					adapter.TableMappings.Add("TestTable","dt_TestTable");
					adapter.TableMappings["TestTable"].ColumnMappings.Add("ID", "dt_ID");
					adapter.TableMappings["TestTable"].ColumnMappings.Add("氏名", "dt_氏名");
					adapter.TableMappings["TestTable"].ColumnMappings.Add("読み", "dt_読み");

					// DataSetオブジェクトにない列orテーブルは無視
					//adapter.MissingMappingAction = MissingMappingAction.Ignore;

					viewDataGrid.DataContext = dt;

					adapter.Fill(dt);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				oleCon.Close();
			}
		}
	}
}
