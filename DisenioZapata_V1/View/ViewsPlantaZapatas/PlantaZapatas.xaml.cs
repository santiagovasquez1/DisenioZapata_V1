using DisenioZapata_V1.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace DisenioZapata_V1.View.ViewsPlantaZapatas
{
    /// <summary>
    /// Lógica de interacción para PlantaZapatas.xaml
    /// </summary>
    public partial class PlantaZapatas : Window
    {

        public PlantaZapatas()
        {
            InitializeComponent();
            CanvasZapatas.Xw = (float)this.Width-30f;
            CanvasZapatas.Yh = (float)this.Height - 30f;
            CanvasZapatas.DibujarPlantas();
        }

        private void DrawingZapatas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CanvasZapatas.Xw = (float)this.Width - 30f;
            CanvasZapatas.Yh = (float)this.Height - 30f;
            CanvasZapatas.DibujarPlantas();
        }

        private void Window_StateChanged(object sender, System.EventArgs e)
        {
            CanvasZapatas.Xw = (float)this.Width - 30f;
            CanvasZapatas.Yh = (float)this.Height - 30f;
            CanvasZapatas.DibujarPlantas();
        }
    }
}