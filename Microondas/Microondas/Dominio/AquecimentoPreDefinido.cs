using System;
using System.ComponentModel;

namespace Microondas.Aplicacao
{
	public class AquecimentoPreDefinido : INotifyPropertyChanged
	{
		private string _nomePrograma;
		private string _alimento;
		private int _tempo;
		private int _potencia;
		private string _caracterAquecimento;
		private string _instrucoes;

		public string NomePrograma 
		{
			get
			{
				return _nomePrograma;
			}
			set
			{
				if (_nomePrograma != value)
				{
					_nomePrograma = value;
					NotifyPropertyChanged("NomePrograma");
				}
			} 
		}
		public string Alimento
		{
			get
			{
				return _alimento;
			}
			set
			{
				if (_alimento != value)
				{
					_alimento = value;
					NotifyPropertyChanged("Alimento");
				}
			}
		}
		public int Tempo
		{
			get
			{
				return _tempo;
			}
			set
			{
				if (_tempo != value)
				{
					_tempo = value;
					NotifyPropertyChanged("Tempo");
				}
			}
		}
		public int Potencia
		{
			get
			{
				return _potencia;
			}
			set
			{
				if (_potencia != value)
				{
					_potencia = value;
					NotifyPropertyChanged("Potencia");
				}
			}
		}
		public string CaracterAquecimento
		{
			get
			{
				return _caracterAquecimento;
			}
			set
			{
				if (_caracterAquecimento != value)
				{
					_caracterAquecimento = value;
					NotifyPropertyChanged("CaracterAquecimento");
				}
			}
		}
		public string Instrucoes
		{
			get
			{
				return _instrucoes;
			}
			set
			{
				if (_instrucoes != value)
				{
					_instrucoes = value;
					NotifyPropertyChanged("Instrucoes");
				}
			}
		}


		public AquecimentoPreDefinido(string nomePrograma, string alimento, int tempo, int potencia, string caracterAquecimento, string instrucoes)
		{
			if (nomePrograma == null &&  alimento == null && tempo == 0 && potencia == 0 && caracterAquecimento == null)
			{
				throw new ArgumentException("Campos Obrigatorios: Nome, Alimento, Caracter, Tempo e Potencia");
			}

			if (potencia > 10 || potencia < 1)
			{
				throw new ArgumentException("Digite um numero entre 1 a 10 para a potencia!");
			}

			NomePrograma = nomePrograma;
			Alimento = alimento;
			Tempo = tempo;
			Potencia = potencia;
			CaracterAquecimento = caracterAquecimento;
			Instrucoes = instrucoes;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged(string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}


	}
}
