namespace Modulo10;

public class TrabalhandoComStrings
{
    public void ConverterParaLetrasMinusculas()
    {
        Console.WriteLine("Favor digiar alguma informação: ");
        var linha = Console.ReadLine();

        Console.WriteLine(linha.ToLower());
    }


    public void ConverterParaLetrasMaisculas()
    {
        Console.WriteLine("Favor digiar alguma informação: ");
        var linha = Console.ReadLine();

        Console.WriteLine(linha.ToUpper());
    }

    public void AulaSubString()
    {
        Console.WriteLine("Favor digiar alguma informação: ");
        var linha = Console.ReadLine();

        Console.WriteLine(linha.Substring(1,6));
    }

    public void AulaRange()
    {
        var nomeArquivo = "2024-03-12_backup.txt";
        string ano = nomeArquivo[..4];
        Console.WriteLine(ano);

        string extensao = nomeArquivo[^3..];
        Console.WriteLine(extensao);

        string nome = nomeArquivo[11..^4];
        Console.WriteLine(nome);

        string apenasNome = nomeArquivo[..^4];
        Console.WriteLine(apenasNome);
    }

    public void AulaContains()
    {
        var nomeArquivo = "2024-03-12_backup.txt";
        if(nomeArquivo.Contains("backup"))
        {
            Console.WriteLine($"Palavra encontrada");
        } 
        else 
        {
            Console.WriteLine($"Palavra nao encontrada");
        }
        //Console.WriteLine($"Contem nome: {nomeArquivo.Contains("backup")}");
    }

    public void AulaTrim()
    {
        var teste = "**JASON LUIS**";

        Console.WriteLine($"TOTAL: {teste.Trim('*')}");
        Console.WriteLine($"INICIO: {teste.TrimStart('*')}");
        Console.WriteLine($"FINAL: {teste.TrimEnd('*')}");
    }

    public void AulaStartWithEndWith()
    {
        var teste = "Curso Csharp";

        Console.WriteLine($"INICIO: {teste.StartsWith("Curso")}");
        Console.WriteLine($"FINAL: {teste.EndsWith("Csharp")}");
    }

    public void AulaReplace()
    {
        var teste = "Curso Csharp";
        Console.WriteLine(teste);
        Console.WriteLine(teste.Replace("Csharp", "C#"));
    }

    public void AulaLength()
    {
        Console.Write("Favor digiar alguma informação: ");
        var teste = Console.ReadLine();
         Console.WriteLine(teste);
        Console.WriteLine(teste.Length);
    }
}