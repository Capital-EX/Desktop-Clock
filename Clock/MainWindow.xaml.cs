using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace ClockDesktopApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
			const double degreeRatio = Math.PI / 180.0;
			var centerX = (int)ClockFace.Width / 2;
			var centerY = (int)ClockFace.Height / 2;
			
			for (double i = 0; i < 360; i += 6)
			{
				var rad = i * degreeRatio;
				Marks.Children.Add(new Line()
					{
						X1 = 140 * Math.Cos(rad) + centerX,
						Y1 = 140 * Math.Sin(rad) + centerY,
						X2 = 190 * Math.Cos(rad) + centerX,
						Y2 = 190 * Math.Sin(rad) + centerY,
						Stroke = new SolidColorBrush(Colors.Black),
						StrokeThickness = i % 5 == 0 ? 8 : 2,
					});
			}

		}
	} // RenderTransform="{Binding MinuteRotation}" RenderTransform="{Binding SecondRotation}"

	public class Clock : INotifyPropertyChanged
	{
		public string TimeString => 
			DateTime.Now.ToLongTimeString();

		public string DateString =>
			DateTime.Now.ToShortDateString();

		public double Seconds => 
			DateTime.Now.Second;

		public double Minutes => 
			DateTime.Now.Minute + Seconds / 60.0;

		public double Hours => 
			DateTime.Now.Hour + Minutes / 60.0;

		public double SecondsAngle =>
			Seconds * 6 + 180;

		public double MinutesAngle =>
			Minutes * 6 + 180;

		public double HoursAngle =>
			Hours * 6 + 180;

		public Clock()
		{
			Task.Factory.StartNew(Tick);
		}

		void Tick()
		{
			while (true)
			{
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("SecondsAngle"));
					PropertyChanged(this, new PropertyChangedEventArgs("MinutesAngle"));
					PropertyChanged(this, new PropertyChangedEventArgs("HoursAngle"));
					PropertyChanged(this, new PropertyChangedEventArgs("TimeString"));
					PropertyChanged(this, new PropertyChangedEventArgs("DateString"));
				}
				System.Threading.Thread.Sleep(999);
				
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
