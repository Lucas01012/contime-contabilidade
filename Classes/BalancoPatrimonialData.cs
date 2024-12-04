using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConTime.Classes;
public class BalancoPatrimonialData
{
    public List<Conta> Ativos { get; set; }
    public List<Conta> Passivos { get; set; }
    public List<Conta> Patrimonio { get; set; }
    public float TotalAtivos { get; set; }
    public float TotalPassivos { get; set; }
    public float TotalPatrimonio { get; set; }

    public BalancoPatrimonialData()
    {
        Ativos = new List<Conta>();
        Passivos = new List<Conta>();
        Patrimonio = new List<Conta>();
    }

    public void AdicionarDados(string categoria, string conta, float saldo)
    {
        switch (categoria)
        {
            case "Ativos":
                Ativos.Add(new Conta { Nome = conta, Saldo = saldo });
                break;
            case "Passivos":
                Passivos.Add(new Conta { Nome = conta, Saldo = saldo });
                break;
            case "Patrimonio":
                Patrimonio.Add(new Conta { Nome = conta, Saldo = saldo });
                break;
        }
    }

    public void CalcularTotais()
    {
        TotalAtivos = Ativos.Sum(a => a.Saldo);
        TotalPassivos = Passivos.Sum(p => p.Saldo);
        TotalPatrimonio = Patrimonio.Sum(p => p.Saldo);
    }
}

public class Conta
{
    public string Nome { get; set; }
    public float Saldo { get; set; }
}
