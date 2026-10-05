using System;
using System.Collections.Generic;

class Program
{
    static Projetos projetos = new Projetos();

    static void Main(string[] args)
    {
        int opcao;

        do
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("     GERENCIADOR DE PROJETOS");
            Console.WriteLine("======================================");
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Adicionar projeto");
            Console.WriteLine("2. Pesquisar projeto");
            Console.WriteLine("3. Remover projeto");
            Console.WriteLine("4. Adicionar tarefa em projeto");
            Console.WriteLine("5. Concluir tarefa");
            Console.WriteLine("6. Cancelar tarefa");
            Console.WriteLine("7. Reabrir tarefa");
            Console.WriteLine("8. Listar tarefas de um projeto");
            Console.WriteLine("9. Filtrar tarefas em um projeto");
            Console.WriteLine("10. Filtrar tarefas em todos os projetos");
            Console.WriteLine("11. Resumo geral");
            Console.WriteLine("======================================");
            Console.Write("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção inválida!");
                Console.ReadKey();
                continue;
            }

            switch (opcao)
            {
                case 0:
                    Console.WriteLine("Encerrando...");
                    break;

                case 1:
                    AdicionarProjeto();
                    break;

                case 2:
                    PesquisarProjeto();
                    break;

                case 3:
                    RemoverProjeto();
                    break;

                case 4:
                    AdicionarTarefa();
                    break;

                case 5:
                    ConcluirTarefa();
                    break;

                case 6:
                    CancelarTarefa();
                    break;

                case 7:
                    ReabrirTarefa();
                    break;

                case 8:
                    ListarTarefasProjeto();
                    break;

                case 9:
                    FiltrarTarefasProjeto();
                    break;

                case 10:
                    FiltrarTarefasTodosProjetos();
                    break;

                case 11:
                    ResumoGeral();
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }

