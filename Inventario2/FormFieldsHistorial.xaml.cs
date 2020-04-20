using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventario2.Models;
using Inventario2.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormFieldsHistorial : ContentPage
    {
        bool NoDate = true;
        int iduser = 0;
        int idlugar = 0;
        int idsalida = 0;
        int idtipomov = 0;
        public delegate void ONFieldEventHandler(Object sender);
        public event ONFieldEventHandler OnEventSender;

        public delegate void ONstatusQueryEventHandler(Object sender);
        public event ONstatusQueryEventHandler OnStatusQuerySender;


        ModelMovements modelhistorial;
        public FormFieldsHistorial()
        {
            InitializeComponent();
            modelhistorial = new ModelMovements();


        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var lugares = await LugaresService.getlugares();

            if (lugares == null)
            {
                await DisplayAlert("error", "error de conexion con el servidor", "Aceptar");
                return;
            }

            if (lugares[0].statuscode == 200 || lugares[0].statuscode == 201)
            {
                pickerlugar.ItemsSource = lugares;
            }


            var users = await UserService.getusers();

            if (users == null)
            {
                await DisplayAlert("error", "error de conexion con el servidor", "Aceptar");
                return;
            }

            if (users[0].statuscode == 200 || users[0].statuscode == 201)
            {
                pickeruser.ItemsSource = users;
            }


            var tiposmov = await MovesService.gettypemoves();

            if (tiposmov == null)
            {
                await DisplayAlert("error", "error de conexion con el servidor", "Aceptar");
                return;
            }

            if (tiposmov[0].statuscode == 200 || tiposmov[0].statuscode == 201)
            {
                pickertipomov.ItemsSource = tiposmov;
            }

        }

        private int validatefields()
        {




            ////////first combination
            ///
            if (InIDProduct.Text.Equals("") && idlugar==0 && iduser==0 && idtipomov==0 && InProd.Text.Equals("") && InModel.Text.Equals("") && InSerie.Text.Equals(""))
            {
                //0 0 0 0 0 0
                return 0;
            }



            return 1;
        }



        private void FillFieldsOnModel(int res)
        {
            //int day = datePickerStart.Date.Day;
            //int month = datePickerStart.Date.Month;
            //string year = datePickerStart.Date.Year.ToString();

            //string AllDate = zeroday + day.ToString() + "/" + zeromonth + month.ToString() + "/" + year;
            
            modelhistorial.IDusuario = iduser;
            modelhistorial.codigo = InIDProduct.Text;
           
            modelhistorial.producto = InProd.Text;
            modelhistorial.modelo = InModel.Text;
            //modelhistorial.fechamovimiento = AllDate;
            modelhistorial.serie = InSerie.Text;
            modelhistorial.IDtipomov = idtipomov;
            modelhistorial.IDlugar = idlugar;
        }


        private async void OnAccept(object sender, EventArgs e)
        {
            Console.WriteLine("testing..");
            int res = validatefields();

            if (res == 0)
            {

                await DisplayAlert("error", "Campos de busqueda vacios", "Aceptar");
            }
            else
            {

                FillFieldsOnModel(res);


                await Navigation.PopAsync();
                OnEventSender(modelhistorial);
            }

        }

        private async void OnCancel(object sender, EventArgs e)
        {
            Console.WriteLine("testing..");
            await Navigation.PopAsync();
        }

        private void DateSwitch_Toggled(object sender, EventArgs e)
        {
            /*
            if (DateSwitch.IsToggled) {
                NoDate = false;
            }
            else
            {
                NoDate = true;
            }
            */
        }



        private void PickerUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var modus = (ModelUser)pickeruser.SelectedItem;
                iduser = modus.ID;
                modus.Dispose();
            }
            catch
            {
                iduser = 0;
            }
            
        }


        private void Pickerlugar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var modus = (ModelLugares)pickerlugar.SelectedItem;
                idlugar = modus.ID;
                modus.Dispose();
            }
            catch
            {
                idlugar = 0;
            }

            
        }

        private void pickertipomov_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var modus = (Modelmoves)pickertipomov.SelectedItem;
                idtipomov = modus.ID;
                modus.Dispose();
            }
            catch
            {
                idtipomov = 0;
            }
            
        }

        void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            int aux = 2;
            var lista = Navigation.NavigationStack;
            for (int x = 0; x < lista.Count; x++)
            {
                if (lista.Count > 3)
                {
                    if (x == 2)
                    {
                        x--;
                        Navigation.RemovePage(lista[aux]);
                    }

                }
            }
            Navigation.PopAsync();
        }
    }
}