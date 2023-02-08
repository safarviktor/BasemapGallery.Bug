using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Location;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Toolkit.UI;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace BasemapGallery.Bug.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {
            Map = new Map(SpatialReference.Create(25833));

            Load().Await();
        }

        private async Task Load()
        {
            var bm1 = new Basemap() { Name = "GeocacheBasis" };
            bm1.BaseLayers.Add(new ArcGISVectorTiledLayer(new Uri("https://services.geodataonline.no/arcgis/rest/services/GeocacheVector/GeocacheBasis/VectorTileServer")));

            var bm2 = new Basemap() { Name = "GeocacheGraatone" };
            bm2.BaseLayers.Add(new ArcGISVectorTiledLayer(new Uri("https://services.geodataonline.no/arcgis/rest/services/GeocacheVector/GeocacheGraatone/VectorTileServer")));


            var gi1 = await BasemapGalleryItem.CreateAsync(bm1);
            var bytes = await File.ReadAllBytesAsync($"{AppDomain.CurrentDomain.BaseDirectory}/thumbnail-B.png");
            gi1.Thumbnail = new Esri.ArcGISRuntime.UI.RuntimeImage(bytes);

            var gi2 = await BasemapGalleryItem.CreateAsync(bm2);
            bytes = await File.ReadAllBytesAsync($"{AppDomain.CurrentDomain.BaseDirectory}/thumbnail-G.png");
            gi2.Thumbnail = new Esri.ArcGISRuntime.UI.RuntimeImage(bytes);

            await Application.Current.Dispatcher.BeginInvoke(() =>
            {
                Map.Basemap = bm1;
                AvailableBasemaps = new ObservableCollection<BasemapGalleryItem>();
                AvailableBasemaps.Add(gi1);
                AvailableBasemaps.Add(gi2);
            });
        }

        private Map _map;
        public Map Map
        {
            get { return _map; }
            set { SetProperty(ref _map, value); }
        }

        private ObservableCollection<BasemapGalleryItem> _availableBasemaps;
        public ObservableCollection<BasemapGalleryItem> AvailableBasemaps
        {
            get { return _availableBasemaps; }
            set { SetProperty(ref _availableBasemaps, value); }
        }
    }
}
