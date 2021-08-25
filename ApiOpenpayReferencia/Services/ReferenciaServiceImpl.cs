using ApiOpenpayReferencia.Helpers;
using ApiOpenpayReferencia.Models;
using Openpay;
using Openpay.Entities;
using Openpay.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiOpenpayReferencia.Services
{
    public class ReferenciaServiceImpl : IReferenciaService
    {
        public Charge GetReferencia(DatoEntrada dato)
        {
            string API_KEY = AppSettings.GetValorOpenpay("ApiKey");
            string MERCHANT_ID = AppSettings.GetValorOpenpay("MerchantID");
            bool productivo = false;

            

            OpenpayAPI openpayAPI = new OpenpayAPI(API_KEY, MERCHANT_ID, productivo);

            //Se crea un customerId
            Customer customer = new Customer();
            customer.Name = dato.Cliente;
            customer.LastName = String.Empty;
            customer.Email = "m@p.mx";
            customer.Address = new Address();
            customer.Address.Line1 = "X";
            customer.Address.PostalCode = "0";//longitud entre 1 y 12
            customer.Address.City = "X";
            customer.Address.CountryCode = "MX";
            customer.Address.State = "X";

            //Descomentar la línea de abajo para poder crear nuevos customerIds
            Customer customerCreated = openpayAPI.CustomerService.Create(customer); //customerCreated.Id	"axeyb4lkq85b1xdvw1au"	string
            string customer_id = customerCreated.Id;
            //string customer_id = "axeyb4lkq85b1xdvw1au";

            //Se crea el Cargo
            ChargeRequest charge = new ChargeRequest();
            charge.Method = "store";
            charge.Amount = dato.Importe; //>0, checar el maximo, sólo enteros
            charge.Description = dato.ClaveSolicitud + "-" + "Abono Macropay " + string.Format("{0:C}", dato.Importe) + dato.ModeloEquipo;

            Charge ch = openpayAPI.ChargeService.Create(customer_id, charge);


            return ch;
        }        
    }
}
