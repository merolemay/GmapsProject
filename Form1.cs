using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GmapsProject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GmapsProject
{
    public partial class Form1 : Form
    {

        private DataLoader dataLoader;

        private List<PointLatLng> puntos;       //Lista de puntos para los Marcadores
        private List<PointLatLng> poligonos;    //Lista de puntos para los poligonos
        private List<PointLatLng> rutas;        //Lista de puntos para las rutas

        GMapOverlay markers = new GMapOverlay("markers");   //La "capa" donde iran los Marcadores
        GMapOverlay polygons = new GMapOverlay("polygons"); //La "capa" donde iran los poligonos
        GMapOverlay routes = new GMapOverlay("routes");     //La "capa" donde iran los rutas  
        public Form1()
        {   
            InitializeComponent();
            dataLoader = new DataLoader();

            puntos = new List<PointLatLng>();
            poligonos = new List<PointLatLng>();
            rutas = new List<PointLatLng>();

            dataViewLoad();

            comboBox1.Items.Add("Departamento");
            comboBox1.Items.Add("Cauca");
            comboBox1.Items.Add("Chocó");
            comboBox1.Items.Add("Nariño");
            
        }

        private void dataViewLoad()
        {
            dgv.ColumnCount = 2;
            dgv.Columns[0].Name = "Ciudad/Municipio";
            dgv.Columns[1].Name = "Departamento";

            dgv.Rows.Clear();

            for(int i=0;i<dataLoader.getLista().Count-1;i++)
            {
                Ciudad aux = dataLoader.getLista()[i];
                String[] row = new string[] { aux.getCiudadNombre(), aux.getDepartamento()};
                dgv.Rows.Add(row);
            }
      
            dgv.AutoResizeColumns();
        }
 

        private void gmap_Load(object sender, EventArgs e)
        {
            gmap.MapProvider = GoogleMapProvider.Instance;  //Proveedor del servicio
            GMaps.Instance.Mode = AccessMode.ServerOnly;

            gmap.Position = new PointLatLng(3.42158, -76.5205); //No necesario, simplemente da una posicion para empezar

            // Se agregan las "capas" al conjunto de capas del GmapControl
            gmap.Overlays.Add(markers);
            gmap.Overlays.Add(polygons);
            gmap.Overlays.Add(routes);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            markers.Clear();
            List<Ciudad> lista = dataLoader.getLista(); //Trae los nombres de los municipios desde el Model

            foreach (Ciudad f in lista)
            {
                GeoCoderStatusCode statusCode;
                PointLatLng? pointLatLng1 = OpenStreet4UMapProvider.Instance.GetPoint(f.getCiudadNombre() + ',' + f.getDepartamento() + ',' + f.getPais(), out statusCode);

                //Las anteriores dos lineas proveen las funcionalidades para hacer la georeferenciación inversa
                if (comboBox1.Text == "Departamento")
                {
                    if (pointLatLng1 != null)
                    {
                        GMapMarker marker00 = new GMarkerGoogle(new PointLatLng(pointLatLng1.Value.Lat, pointLatLng1.Value.Lng), GMarkerGoogleType.blue_dot);
                        marker00.ToolTipText = f + "\n" + pointLatLng1.Value.Lat + "\n" + pointLatLng1.Value.Lng; // Esta linea es solo apariencia
                        markers.Markers.Add(marker00);
                    }
                } else
                {
                    if (f.getDepartamento() == comboBox1.SelectedItem.ToString())
                    {
                        GMapMarker marker00 = new GMarkerGoogle(new PointLatLng(pointLatLng1.Value.Lat, pointLatLng1.Value.Lng), GMarkerGoogleType.blue_dot);
                        marker00.ToolTipText = f + "\n" + pointLatLng1.Value.Lat + "\n" + pointLatLng1.Value.Lng; // Esta linea es solo apariencia
                        markers.Markers.Add(marker00);
                    }
                }
            }

        }

        private void load_table()
        {
            //reset your chart series and legends
            chart1.Series.Clear();
            chart1.Series.Add(null);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
    }
}
