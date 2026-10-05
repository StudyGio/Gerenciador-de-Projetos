using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_de_Projetos
using System.Collections.Generic;
using System.Linq;

public class Projeto
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public List<Tarefa> Tarefas { get; private set; }

    public Projeto(int id, string nome)
    {
        Id = id;
        Nome = nome;
        Tarefas = new List<Tarefa>();
    }

    public void AdicionarTarefa(Tarefa t)
    {
        Tarefas.Add(t);
    }

    public bool RemoverTarefa(Tarefa t)
    {
        return Tarefas.Remove(t);
    }

    // Busca pela tarefa (compara pelo Id)
    public Tarefa BuscarTarefa(Tarefa t)
    {
        return Tarefas.FirstOrDefault(x => x.Id == t.Id);
    }

    public List<Tarefa> TarefasPorStatus(string s)
    {
        return Tarefas
            .Where(t => t.Status.Equals(s, System.StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Tarefa> TarefasPorPrioridade(int p)
    {
        return Tarefas.Where(t => t.Prioridade == p).ToList();
    }

    public int TotalAbertas()
    {
        return Tarefas.Count(t => t.Status == "Aberta");
    }

    public int TotalFechadas()
    {
        return Tarefas.Count(t => t.Status == "Fechada");
    }

    public int TotalCanceladas()
    {
        return Tarefas.Count(t => t.Status == "Cancelada");
    }

    public override string ToString()
    {
        return $"[{Id}] {Nome} ({Tarefas.Count} tarefa(s))";
    }
}
