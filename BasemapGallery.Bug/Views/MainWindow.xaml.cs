using BasemapGallery.Bug.ViewModels;
using System.Threading.Tasks;
using System.Windows;

namespace BasemapGallery.Bug.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BasemapGallery.AvailableBasemaps.Clear();
            BasemapGallery.AvailableBasemaps.AddRange(((MainWindowViewModel)DataContext).AvailableBasemaps);
        }
    }
}
