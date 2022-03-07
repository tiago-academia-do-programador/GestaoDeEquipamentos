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

        #region Variáveis de Chamados

        static int[] idsChamado = new int[CAPACIDADE_REGISTROS];

        static string[] titulosChamado = new string[CAPACIDADE_REGISTROS];
        static string[] descricoesChamado = new string[CAPACIDADE_REGISTROS];
        static int[] idsEquipamentoChamado = new int[CAPACIDADE_REGISTROS];

        private static int IdChamado;

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
                    string opcaoControleEquipamentos = ApresentarMenuCadastroEquipamentos();

                    if (opcaoControleEquipamentos == "s")
                        break;

                    else if (opcaoControleEquipamentos == "1")
                        InserirNovoEquipamento();

                    else if (opcaoControleEquipamentos == "2")
                        VisualizarEquipamentos(true);

                    else if (opcaoControleEquipamentos == "3")
                        EditarEquipamento();

                    else if (opcaoControleEquipamentos == "4")
                        ExcluirEquipamento();
                }
                else if (opcao == "2")
                {
                    string opcaoControleChamados = ApresentarMenuControleChamados();

                    if (opcaoControleChamados == "s")
                        break;

                    if (opcaoControleChamados == "1")
                        InserirNovoChamado();

                    else if (opcaoControleChamados == "2")
                        VisualizarChamados();

                    else if (opcaoControleChamados == "3")
                        EditarChamado();

                    else if (opcaoControleChamados == "4")
                        ExcluirChamado();
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

        #region Cadastro de Chamados

        private static string ApresentarMenuControleChamados()
        {
            Console.Clear();

            Console.WriteLine("Controle de Chamados\n");

            Console.WriteLine("Digite 1 para inserir novo chamado");
            Console.WriteLine("Digite 2 para visualizar chamados");
            Console.WriteLine("Digite 3 para editar um chamado");
            Console.WriteLine("Digite 4 para excluir um chamado");

            Console.WriteLine("Digite S para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        private static void InserirNovoChamado()
        {
            MostrarCabecalho("Controle de Chamados", "Registrando um novo chamado:");

            IdChamado++;

            GravarChamado(0);

            ApresentarMensagem("Chamado cadastrado com sucesso", ConsoleColor.Green);
        }

        private static void GravarChamado(int idChamadoSelecionado)
        {
            int qtdEquipamentos = VisualizarEquipamentos(false);

            if (qtdEquipamentos == 0)
            {
                ApresentarMensagem("Não há nenhum equipamento cadastrado para abrir um chamado.", ConsoleColor.Yellow);
                return;
            }

            int idEquipamentoChamado = ValidarIdEquipamento();

            string titulo;
            do
            {
                Console.Write("Digite o titulo do chamado: ");
                titulo = Console.ReadLine();

            } while (titulo.Length < 5);

            Console.Write("Digite a descricao do chamado: ");
            string descricao = Console.ReadLine();

            int posicao;

            if (idChamadoSelecionado == 0)
            {
                posicao = ObterPosicaoVagaParaChamados();
                idsChamado[posicao] = IdChamado;
            }
            else
                posicao = ObterPosicaoOcupadaParaChamados(idChamadoSelecionado);

            idsEquipamentoChamado[posicao] = idEquipamentoChamado;
            titulosChamado[posicao] = titulo;
            descricoesChamado[posicao] = descricao;
        }

        private static void EditarChamado()
        {
            MostrarCabecalho("Controle de Chamados", "Editando um chamado:");

            int qtdChamadosCadastrados = VisualizarChamados();

            if (qtdChamadosCadastrados == 0)
                return;

            Console.WriteLine();

            bool numeroEncontrado;
            do
            {
                Console.Write("Digite o número do chamado que deseja editar: ");
                int idSelecionado = Convert.ToInt32(Console.ReadLine());

                numeroEncontrado = ExisteChamado(idSelecionado);

                if (numeroEncontrado)
                    GravarChamado(idSelecionado);
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Número não encontrado, tente novamente");
                    Console.ResetColor();
                }

            } while (numeroEncontrado == false);

            ApresentarMensagem("Chamado editado com sucesso", ConsoleColor.Green);
        }

        private static void ExcluirChamado()
        {
            MostrarCabecalho("Controle de Chamados", "Excluindo um chamado:");

            VisualizarChamados();

            Console.WriteLine();

            Console.Write("Digite o número do chamado que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < idsEquipamento.Length; i++)
            {
                if (idsChamado[i] == idSelecionado)
                {
                    idsChamado[i] = 0;
                    idsEquipamentoChamado[i] = 0;
                    titulosChamado[i] = null;
                    descricoesChamado[i] = null;

                    break;
                }
            }
        }

        private static int VisualizarChamados()
        {
            MostrarCabecalho("Controle de Chamados", "Visualizando chamados:");

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("{0,-10} | {1,-30} | {2,-55}", "Id", "Equipamento", "Título");

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

            Console.ResetColor();

            int numeroChamadosRegistrados = 0;

            for (int indiceChamados = 0; indiceChamados < idsChamado.Length; indiceChamados++)
            {
                if (idsChamado[indiceChamados] > 0)
                {
                    string nomeEquipamento = "";

                    for (int indiceEquipamentos = 0; indiceEquipamentos < idsEquipamento.Length; indiceEquipamentos++)
                    {
                        if (idsEquipamento[indiceEquipamentos] == idsEquipamentoChamado[indiceChamados])
                        {
                            nomeEquipamento = nomesEquipamento[indiceEquipamentos];
                            break;
                        }
                    }

                    Console.Write("{0,-10} | {1,-30} | {2,-55}",
                       idsChamado[indiceChamados],
                       nomeEquipamento,
                       titulosChamado[indiceChamados]);

                    numeroChamadosRegistrados++;
                }
            }

            if (numeroChamadosRegistrados == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Nenhum chamado registrado!");
                Console.ResetColor();
            }

            Console.ReadLine();

            return numeroChamadosRegistrados;
        }

        private static int ValidarIdEquipamento()
        {
            int idEquipamentoChamado;
            bool equipamentoExiste;
            do
            {
                Console.Write("Digite o Id do equipamento para manutenção: ");
                idEquipamentoChamado = Convert.ToInt32(Console.ReadLine());

                equipamentoExiste = ExisteEquipamento(idEquipamentoChamado);

                if (!equipamentoExiste)
                    ApresentarMensagem("O equipamento com o Id informado não existe! Digite um Id válido.", ConsoleColor.Red);

            } while (!equipamentoExiste);
            return idEquipamentoChamado;
        }

        private static int ObterPosicaoVagaParaChamados()
        {
            int posicao = 0;

            for (int i = 0; i < idsChamado.Length; i++)
            {
                if (idsChamado[i] == 0) //inserindo:
                {
                    posicao = i;
                    break;
                }
            }

            return posicao;
        }

        private static int ObterPosicaoOcupadaParaChamados(int idChamadoSelecionado)
        {
            int posicao = 0;

            for (int i = 0; i < idsChamado.Length; i++)
            {
                if (idChamadoSelecionado == idsChamado[i]) //editando:
                {
                    posicao = i;
                    break;
                }
            }

            return posicao;
        }

        private static bool ExisteChamado(int idSelecionado)
        {
            for (int i = 0; i < idsChamado.Length; i++)
            {
                if (idsChamado[i] == idSelecionado)
                    return true;
            }

            return false;
        }

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
