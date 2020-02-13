using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;

namespace Inventario2
{
    public class ControllerHistorialCompleto
    {

        public ControllerHistorialCompleto()
        {

        }

        //bisquedas dependiando de resultado

        public static async Task<List<Movimientos>> searching(ModelHistorialCompleto modelhistorial)
        {

            try
            {
                var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

                return table;

            }
            catch
            {
                return null;
            }
        }



        public async Task<List<Movimientos>> Search(ModelHistorialCompleto modelhistorial)
        {
            /*
            try {
                
                List<Movimientos> lista = await MovementCase2(modelhistorial);
                return lista;
            }
            catch
            {
                return null;
            }
            */


            List<Movimientos> lista = new List<Movimientos>();
            try
            {
                if (modelhistorial.QueryStatus == 1)
                {
                    lista = await MovementCase1(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 2)
                {
                    lista = await MovementCase2(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 3)
                {
                    lista = await MovementCase3(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 4)
                {
                    lista = await MovementCase4(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 5)
                {
                    lista = await MovementCase5(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 6)
                {
                    lista = await MovementCase6(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 7)
                {
                    lista = await MovementCase7(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 8)
                {
                    lista = await MovementCase8(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 9)
                {
                    lista = await MovementCase9(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 10)
                {
                    lista = await MovementCase10(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 11)
                {
                    lista = await MovementCase11(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 12)
                {
                    lista = await MovementCase12(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 13)
                {
                    lista = await MovementCase13(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 14)
                {
                    lista = await MovementCase14(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 15)
                {
                    lista = await MovementCase15(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 16)
                {
                    lista = await MovementCase16(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 17)
                {
                    lista = await MovementCase17(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 18)
                {
                    lista = await MovementCase18(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 19)
                {
                    lista = await MovementCase19(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 20)
                {
                    lista = await MovementCase20(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 21)
                {
                    lista = await MovementCase21(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 22)
                {
                    lista = await MovementCase22(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 23)
                {
                    lista = await MovementCase23(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 24)
                {
                    lista = await MovementCase24(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 25)
                {
                    lista = await MovementCase25(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 26)
                {
                    lista = await MovementCase26(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 27)
                {
                    lista = await MovementCase27(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 28)
                {
                    lista = await MovementCase28(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 29)
                {
                    lista = await MovementCase29(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 30)
                {
                    lista = await MovementCase30(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 31)
                {
                    lista = await MovementCase31(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 32)
                {
                    lista = await MovementCase32(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 33)
                {
                    lista = await MovementCase33(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 34)
                {
                    lista = await MovementCase34(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 35)
                {
                    lista = await MovementCase35(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 36)
                {
                    lista = await MovementCase36(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 37)
                {
                    lista = await MovementCase37(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 38)
                {
                    lista = await MovementCase38(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 39)
                {
                    lista = await MovementCase39(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 40)
                {
                    lista = await MovementCase40(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 41)
                {
                    lista = await MovementCase41(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 42)
                {
                    lista = await MovementCase42(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 43)
                {
                    lista = await MovementCase43(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 44)
                {
                    lista = await MovementCase44(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 45)
                {
                    lista = await MovementCase45(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 46)
                {
                    lista = await MovementCase46(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 47)
                {
                    lista = await MovementCase47(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 48)
                {
                    lista = await MovementCase48(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 49)
                {
                    lista = await MovementCase49(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 50)
                {
                    lista = await MovementCase50(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 51)
                {
                    lista = await MovementCase51(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 52)
                {
                    lista = await MovementCase52(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 53)
                {
                    lista = await MovementCase53(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 54)
                {
                    lista = await MovementCase54(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 55)
                {
                    lista = await MovementCase55(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 56)
                {
                    lista = await MovementCase56(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 57)
                {
                    lista = await MovementCase57(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 58)
                {
                    lista = await MovementCase58(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 59)
                {
                    lista = await MovementCase59(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 60)
                {
                    lista = await MovementCase60(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 61)
                {
                    lista = await MovementCase61(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 62)
                {
                    lista = await MovementCase62(modelhistorial);
                }
                if (modelhistorial.QueryStatus == 63)
                {
                    lista = await MovementCase63(modelhistorial);
                }


                return lista;
            }
            catch
            {
                return null;
            }



        }




        /// 
        /// 
        /// 
        private async Task<List<Movimientos>> MovementCase1(ModelHistorialCompleto modelhistorial)
        {

            // searching only by date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase2(ModelHistorialCompleto modelhistorial)
        {

            // searching only by model 
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.modelo == modelhistorial.modelo).ToListAsync();
            Console.WriteLine("executing");
            return table;
        }

        private async Task<List<Movimientos>> MovementCase3(ModelHistorialCompleto modelhistorial)
        {

            // searching only by model and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.modelo == modelhistorial.modelo).
                Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase4(ModelHistorialCompleto modelhistorial)
        {

            // searching only by product
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.producto == modelhistorial.producto).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase5(ModelHistorialCompleto modelhistorial)
        {

            // searching only by product and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.producto == modelhistorial.producto).
                Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase6(ModelHistorialCompleto modelhistorial)
        {

            // searching only by product and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.producto == modelhistorial.producto).
                Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase7(ModelHistorialCompleto modelhistorial)
        {

            // searching only by model and date, product
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.producto == modelhistorial.producto).
                Where(u => u.modelo == modelhistorial.modelo).Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase8(ModelHistorialCompleto modelhistorial)
        {

            // searching only by product
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.lugar == modelhistorial.lugar).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase9(ModelHistorialCompleto modelhistorial)
        {

            // searching only by model and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.lugar == modelhistorial.lugar).
                Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase10(ModelHistorialCompleto modelhistorial)
        {

            // searching only by movimiento and modelo
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.lugar == modelhistorial.lugar).
                Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase11(ModelHistorialCompleto modelhistorial)
        {

            // searching only by movimiento and date, modelo
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.lugar == modelhistorial.lugar).
                Where(u => u.modelo == modelhistorial.modelo).Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase12(ModelHistorialCompleto modelhistorial)
        {

            // searching only by movimiento and producto
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.lugar == modelhistorial.lugar).
                Where(u => u.producto == modelhistorial.producto).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase13(ModelHistorialCompleto modelhistorial)
        {

            // searching only by movimiendo, producto y serie
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.lugar == modelhistorial.lugar).
                Where(u => u.producto == modelhistorial.producto).Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase14(ModelHistorialCompleto modelhistorial)
        {

            // searching only by movimiendo, producto y modelo
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.lugar == modelhistorial.lugar).
                Where(u => u.producto == modelhistorial.producto).Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase15(ModelHistorialCompleto modelhistorial)
        {

            // searching only by movimiendo, producto  modelo date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.lugar == modelhistorial.lugar).
                Where(u => u.producto == modelhistorial.producto).Where(u => u.modelo == modelhistorial.modelo).
                Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase16(ModelHistorialCompleto modelhistorial)
        {

            // searching only idproduct
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase17(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase18(ModelHistorialCompleto modelhistorial)
        {


            // searching only by idproduct and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase19(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  model, date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.modelo == modelhistorial.modelo).Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase20(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and product
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.producto == modelhistorial.producto).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase21(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and product and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.producto == modelhistorial.producto).Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase22(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and product and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.producto == modelhistorial.producto).Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase23(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and product and model and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.producto == modelhistorial.producto).Where(u => u.modelo == modelhistorial.modelo).
            Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase24(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and movement
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.lugar == modelhistorial.lugar).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase25(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and movement and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.lugar == modelhistorial.lugar).Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase26(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and movement and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.lugar == modelhistorial.lugar).Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase27(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and movement model and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.lugar == modelhistorial.lugar).Where(u => u.modelo == modelhistorial.modelo).
            Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase28(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and movement   product
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.lugar == modelhistorial.lugar).Where(u => u.producto == modelhistorial.producto).
            ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase29(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and movement   product   and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.lugar == modelhistorial.lugar).Where(u => u.producto == modelhistorial.producto).
            Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }


        private async Task<List<Movimientos>> MovementCase30(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and movement   product   and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.lugar == modelhistorial.lugar).Where(u => u.producto == modelhistorial.producto).
            Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase31(ModelHistorialCompleto modelhistorial)
        {

            // searching only by idproduct  and movement   product   and model  and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == modelhistorial.IdProducto).
            Where(u => u.lugar == modelhistorial.lugar).Where(u => u.producto == modelhistorial.producto).
            Where(u => u.modelo == modelhistorial.modelo).
            Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase32(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase33(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase34(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase35(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and model date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.modelo == modelhistorial.modelo).Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase36(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and product
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.producto == modelhistorial.producto).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase37(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and product and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.producto == modelhistorial.producto).Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase38(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and product and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.producto == modelhistorial.producto).Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase39(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and product and model and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.producto == modelhistorial.producto).Where(u => u.modelo == modelhistorial.modelo).
            Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase40(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and movimiento
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.lugar == modelhistorial.lugar).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase41(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and movimiento and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.lugar == modelhistorial.lugar).Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase42(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and movimiento and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.lugar == modelhistorial.lugar).Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase43(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and movimiento  model and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.lugar == modelhistorial.lugar).Where(u => u.modelo == modelhistorial.modelo).
            Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase44(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and movimiento  producto 
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.lugar == modelhistorial.lugar).Where(u => u.producto == modelhistorial.producto).
            ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase45(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and movimiento  producto and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.lugar == modelhistorial.lugar).Where(u => u.producto == modelhistorial.producto).
            Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase46(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and movimiento  producto and model 
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.lugar == modelhistorial.lugar).Where(u => u.producto == modelhistorial.producto).
            Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase47(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and movimiento  producto  model date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.lugar == modelhistorial.lugar).Where(u => u.producto == modelhistorial.producto).
            Where(u => u.modelo == modelhistorial.modelo).Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase48(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase49(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.serie == modelhistorial.serie.ToString()).
            ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase50(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.modelo == modelhistorial.modelo).
            ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase51(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and model and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.modelo == modelhistorial.modelo).
            Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase52(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and product 
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.producto == modelhistorial.producto).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase53(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and product and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.producto == modelhistorial.producto).
            Where(u => u.serie == modelhistorial.serie.ToString()).
            ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase54(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and product and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.producto == modelhistorial.producto).
            Where(u => u.modelo == modelhistorial.modelo).
            ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase55(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and product and model and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.producto == modelhistorial.producto).
            Where(u => u.modelo == modelhistorial.modelo).Where(u => u.serie == modelhistorial.serie.ToString()).
            ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase56(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and movement
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.lugar == modelhistorial.lugar).
            ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase57(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and movement and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.lugar == modelhistorial.lugar).
            Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase58(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and movement and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.lugar == modelhistorial.lugar).
            Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase59(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and movement and model and date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.lugar == modelhistorial.lugar).
            Where(u => u.modelo == modelhistorial.modelo).Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase60(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and movement and product
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.lugar == modelhistorial.lugar).
            Where(u => u.producto == modelhistorial.producto).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase61(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and movement and product and Date
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.lugar == modelhistorial.lugar).
            Where(u => u.producto == modelhistorial.producto).Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase62(ModelHistorialCompleto modelhistorial)
        {

            // searching only by USER and idproduct  and movement and product and model
            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.lugar == modelhistorial.lugar).
            Where(u => u.producto == modelhistorial.producto).Where(u => u.modelo == modelhistorial.modelo).ToListAsync();

            return table;
        }

        private async Task<List<Movimientos>> MovementCase63(ModelHistorialCompleto modelhistorial)
        {

            // searching by everithing, user,idproduct,movimiento, producto, id producto, date

            var table = await App.MobileService.GetTable<Movimientos>().Where(u => u.usuario == modelhistorial.usuario).
            Where(u => u.IdProducto == modelhistorial.IdProducto).Where(u => u.lugar == modelhistorial.lugar).
            Where(u => u.producto == modelhistorial.producto).Where(u => u.modelo == modelhistorial.modelo).
            Where(u => u.serie == modelhistorial.serie.ToString()).ToListAsync();

            return table;
        }

    }
}
