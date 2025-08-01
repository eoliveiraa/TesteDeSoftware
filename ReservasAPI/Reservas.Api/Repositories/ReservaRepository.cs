﻿using Reservas.Api.Interfaces;
using Reservas.Api.Models;

namespace Reservas.Api.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private Dictionary<int, Reserva> items;

        public ReservaRepository()
        {
            items = new Dictionary<int, Reserva>();

            new List<Reserva> {
             new Reserva {ReservaId=1, Nome = "Macoratti", InicioLocacao = "São Paulo", FimLocacao="Lins" },
             new Reserva {ReservaId=2, Nome = "Paulo", InicioLocacao = "Campinas", FimLocacao="São Paulo" },
             new Reserva {ReservaId=3, Nome = "Maria", InicioLocacao = "Jundiaí", FimLocacao="Campinas" }
             }.ForEach(r => AddReserva(r));
        }

        public Reserva this[int id] => items.ContainsKey(id) ? items[id] : null;

        public IEnumerable<Reserva> Reservas => items.Values;

        public Reserva AddReserva(Reserva reserva)
        {
            if (reserva.ReservaId == 0)
            {
                int key = items.Count;
                while (items.ContainsKey(key)) { key++; }
                ;
                reserva.ReservaId = key;
            }
            items[reserva.ReservaId] = reserva;
            return reserva;
        }

        public void DeleteReserva(int id)
        {
            items.Remove(id);
        }

        public Reserva UpdateReserva(Reserva reserva)
        {
            AddReserva(reserva);
            return reserva;
        }
    }
}
