using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios_Entrega3.Models
{

    public class Cliente
    {
        int identificacion;
        int estrato;
        int metaahorroenergia;
        int consumoactualenergia;
        int promedioconsumodeagua;
        int consumoactualagua;
        int consumogas;

        public Cliente(int identificacion, int estrato, int metaahorroenergia, int consumoactualenergia, int promedioconsumodeagua, int consumoactualagua,int consumogas, int consumoactualagua1, int consumogas1)
        {
            this.identificacion = identificacion;
            this.estrato = estrato;
            this.metaahorroenergia = metaahorroenergia;
            this.consumoactualenergia = consumoactualenergia;
            this.promedioconsumodeagua = promedioconsumodeagua;
            this.consumoactualagua = consumoactualagua;
            this.consumogas = consumogas;
        }
        public int Identificacion { get => identificacion; set => identificacion = value; }
        public int Estrato { get => estrato; set => estrato = value; }
        public int Metaahorroenergia { get => metaahorroenergia; set => metaahorroenergia = value; }
        public int Consumoactualenergia { get => consumoactualenergia; set => consumoactualenergia = value; }
        public int Promedioconsumodeagua { get => promedioconsumodeagua; set => promedioconsumodeagua = value; }
        public int Consumoactualagua { get => consumoactualagua; set => consumoactualagua = value; }
        public int Consumogas { get => consumogas; set => consumogas = value; }
    }
}
