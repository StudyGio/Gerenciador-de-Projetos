using System;
using System.Collections.Generic;
using System.Linq;

public class Projetos
{
    private List<Projeto> itens;

    public Projetos()
    {
        itens = new List<Projeto>();
    }

    public bool adicionar(Projeto p)
    {
        if (p == null)
            return false;

        if (buscar(p) != null)
            return false;

        itens.Add(p);
        return true;
    }

    public bool remover(Projeto p)
    {
        if (p == null)
            return false;

        Projeto projeto = buscar(p);

        if (projeto == null)
            return false;

        if (projeto.Tarefas.Count > 0)
            return false;

        return itens.Remove(projeto);
    }

    public Projeto buscar(Projeto p)
    {
        if (p == null)
            return null;

        return itens.FirstOrDefault(x => x.Id == p.Id);
    }

    public List<Projeto> listar()
    {
        return new List<Projeto>(itens);
    }
}