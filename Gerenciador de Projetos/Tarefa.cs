using System;
using System.Collections.Generic;
using System.Linq;

public class Tarefa
{
    private int id;
    private string titulo;
    private string descricao;
    private int prioridade;
    private string status;
    private DateTime dataCriacao;
    private DateTime? dataConclusao;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Titulo
    {
        get { return titulo; }
        set { titulo = value; }
    }

    public string Descricao
    {
        get { return descricao; }
        set { descricao = value; }
    }

    public int Prioridade
    {
        get { return prioridade; }
        set { prioridade = value; }
    }

    public string Status
    {
        get { return status; }
        set { status = value; }
    }

    public DateTime DataCriacao
    {
        get { return dataCriacao; }
        set { dataCriacao = value; }
    }

    public DateTime? DataConclusao
    {
        get { return dataConclusao; }
        set { dataConclusao = value; }
    }

    public Tarefa(int id, string titulo, string descricao, int prioridade)
    {
        this.id = id;
        this.titulo = titulo;
        this.descricao = descricao;
        this.prioridade = prioridade;
        this.status = "Aberta";
        this.dataCriacao = DateTime.Now;
        this.dataConclusao = null;
    }

    public void concluir()
    {
        if (status == "Aberta")
        {
            status = "Fechada";
            dataConclusao = DateTime.Now;
        }
    }

    public void cancelar()
    {
        if (status == "Aberta")
        {
            status = "Cancelada";
            dataConclusao = DateTime.Now;
        }
    }

    public void reabrir()
    {
        if (status == "Fechada" || status == "Cancelada")
        {
            status = "Aberta";
            dataConclusao = null;
        }
    }

    public override string ToString()
    {
        return $"ID: {id} | Título: {titulo} | Prioridade: {prioridade} | Status: {status}";
    }
}