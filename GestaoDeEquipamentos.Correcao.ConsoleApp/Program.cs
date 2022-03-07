using System;

namespace GestaoDeEquipamentos.Correcao.ConsoleApp
{
    internal class Program
    {
        const int CAPACIDADE_REGISTROS = 100;

        #region Variáveis de Equipamentos

        static int[] idsEquipamento = new int[CAPACIDADE_REGISTROS];

        static string[] nomesEquipamento = new string[CAPACIDADE_REGISTROS];
        static double[] precosEquipamento = new double[CAPACIDADE_REGISTROS];
        static string[] numerosSerieEquipamento = new string[CAPACIDADE_REGISTROS];
        static DateTime[] datasFabricacaoEquipamento = new DateTime[CAPACIDADE_REGISTROS];
        static string[] fabricantesEquipamento = new string[CAPACIDADE_REGISTROS];

        private static int IdEquipamento;

        #endregion

        static void Main(string[] args)
        {
            while (true)
            {
                string opcao = ApresentarMenuPrincipal();

                if (opcao == "s" || opcao == "S")
                    break;

                if (opcao == "1")
                {
                    string opcaoCadastroEquipamentos = ApresentarMenuCadastroEquipamentos();

                    if (opcaoCadastroEquipamentos == "s")
                        break;

                    else if (opcaoCadastroEquipamentos == "1")
                        InserirNovoEquipamento();

                    else if (opcaoCadastroEquipamentos == "2")
                        VisualizarEquipamentos(true);

                    else if (opcaoCadastroEquipamentos == "3")
                        EditarEquipamento();

                    else if (opcaoCadastroEquipamentos == "4")
                        ExcluirEquipamento();
                }
            }
        }

        #region Cadastro de Equipamentos