            if (opcao != 0)
            {
                Console.WriteLine();
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }

        } while (opcao != 0);
    }

    static void AdicionarProjeto()
    {
        Console.Write("ID do projeto: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Nome do projeto: ");
        string nome = Console.ReadLine();

        Projeto projeto = new Projeto(id, nome);

        if (projetos.adicionar(projeto))
            Console.WriteLine("Projeto adicionado com sucesso!");
        else
            Console.WriteLine("Não foi possível adicionar o projeto.");
    }

    static void PesquisarProjeto()
    {
        Console.Write("ID do projeto: ");
        int id = int.Parse(Console.ReadLine());

        Projeto projeto = projetos.buscar(new Projeto(id, ""));

        if (projeto == null)
        {
            Console.WriteLine("Projeto não encontrado.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Projeto: " + projeto.Nome);
        Console.WriteLine("Tarefas abertas: " + projeto.totalAbertas());
        Console.WriteLine("Tarefas fechadas: " + projeto.totalFechadas());
        Console.WriteLine("Tarefas canceladas: " + projeto.totalCanceladas());

        Console.WriteLine();
        Console.WriteLine("Tarefas abertas:");

        foreach (Tarefa tarefa in projeto.tarefasPorStatus("Aberta"))
        {
            Console.WriteLine(tarefa);
        }

        Console.WriteLine();
        Console.WriteLine("Tarefas fechadas:");

        foreach (Tarefa tarefa in projeto.tarefasPorStatus("Fechada"))
        {
            Console.WriteLine(tarefa);
        }

        Console.WriteLine();
        Console.WriteLine("Tarefas canceladas:");

        foreach (Tarefa tarefa in projeto.tarefasPorStatus("Cancelada"))
        {
            Console.WriteLine(tarefa);
        }
    }

    static void RemoverProjeto()
    {
        Console.Write("ID do projeto: ");
        int id = int.Parse(Console.ReadLine());

        Projeto projeto = projetos.buscar(new Projeto(id, ""));

        if (projeto == null)
        {
            Console.WriteLine("Projeto não encontrado.");
            return;
        }

        if (projetos.remover(projeto))
            Console.WriteLine("Projeto removido com sucesso!");
        else
            Console.WriteLine("Não é possível remover um projeto que possui tarefas.");
    }

    static void AdicionarTarefa()
    {
        Console.Write("ID do projeto: ");
        int idProjeto = int.Parse(Console.ReadLine());

        Projeto projeto = projetos.buscar(new Projeto(idProjeto, ""));

        if (projeto == null)
        {
            Console.WriteLine("Projeto não encontrado.");
            return;
        }

        Console.Write("ID da tarefa: ");
        int idTarefa = int.Parse(Console.ReadLine());

        Console.Write("Título: ");
        string titulo = Console.ReadLine();

        Console.Write("Descrição: ");
        string descricao = Console.ReadLine();

        Console.WriteLine("Prioridade:");
        Console.WriteLine("1 - Alta");
        Console.WriteLine("2 - Média");
        Console.WriteLine("3 - Baixa");
        Console.Write("Escolha: ");

        int prioridade = int.Parse(Console.ReadLine());

        if (prioridade < 1 || prioridade > 3)
        {
            Console.WriteLine("Prioridade inválida.");
            return;
        }

        Tarefa tarefa = new Tarefa(
            idTarefa,
            titulo,
            descricao,
            prioridade
        );

        projeto.adicionarTarefa(tarefa);

        Console.WriteLine("Tarefa adicionada com sucesso!");
    }

    static Tarefa BuscarTarefaDoProjeto(Projeto projeto)
    {
        Console.Write("ID da tarefa: ");
        int id = int.Parse(Console.ReadLine());

        return projeto.buscarTarefa(
            new Tarefa(id, "", "", 1)
        );
    }

    static Projeto BuscarProjeto()
    {
        Console.Write("ID do projeto: ");
        int id = int.Parse(Console.ReadLine());

        return projetos.buscar(
            new Projeto(id, "")
        );
    }

    static void ConcluirTarefa()
    {
        Projeto projeto = BuscarProjeto();

        if (projeto == null)
        {
            Console.WriteLine("Projeto não encontrado.");
            return;
        }

        Tarefa tarefa = BuscarTarefaDoProjeto(projeto);

        if (tarefa == null)
        {
            Console.WriteLine("Tarefa não encontrada.");
            return;
        }

        tarefa.concluir();

        Console.WriteLine("Tarefa concluída!");
    }

    static void CancelarTarefa()
    {
        Projeto projeto = BuscarProjeto();

        if (projeto == null)
        {
            Console.WriteLine("Projeto não encontrado.");
            return;
        }

        Tarefa tarefa = BuscarTarefaDoProjeto(projeto);

        if (tarefa == null)
        {
            Console.WriteLine("Tarefa não encontrada.");
            return;
        }

        tarefa.cancelar();

        Console.WriteLine("Tarefa cancelada!");
    }

    static void ReabrirTarefa()
    {
        Projeto projeto = BuscarProjeto();

        if (projeto == null)
        {
            Console.WriteLine("Projeto não encontrado.");
            return;
        }

        Tarefa tarefa = BuscarTarefaDoProjeto(projeto);

        if (tarefa == null)
        {
            Console.WriteLine("Tarefa não encontrada.");
            return;
        }

        tarefa.reabrir();

        Console.WriteLine("Tarefa reaberta!");
    }

    static void ListarTarefasProjeto()
    {
        Projeto projeto = BuscarProjeto();

        if (projeto == null)
        {
            Console.WriteLine("Projeto não encontrado.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Tarefas do projeto: " + projeto.Nome);
        Console.WriteLine();

        if (projeto.Tarefas.Count == 0)
        {
            Console.WriteLine("Este projeto não possui tarefas.");
            return;
        }

        foreach (Tarefa tarefa in projeto.Tarefas)
        {
            Console.WriteLine(tarefa);
        }
    }

    static void FiltrarTarefasProjeto()
    {
        Projeto projeto = BuscarProjeto();

        if (projeto == null)
        {
            Console.WriteLine("Projeto não encontrado.");
            return;
        }

        Console.WriteLine("1 - Filtrar por status");
        Console.WriteLine("2 - Filtrar por prioridade");
        Console.Write("Escolha: ");

        int opcao = int.Parse(Console.ReadLine());

        if (opcao == 1)
        {
            Console.Write("Digite o status (Aberta, Fechada ou Cancelada): ");
            string status = Console.ReadLine();

            List<Tarefa> tarefas = projeto.tarefasPorStatus(status);

            foreach (Tarefa tarefa in tarefas)
            {
                Console.WriteLine(tarefa);
            }
        }
        else if (opcao == 2)
        {
            Console.Write("Digite a prioridade (1-Alta, 2-Média, 3-Baixa): ");
            int prioridade = int.Parse(Console.ReadLine());

            List<Tarefa> tarefas = projeto.tarefasPorPrioridade(prioridade);

            foreach (Tarefa tarefa in tarefas)
            {
                Console.WriteLine(tarefa);
            }
        }
        else
        {
            Console.WriteLine("Opção inválida.");
        }
    }

    static void FiltrarTarefasTodosProjetos()
    {
        Console.WriteLine("1 - Filtrar por status");
        Console.WriteLine("2 - Filtrar por prioridade");
        Console.Write("Escolha: ");

        int opcao = int.Parse(Console.ReadLine());

        if (opcao == 1)
        {
            Console.Write("Digite o status (Aberta, Fechada ou Cancelada): ");
            string status = Console.ReadLine();

            foreach (Projeto projeto in projetos.listar())
            {
                List<Tarefa> tarefas = projeto.tarefasPorStatus(status);

                foreach (Tarefa tarefa in tarefas)
                {
                    Console.WriteLine(
                        $"Projeto: {projeto.Nome} | {tarefa}"
                    );
                }
            }
        }
        else if (opcao == 2)
        {
            Console.Write("Digite a prioridade (1-Alta, 2-Média, 3-Baixa): ");
            int prioridade = int.Parse(Console.ReadLine());

            foreach (Projeto projeto in projetos.listar())
            {
                List<Tarefa> tarefas = projeto.tarefasPorPrioridade(prioridade);

                foreach (Tarefa tarefa in tarefas)
                {
                    Console.WriteLine(
                        $"Projeto: {projeto.Nome} | {tarefa}"
                    );
                }
            }
        }
        else
        {
            Console.WriteLine("Opção inválida.");
        }
    }

    static void ResumoGeral()
    {
        int totalProjetos = projetos.listar().Count;
        int abertas = 0;
        int fechadas = 0;
        int canceladas = 0;

        foreach (Projeto projeto in projetos.listar())
        {
            abertas += projeto.totalAbertas();
            fechadas += projeto.totalFechadas();
            canceladas += projeto.totalCanceladas();
        }

        int totalTarefas = abertas + fechadas + canceladas;

        double percentual = 0;

        if (abertas > 0)
        {
            percentual = ((double)fechadas / abertas) * 100;
        }

        Console.WriteLine("======================================");
        Console.WriteLine("          RESUMO GERAL");
        Console.WriteLine("======================================");
        Console.WriteLine("Quantidade de projetos: " + totalProjetos);
        Console.WriteLine("Tarefas abertas: " + abertas);
        Console.WriteLine("Tarefas fechadas: " + fechadas);
        Console.WriteLine("Tarefas canceladas: " + canceladas);
        Console.WriteLine("Total de tarefas: " + totalTarefas);
        Console.WriteLine("Concluídas em relação às abertas: " +
                          percentual.ToString("F2") + "%");
    }
}