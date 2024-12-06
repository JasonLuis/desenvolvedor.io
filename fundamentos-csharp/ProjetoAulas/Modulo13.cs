namespace Modulo13;

public class TrabalhandoComArquivos 
{
    public void AulaCriandoArquivo()
    {
        var escrever = new StreamWriter("arquivo.txt", true);
        Console.Write("Informe um nome: ");
        var nome = Console.ReadLine();

        escrever.WriteLine($"ID...: {Random.Shared.Next(1, 100)}");
        escrever.WriteLine($"Nome.: {nome}");
        escrever.WriteLine($"------------------------");
        escrever.Close();
    }

    public void AulaLendoArquivo()
    {
        // var conteudo = File.ReadAllText("arquivo.txt");
        // Console.WriteLine(conteudo);

        var ler = new StreamReader("arquivo.txt");
        while (!ler.EndOfStream)
        {
            var linha = ler.ReadLine();
            Console.WriteLine(linha);
        }

        ler.Close();
    }

    public void AulaExcluindoArquivo()
    {
       if(File.Exists("arquivo.txt"))
       {
         File.Delete("arquivo.txt");
       }
    }
}