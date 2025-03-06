using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using Microondas.Aplicacao;
using Microondas.Dominio;


namespace Microondas
{
	/// <summary>
	/// Interação lógica para MainWindow.xam
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly ServicoAquecimento _servicoAquecimento;
		private AquecimentoPreDefinido _aquecimentoPreDefinido;
		private DispatcherTimer _timerAquecimento;
		public ObservableCollection<AquecimentoPreDefinido> OpcaoDeAquecimentoPreDefinido { get; set; } /*= new ObservableCollection<AquecimentoPreDefinido>();*/
		private int? _tempoRestante;
		private bool situacao;

		public MainWindow()
		{
			InitializeComponent();
			_servicoAquecimento = new ServicoAquecimento();
			_timerAquecimento = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1000)};
			OpcaoDeAquecimentoPreDefinido = _servicoAquecimento.AquecimentosPreDefinidos;

			_timerAquecimento.Tick += AtualizarContador;

			DataContext = this;
		}

		private void ButtonIniciar_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (_timerAquecimento.IsEnabled && situacao == false && cmbOpcoesPreDefinidas.SelectedItem == null)
				{
					_tempoRestante += _servicoAquecimento.AcrescentarTempo();
				}
				else if (situacao == true)
				{
					situacao = false;
					_timerAquecimento.Start();
				}
				else
				{
					if (cmbOpcoesPreDefinidas.SelectedItem == null)
					{
						var aquecimento = _servicoAquecimento.ValidarEntrada(txtTempo.Text, txtPotencia.Text);

						txtTempo.Text = aquecimento.FormatarTempo().ToString();
						txtPotencia.Text = aquecimento.Potencia.ToString();
					}
					else
					{
						var aquecimento = _servicoAquecimento.IniciarAquecimento(int.Parse(txtTempo.Text), int.Parse(txtPotencia.Text));
					}

					IniciarContador();

				}
			}
			catch (Exception exc)
			{
				MessageBox.Show($"Erro: {exc.Message}");
			}
		}

		private void ButtonPausarCancelarAquecimento_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (_timerAquecimento.IsEnabled)
				{
					situacao = true;
					_timerAquecimento.Stop();
				}
				else
				{
					CancelarAquecimento();
				}
			}
			catch (Exception exc)
			{
				MessageBox.Show($"Erro: {exc.Message}");
			}
		}

		
		private void IniciarContador()
		{
			_tempoRestante = int.Parse(txtTempo.Text);
			statusAquecimento.Text = "Aquecendo: ";
			_timerAquecimento.Start();
		}
		
		private void AtualizarContador(object sender, EventArgs e)
		{
			_tempoRestante--;
			
			try
			{
				if (_tempoRestante < 0)
				{
					_timerAquecimento.Stop();
					statusAquecimento.Text += " Aquecimento Concluido!";
				}
				else
				{
					statusAquecimento.Text += " " + CaracterAquecimento();
				}
			}
			catch (Exception exc)
			{
				MessageBox.Show($"Erro: Erro: {exc.Message}");
			}
		}

		private string CaracterAquecimento()
		{
			string caracter = ".";
			if (!string.IsNullOrEmpty(txtCaracter.Text))
			{
				caracter = txtCaracter.Text;
			}
			return new string(caracter[0], int.Parse(txtPotencia.Text));
		}

		public void CancelarAquecimento()
		{
			_timerAquecimento.Stop();
			cmbOpcoesPreDefinidas.SelectedItem = null;
			_tempoRestante = 0;
			situacao = false;
			txtTempo.Text = "";
			txtTempo.IsReadOnly = false;
			txtPotencia.Text = "";
			txtPotencia.IsReadOnly = false;
			statusAquecimento.Text = "";
			txtInstrucoes.Text = "";
		}

		private void CmbOpcoesPreDefinidas_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (cmbOpcoesPreDefinidas.SelectedItem == null)
			{
				txtTempo.IsReadOnly = true;
				txtPotencia.IsReadOnly = true;
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			CadastrarProgramas cadastrarProgramas = new CadastrarProgramas();
			cadastrarProgramas.ProgramasCustomizados += ReceberProgramasCustomizados;
			cadastrarProgramas.Show();
		}

		public void ReceberProgramasCustomizados(ObservableCollection<AquecimentoPreDefinido> programas)
		{
			foreach (var programa in programas)
			{
				OpcaoDeAquecimentoPreDefinido.Add(programa);
			}
		}
	}
}