        private static string ApresentarMenuCadastroEquipamentos()
        {
            Console.Clear();

            Console.WriteLine("Gestão de Equipamentos\n");

            Console.WriteLine("Digite 1 para inserir novo equipamento");
            Console.WriteLine("Digite 2 para visualizar equipamentos");
            Console.WriteLine("Digite 3 para editar um equipamento");
            Console.WriteLine("Digite 4 para excluir um equipamento");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        private static void InserirNovoEquipamento()
        {
            MostrarCabecalho("Cadastro de Equipamentos", "Registrando um novo equipamento:");

            IdEquipamento++;

            GravarEquipamento(0);

            ApresentarMensagem("Equipamento cadastrado com sucesso", ConsoleColor.Green);
        }

        private static void GravarEquipamento(int idEquipamentoSelecionado)
        {
            string nome = ObterNomeEquipamento();

            double preco = ObterPrecoEquipamento();

            string numeroSerie = ObterNumeroSerieEquipamento();

            DateTime dataFabricacao = ObterDataFabricacaoEquipamento();

            string fabricante = ObterFabricanteEquipamento();

            int posicao;

            if (idEquipamentoSelecionado == 0)
            {
                posicao = ObterPosicaoVagaParaEquipamentos();
                idsEquipamento[posicao] = IdEquipamento;
            }
            else
                posicao = ObterPosicaoOcupadaParaEquipamentos(idEquipamentoSelecionado);

            nomesEquipamento[posicao] = nome;
            precosEquipamento[posicao] = preco;
            numerosSerieEquipamento[posicao] = numeroSerie;
            datasFabricacaoEquipamento[posicao] = dataFabricacao;
            fabricantesEquipamento[posicao] = fabricante;
        }

        private static void EditarEquipamento()
        {
            MostrarCabecalho("Cadastro de Equipamentos", "Editando um equipamento:");

            int quantidadeEquipamentosCadastrados = VisualizarEquipamentos(false);

            if (quantidadeEquipamentosCadastrados == 0)
            {
                ApresentarMensagem("Não há nenhum equipamento disponível para editar.", ConsoleColor.DarkYellow);
                return;
            }

            Console.WriteLine();

            bool equipamentoEncontrado;
            do
            {
                Console.Write("Digite o número do equipamento que deseja editar: ");
                int idSelecionado = Convert.ToInt32(Console.ReadLine());

                equipamentoEncontrado = ExisteEquipamento(idSelecionado);

                if (equipamentoEncontrado)
                    GravarEquipamento(idSelecionado);
                else
                    ApresentarMensagem("Equipamento não encontrado. Verifique se informou um ID válido.", ConsoleColor.DarkYellow);

            } while (equipamentoEncontrado == false);

            ApresentarMensagem("Equipamento editado com sucesso", ConsoleColor.Green);
        }

        private static void ExcluirEquipamento()
        {
            MostrarCabecalho("Cadastro de Equipamentos", "Excluindo um equipamento:");

            int quantidadeEquipamentosCadastrados = VisualizarEquipamentos(false);

            if (quantidadeEquipamentosCadastrados == 0)
            {
                ApresentarMensagem("Não há nenhum equipamento disponível para excluir.", ConsoleColor.DarkYellow);
                return;
            }

            Console.WriteLine();

            bool numeroEncontrado;
            do
            {
                Console.Write("Digite o número do equipamento que deseja excluir: ");
                int idSelecionado = Convert.ToInt32(Console.ReadLine());

                numeroEncontrado = ExisteEquipamento(idSelecionado);

                if (numeroEncontrado)
                {
                    for (int i = 0; i < idsEquipamento.Length; i++)
                    {
                        if (idsEquipamento[i] == idSelecionado)
                        {
                            idsEquipamento[i] = 0;
                            nomesEquipamento[i] = null;
                            precosEquipamento[i] = 0;
                            numerosSerieEquipamento[i] = null;
                            datasFabricacaoEquipamento[i] = DateTime.MinValue;
                            fabricantesEquipamento[i] = null;

                            break;
                        }
                    }
                }
                else
                    ApresentarMensagem("Equipamento não encontrado. Verifique se informou um ID válido.", ConsoleColor.DarkYellow);

            } while (numeroEncontrado == false);

            ApresentarMensagem("Equipamento excluído com sucesso", ConsoleColor.Green);
        }

        private static int VisualizarEquipamentos(bool mostrarCabecalho)
        {
            if (mostrarCabecalho)
                MostrarCabecalho("Cadastro de Equipamentos", "Visualizando equipamentos:");

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("{0,-10} | {1,-55} | {2,-35}", "Id", "Nome", "Fabricante");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();

            int numeroEquipamentosCadastrados = 0;

            for (int i = 0; i < idsEquipamento.Length; i++)
            {
                if (idsEquipamento[i] > 0)
                {
                    Console.Write("{0,-10} | {1,-55} | {2,-35}", idsEquipamento[i], nomesEquipamento[i], fabricantesEquipamento[i]);

                    Console.WriteLine();

                    numeroEquipamentosCadastrados++;
                }
            }

            if (numeroEquipamentosCadastrados == 0)
                ApresentarMensagem("Nenhum equipamento cadastrado!", ConsoleColor.DarkYellow);
            else
                Console.ReadLine();

            return numeroEquipamentosCadastrados;
        }


        private static int ObterPosicaoVagaParaEquipamentos()
        {
            int posicao = 0;

            for (int i = 0; i < idsEquipamento.Length; i++)
            {
                if (idsEquipamento[i] == 0)
                {
                    posicao = i;
                    break;
                }
            }

            return posicao;
        }

        private static int ObterPosicaoOcupadaParaEquipamentos(int idEquipamentoSelecionado)
        {
            int posicao = 0;

            for (int i = 0; i < idsEquipamento.Length; i++)
            {
                if (idEquipamentoSelecionado == idsEquipamento[i])
                {
                    posicao = i;
                    break;
                }
            }

            return posicao;
        }

        private static bool ExisteEquipamento(int idSelecionado)
        {
            for (int i = 0; i < idsEquipamento.Length; i++)
            {
                if (idsEquipamento[i] == idSelecionado)
                    return true;
            }

            return false;
        }

        #region Validações de Cadastro de Equipamentos

        private static string ObterNomeEquipamento()
        {
            string nome;

            bool nomeInvalido;
            do
            {
                nomeInvalido = false;
                Console.Write("Digite o nome do equipamento: ");
                nome = Console.ReadLine();

                if (nome.Length < 3)
                {
                    nomeInvalido = true;
                    ApresentarMensagem("Nome inválido. No mínimo 3 caracteres", ConsoleColor.Red);
                }

            } while (nomeInvalido);

            return nome;
        }

        private static double ObterPrecoEquipamento()
        {
            double preco;

            bool precoValido;
            do
            {
                Console.Write("Digite o preço do equipamento: ");
                precoValido = Double.TryParse(Console.ReadLine(), out preco);

                if (PrecoEstaInvalido(preco, precoValido))
                    ApresentarMensagem("Preço inválido. Por favor digite um valor numérico positivo.", ConsoleColor.Red);

            } while (!precoValido);

            return preco;
        }

        private static bool PrecoEstaInvalido(double preco, bool precoValido)
        {
            return !precoValido || preco <= 0;
        }

        private static string ObterNumeroSerieEquipamento()
        {
            string numeroSerie;

            bool numeroInvalido;
            do
            {
                numeroInvalido = false;
                Console.Write("Digite o número de série do equipamento: ");
                numeroSerie = Console.ReadLine().ToUpper();

                if (TamanhoNumeroEstaInvalido(numeroSerie))
                {
                    ApresentarMensagem("Numero de série inválido. O numero não pode ser vazio.", ConsoleColor.Red);
                    numeroInvalido = true;
                }
                else
                {
                    for (int i = 0; i < numeroSerie.Length; i++)
                    {
                        if (NumeroSerieJaExiste(numeroSerie, i))
                        {
                            ApresentarMensagem("Numero de série inválido. Digite um número de série único.", ConsoleColor.Red);
                            numeroInvalido = true;
                            break;
                        }
                    }
                }

            } while (numeroInvalido);

            return numeroSerie;
        }

        private static bool NumeroSerieJaExiste(string numeroSerie, int i)
        {
            return numeroSerie == numerosSerieEquipamento[i];
        }

        private static bool TamanhoNumeroEstaInvalido(string numeroSerie)
        {
            return numeroSerie.Length == 0;
        }

        private static DateTime ObterDataFabricacaoEquipamento()
        {
            DateTime dataFabricacao;
            bool dataValida;
            do
            {
                Console.Write("Digite a data de fabricação do equipamento: ");
                dataValida = DateTime.TryParse(Console.ReadLine(), out dataFabricacao);

                if (DataDeFabricacaoExcedeDiaDeHoje(dataFabricacao))
                {
                    dataValida = false;
                    ApresentarMensagem("Data de fabricação não pode ser maior que hoje.", ConsoleColor.Red);
                }

                if (!dataValida)
                    ApresentarMensagem("Data inválida. Digite uma data válida no formato 'dd/MM/aaaa'.", ConsoleColor.Red);

            } while (!dataValida);

            return dataFabricacao;
        }

        private static bool DataDeFabricacaoExcedeDiaDeHoje(DateTime dataFabricacao)
        {
            return dataFabricacao > DateTime.Today;
        }

        private static string ObterFabricanteEquipamento()
        {
            string fabricante;

            bool fabricanteInvalido;
            do
            {
                fabricanteInvalido = false;
                Console.Write("Digite o fabricante do equipamento: ");
                fabricante = Console.ReadLine();

                if (TamanhoFabricanteEstaInvalido(fabricante))
                {
                    ApresentarMensagem("Fabricante inválido. Por favor insira um fabricante.", ConsoleColor.Red);

                    fabricanteInvalido = true;
                }

            } while (fabricanteInvalido);

            return fabricante;
        }

        private static bool TamanhoFabricanteEstaInvalido(string fabricante)
        {
            return fabricante.Length == 0;
        }
        #endregion

        #endregion

        #region Métodos de uso geral

        private static void MostrarCabecalho(string titulo, string subtitulo)
        {
            Console.Clear();
            Console.WriteLine(titulo);
            Console.WriteLine();
            Console.WriteLine(subtitulo);
            Console.WriteLine();
        }

        private static string ApresentarMenuPrincipal()
        {
            Console.Clear();

            Console.WriteLine("Gestão de Equipamentos\n");

            Console.WriteLine("Digite 1 para o Cadastro de Equipamentos");
            Console.WriteLine("Digite 2 para o Controle de Chamados");

            Console.WriteLine("Digite s para Sair");

            string opcao = Console.ReadLine();
            return opcao;
        }

        private static void ApresentarMensagem(string mensagem, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;
            Console.WriteLine(mensagem);
            Console.ResetColor();

            Console.ReadLine();
        }

        #endregion
    }
}
