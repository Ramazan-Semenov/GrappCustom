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
			tas n0 = new tas { id = 1, name="Проект 1" };
			tas n1 = new tas { id = 2, name = "Проект 2" };
			tas n2 = new tas { id = 3, name = "Проект 3" };
			tas n3 = new tas { id = 4, name = "Проект " };
			tas n9 = new tas { id = 9, name = "Проект 1" };
			tas n4 = new tas { id = 5, name = "Проект 1" };
			tas n10 = new tas { id = 10, name = "Проект 1" };
			List<tas> tas = new List<tas>() { n0, n1, n2, n3, n4, n9, n10 };
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

            node = new Node() { level = new tas { id = 1 } };
			//Node node1=new Node { level=new tas { } }
			//Node node = new Node()
			//{
			//    level = new tas { id = 1 },
			//    Child = new List<Node> { new Node { level = new tas { id = 2 },
			//        Child=new List<Node> { new Node { level=new tas { id=9 } } } }, new Node { level=new tas { id=3 }, Child=new List<Node>() { new Node {  level=new tas {id =4 } } } } }
			//};
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


		static void addElem(Node node, List<relaCh> ches, List<tas> tass)
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
						Console.WriteLine(item.name);

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
	}
	[XmlRoot(ElementName = "Node")]
	public class Node
	{
		public tas level { get; set; }
		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }
		[XmlElement(ElementName = "Children")]
		public List<Node> Child { get; set; } = new List<Node>();
	}
	public class rela
	{
		public tas Parent { get; set; }
		// public int id_Par { get; set; }
		public tas Children { get; set; }
		//   public int id_Child { get; set; }

	}
	public class tas
	{
		public int id { get; set; }
		public string name { get; set; }

	}
	public class relaCh
	{
		//public tas Parent { get; set; }
		public int id_Par { get; set; }
		//public tas Children { get; set; }
		public int id_Child { get; set; }

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
