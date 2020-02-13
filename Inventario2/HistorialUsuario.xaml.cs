using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistorialUsuario : ContentPage
    {
        public HistorialUsuario()
        {
            InitializeComponent();
            desdeU.Date = DateTime.Now;
            desdeU.MinimumDate = new DateTime(2000, 1, 1);
            desdeU.MaximumDate = desdeU.Date;
            desdeU.DateSelected += desdeU_DateSelected;

            hastaU.Date = DateTime.Now;
            hastaU.MinimumDate = new DateTime(2000, 1, 1);
            hastaU.MaximumDate = hastaU.Date;
            hastaU.DateSelected += hastaU_DateSelected;
        }
        private void desdeU_DateSelected(object sender, DateChangedEventArgs e)
        {

        }

        private void hastaU_DateSelected(object sender, DateChangedEventArgs e)
        {

        }
    }
}