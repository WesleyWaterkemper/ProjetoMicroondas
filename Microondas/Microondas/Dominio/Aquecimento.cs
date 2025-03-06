using System;

namespace Microondas.Dominio
{
	class Aquecimento
	{
		public int? Tempo { get; set; }
		public int? Potencia { get; set; }

		public Aquecimento(int tempo, int potencia)
		{
			Tempo = tempo;
			Potencia = potencia;
		}

		public string FormatarTempo()
		{
			if (Tempo > 60 && Tempo < 100)
			{
				var minutos = Tempo / 60;
				var segundos = Tempo % 60;
				return $"{minutos}:{segundos}";
			}
			else
			{
				return Tempo.ToString();
			}
		}

		public static Aquecimento AquecimentoRapido()
		{
			return new Aquecimento(30, 10);
		}

		public override string ToString()
		{
			return Tempo + " " + Potencia;
		}
	}
}
