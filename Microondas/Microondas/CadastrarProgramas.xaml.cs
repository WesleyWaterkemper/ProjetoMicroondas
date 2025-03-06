using Microondas.Aplicacao;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Microondas
{
	/// <summary>
	/// Lógica interna para CadastrarProgramas.xaml
	/// </summary>
	public partial class CadastrarProgramas : Window
	{
		public event Action<ObservableCollection<AquecimentoPreDefinido>> ProgramasCustomizados;
		private ObservableCollection<AquecimentoPreDefinido> _listaProgramas = new ObservableCollection<AquecimentoPreDefinido>();
		private AquecimentoPreDefinido _programaCustomizavel;
		private ServicoAquecimento _servicoAquecimento;

		public CadastrarProgramas()
		{
			InitializeComponent();
			_servicoAquecimento = new ServicoAquecimento();
		}

		private void ButtonSalvar_Click(object sender, RoutedEventArgs e)
		{

			//txtInstrucoes.Text = "";
			try
			{
				if (!int.TryParse(txtTempo.Text, out int tempo))
				{
					MessageBox.Show("Tempo inválido! Digite um número inteiro.");
					return;
				}

				if (!int.TryParse(txtPotencia.Text, out int potencia))
				{
					MessageBox.Show("Potência inválida! Digite um número inteiro.");
					return;
				}

				if (string.IsNullOrWhiteSpace(txtCaracter.Text) || txtCaracter.Text.Length != 1)
				{
					MessageBox.Show("O caractere deve conter exatamente 1 caractere.");
					return;
				}


				_programaCustomizavel = new AquecimentoPreDefinido(txtNome.Text, txtAlimento.Text, int.Parse(txtTempo.Text), int.Parse(txtPotencia.Text), txtCaracter.Text, txtInstrucoes.Text);

				foreach (var programa in _servicoAquecimento.AquecimentosPreDefinidos)
				{
					if (programa.CaracterAquecimento == txtCaracter.Text)
					{
						MessageBox.Show("O caracter já está sendo usado por outro programa");
						return;
					}
				}

				_listaProgramas.Add(_programaCustomizavel);


				MessageBox.Show("Programa salvo com sucesso!");
			}
			catch(Exception exc)
			{
				MessageBox.Show($"Erro ao Salvar: {exc.Message}");
			}
		}

		private void ButtonDeletar_Click(object sender, RoutedEventArgs e)
		{
			txtNome.Text = "";
			txtAlimento.Text = "";
			txtCaracter.Text = "";
			txtTempo.Text = "";
			txtPotencia.Text = "";
			txtInstrucoes.Text = "";
		}

		private void ButtonSair_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ProgramasCustomizados?.Invoke(_listaProgramas);

				this.Close();
			}
			catch (Exception exc)
			{
				MessageBox.Show($"Erro ao Sair: {exc.Message}");
			}
		}

	}
}
