namespace Cadastro;

public class Produto
{

    private int Id;
    public string Descricao { get; set; }

    //public readonly int Estoque;
    public int Estoque { get; }

    public Produto()
    {
        Estoque = 1;
    }

    public void ImprimirDescricao()
    {
        Console.WriteLine($"{GetId()} - {Descricao}");
    }

    public void SetId(int id)
    {
        Id = id;
    }

    public int GetId()
    {
        return Id;
    }
}

// Classe estatica
public static class Calculos
{
    public static int SomarNumeros(int num1, int num2)
    {
        return num1 + num2;
    }
}

public class Pessoa
{
    public int Id { get; set; }
    public string Endereco { get; set; }
    public string Cidade { get; set; }
    public string Cep { get; set; }

    public void ImprimirDados()
    {
        Console.WriteLine($"Codigo - {Id}");
        Console.WriteLine($"Endereco - {Endereco}");
        Console.WriteLine($"Cidade - {Cidade}");
        Console.WriteLine($"Cep - {Cep}");
    }
}

public class PessoaFisica : Pessoa
{
    public string CPF { get; set; }

    public void ImprimirCpf()
    {
        Console.WriteLine($"CPF - {CPF}");
    }
}

public class Funcionario : PessoaFisica
{
    public string Matricula { get; set; }
}


// classes seladas é um recurso do C# para impedir que outra classe herde as implementações da classe selada
public sealed class Configuracao
{
    public string Host { get; set; }
}

public abstract class Animal
{
    public string Nome { get; set; }

    public abstract string GetInformacoes();

    public void ImprimirDados()
    {
        Console.WriteLine($"Nome animal: {Nome}");
        Console.WriteLine($"Informações: {GetInformacoes()}");
    }
}

public class Cachorro : Animal
{
    public override string GetInformacoes()
    {
        return "Cachorro é carnívoro";
    }
}

// class record 1
// public record class Curso
// {
//     public int Id { get; set; }
//     public string Descricao { get; set; }

//     // public override bool Equals(object? obj)
//     // {
//     //     if (obj == null) return false;

//     //     if(obj is Curso curso)
//     //     {
//     //         return Id == curso.Id && Descricao == curso.Descricao;
//     //     }

//     //     return base.Equals(obj);
//     // }

//     // public static bool operator == (Curso a, Curso b)
//     // {
//     //     return a.Equals(b);
//     // }

//     // public static bool operator != (Curso a, Curso b)
//     // {
//     //     return !(a == b);
//     // }
// }

// class record 2
public record Curso(int Id, string Descricao);

public class CursoTeste
{
    public int Id { get; set; }
    public string Descricao { get; set; }
}

public interface INotificacao
{
    string Descricao { get; set; }
    void Notificar();
}

public class NotificacaoCliente : INotificacao
{
    public string Descricao { get; set; }

    public void Notificar()
    {
        Console.WriteLine($"Notificando o cliente");
    }

    public void NotificarOutros()
    {
        Console.WriteLine($"Notificando o outros");
    }
}

public class NotificacaoFuncionario : INotificacao
{
    public string Descricao { get; set; }

    public void Notificar()
    {
        Console.WriteLine($"Notificando o funcionario");
    }

    public void NotificarOutros()
    {
        Console.WriteLine($"Notificando o outros");
    }
}