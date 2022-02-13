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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		public Node node { get; set; }

		public MainWindow()
        {
            InitializeComponent();
			Software_Registry n0 = new Software_Registry { id = 1, product_name="Проект 1", owner= "Семенов" };
			Software_Registry n1 = new Software_Registry { id = 2, product_name = "Проект 2", owner = "Кузнецова" };
			Software_Registry n2 = new Software_Registry { id = 3, product_name = "Проект 3", owner = "Степанов" };
			Software_Registry n3 = new Software_Registry { id = 4, product_name = "Проект 4", owner = "Выдрин" };
			Software_Registry n9 = new Software_Registry { id = 9, product_name = "Проект 9", owner = "Емельянова" };
			Software_Registry n4 = new Software_Registry { id = 5, product_name = "Проект 5", owner = "Калиновский" };
			Software_Registry n10 = new Software_Registry { id = 10, product_name = "Проект 10", owner = "Калинина" };
			List<Software_Registry> tas = new List<Software_Registry>() { n0, n1, n2, n3, n4, n9, n10 };
			List<relaCh> ch = new List<relaCh>();
            ch.Add(new relaCh { id_Par = 1, id_Child = 2 });
            ch.Add(new relaCh { id_Par = 1, id_Child = 4 });
            ch.Add(new relaCh { id_Par = 1, id_Child = 5 });
            ch.Add(new relaCh { id_Par = 2, id_Child = 3 });
            ch.Add(new relaCh { id_Par = 2, id_Child = 10 });

            ch.Add(new relaCh { id_Par = 3, id_Child = 5 });
            ch.Add(new relaCh { id_Par = 5, id_Child = 9 });
            ch.Add(new relaCh { id_Par = 4, id_Child = 9 });
            ch.Add(new relaCh { id_Par = 4, id_Child = 10 });
            ch.Add(new relaCh { id_Par = 2, id_Child = 9 });

            ch.Add(new relaCh { id_Par = 10, id_Child = 9 });

            node = new Node() {Name="Главный проект", level = new Software_Registry { id = 1 } };

			addElem(node, ch, tas);
			rec(node);
			//ff.DataContext = node;

			//ff.ItemsSource = node.Child;
			XmlSerializer xsSubmit = new XmlSerializer(typeof(Node));
			var xml = "";
			using (var sww = new StringWriter())
			{
				using (XmlWriter writer = XmlWriter.Create(sww))
				{
					xsSubmit.Serialize(writer, node);
					xml = sww.ToString(); // Your XML
					Console.WriteLine(xml);
				}
			}
			DataContext = this;
			System.Diagnostics.PresentationTraceSources.DataBindingSource.Switch.Level = System.Diagnostics.SourceLevels.Critical;
		}


		static void addElem(Node node, List<relaCh> ches, List<Software_Registry> tass)
		{
			var gg = new List<Node>();
			var nn = new List<int>();
			foreach (var item in ches)
			{
				if (node.level.id == item.id_Par)
				{
					nn.Add(item.id_Child);
				}
			}
			foreach (var item in tass)
			{
				foreach (var ite2 in nn)
				{
					if (item.id == ite2)
					{
						// gg.Add(new Node() {  level=item});
						node.Child.Add(new Node() { level = item, Name = item.id.ToString() });
						Console.WriteLine(item.product_name);

					}
				}

			}
			foreach (var item in node.Child)
			{
				addElem(item, ches, tass);
			}


		}
		static void rec(Node node)
		{
			//Console.WriteLine(node.level.id);
			Node node1 = new Node();
			foreach (var item in node.Child)
			{
				//node1.level = item.level;
				node1.Child = item.Child;
				//Console.WriteLine("--" + item.level.id);

				rec(item);

			}
		}

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
			MessageBox.Show((sender as MenuItem).CommandParameter.ToString());
        }
    }
    [XmlRoot(ElementName = "Node")]
	public class Node
	{
		public Software_Registry level { get; set; }
		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }
		[XmlElement(ElementName = "Children")]
		public List<Node> Child { get; set; } = new List<Node>();
	}
	public class rela
	{
		public Software_Registry Parent { get; set; }
		// public int id_Par { get; set; }
		public Software_Registry Children { get; set; }
		//   public int id_Child { get; set; }

	}
	//public class tas
	//{
	//	public int id { get; set; }
	//	public string name { get; set; }

	//}
	public class relaCh
	{
		//public tas Parent { get; set; }
		public int id_Par { get; set; }
		//public tas Children { get; set; }
		public int id_Child { get; set; }

	}
	public class NewTaskBook
	{
		public int Id { get; set; }
		public DateTime? DateStart { get; set; }
		public DateTime? DateEnd { get; set; }
		public string TypeTask { get; set; }
		public string NameTask { get; set; }
		public string CategoryTask { get; set; }
		public string AffectedProduct { get; set; }
		public string DescriptionTask { get; set; }
		public string Excecuter { get; set; }
		public DateTime? ActualCompletionDate { get; set; }
		public int LaborCosts { get; set; }
		public int ActualLaborCosts { get; set; }
		public string Comment { get; set; }

	}
	public class Software_Registry
	{
		/// <summary>
		/// Id 
		/// </summary>
		public int id { get; set; }
		/// <summary>
		/// Приоритет (Высокий, средний, низкий)
		/// </summary>
		public string priority { get; set; }
		/// <summary>
		/// Вид продукции (ВНутренний, внешний)
		/// </summary>
		public string product_type { get; set; }
		/// <summary>
		/// Имя продукта
		/// </summary>
		public string product_name { get; set; }
		/// <summary>
		/// Краткое описание
		/// </summary>
		public string short_description { get; set; }
		/// <summary>
		/// тип продукта(с#, vba, sql...)
		/// </summary>
		public string type_of_product { get; set; }
		/// <summary>
		/// Заказчик
		/// </summary>
		public string customer { get; set; }
		/// <summary>
		/// Владелец
		/// </summary>
		public string owner { get; set; }
		/// <summary>
		/// главный разработчик 
		/// </summary>
		public string main_developer { get; set; }
		/// <summary>
		/// количество пользователей
		/// </summary>
		public string number_of_users { get; set; }
		/// <summary>
		/// этап
		/// </summary>
		public string stage { get; set; }
		/// <summary>
		/// сопровождение
		/// </summary>
		public string maintenance { get; set; }
		/// <summary>
		/// ссылка на проект
		/// </summary>
		public string link_repository { get; set; }
		/// <summary>
		/// ссылка на описание 
		/// </summary>
		public string link_description { get; set; }
		/// <summary>
		/// комментарии
		/// </summary>
		public string comments { get; set; }

	}
	//class TreeViewLineConverter : IValueConverter
	//{
	//	public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
	//	{
	//		TreeViewItem item = (TreeViewItem)value;
	//		ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);
	//		return ic.ItemContainerGenerator.IndexFromContainer(item) == ic.Items.Count - 1;
	//	}

	//	public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
	//	{
	//		return false;
	//	}
	//}
}
