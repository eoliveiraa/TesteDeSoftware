using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservas.Api.Models;

namespace Reservas.Testes.MockData
{
    public class ReservasMockData
    {
        public static List<Reserva> GetReservas()
        {
            return new List<Reserva> ()
            {
                new Reserva { ReservaId = 1, Nome = "Reserva 1", InicioLocacao = "São Paulo", FimLocacao = "Campinas" },
                new Reserva { ReservaId = 2, Nome = "Reserva 2", InicioLocacao = "Rio De Janeiro", FimLocacao = "Bahia" },
                new Reserva { ReservaId = 3, Nome = "Reserva 3", InicioLocacao = "Bahia", FimLocacao = "São Paulo" }
            };
        }
        
           
        }
    }

