using Microondas.Dominio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;

namespace Microondas.Aplicacao
{
	class ServicoAquecimento
	{
		public readonly ObservableCollection<AquecimentoPreDefinido> AquecimentosPreDefinidos = new ObservableCollection<AquecimentoPreDefinido>
		{
			new AquecimentoPreDefinido("Pipoca", "Pipoca (de micro-ondas)", 180, 7, "@", "Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento."),
			new AquecimentoPreDefinido("Leite", "Leite", 300, 5, "*", "Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode causar fervura imediata causando risco de queimaduras."),
			new AquecimentoPreDefinido("Carnes de boi", "Carne em pedaço ou fatias", 840, 4, "~", "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o escongelamento uniforme."),
			new AquecimentoPreDefinido("Frango", "Frango (qualquer corte)", 480, 7, "$", "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."),
			new AquecimentoPreDefinido("Feijão", "Feijão congelado", 480, 9, "#", "Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo pode perder resistência em altas temperaturas.")
		};


		public Aquecimento IniciarAquecimento(int tempo, int potencia)
		{
			return new Aquecimento(tempo, potencia);
		}

		public Aquecimento ServicoAquecimentoRapido()
		{
			return Aquecimento.AquecimentoRapido();
		}

		public Aquecimento ValidarEntrada(string tempo, string potencia)
		{
			if (string.IsNullOrEmpty(tempo) && string.IsNullOrEmpty(potencia))
			{
				return Aquecimento.AquecimentoRapido();
			}
			if (string.IsNullOrEmpty(potencia))
			{
				potencia = "10";
			}

			int tempoInt = int.Parse(tempo);
			int potenciaInt = int.Parse(potencia);

			if (tempoInt > 120 || tempoInt < 1)
			{
				throw new ArgumentException("O tempo maximo permitido é de 2 minutos / minimo 1 segundo");
			}

			if (potenciaInt < 1 || potenciaInt > 10)
			{
				throw new ArgumentException("Potencia maxima: 10 / Potencia minima: 1");
			}
			
			return new Aquecimento(tempoInt, potenciaInt);
		}

		public int AcrescentarTempo()
		{
			return 30;
		}

	}
}
