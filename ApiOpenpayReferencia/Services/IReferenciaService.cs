using ApiOpenpayReferencia.Models;
using Openpay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiOpenpayReferencia.Services
{
    public interface IReferenciaService
    {
        public Charge GetReferencia(DatoEntrada dato);
    }
}
