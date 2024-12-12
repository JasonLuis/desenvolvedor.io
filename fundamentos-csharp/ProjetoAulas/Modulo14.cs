namespace Modulo14;

public class TrabalhandoComLinq 
{
    public void AulaWhere()
    {
        // string nomeCompleto = "Fulano de Tal";

        // Func<char, bool> filtro = c => c == 'a';
        
        // //var resultado = nomeCompleto.Where(filtro).ToList();
        // //var resultado = nomeCompleto.Where(p => p == 'a').ToList();

        // var resultado = from c in  nomeCompleto where c == 'a' select c;

        // foreach (var item in resultado)
        // {
        //     Console.WriteLine(item);
        // }

        var numeros = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var resultado = numeros.Where(p => p >= 10);

        foreach (var item in resultado)
        {
            Console.WriteLine(item);
        }

    }

    public void AulaOrdenacao()
    {
        
        //var numeros = new int[] { 1, 222, 30, 41, 15, 26, 17, 8, 19, 10 };
        //var resultado = numeros.OrderByDescending(p => p);

        var nomes = new string[] { "Jason", "Bruno", "Victor", "Fernanda" };
        //var resultado = nomes.OrderBy(p => p);
        var resultado = nomes.OrderByDescending(p => p);

        foreach (var item in resultado)
        {
            Console.WriteLine(item);
        }

    }

    public void AulaTake()
    { 
        var numeros = new int[] { 1, 222, 30, 41, 15, 26, 17, 8, 19, 10 };
        
        var resultado = numeros.Where(p => p > 10).Take(3).OrderBy(p => p);

        foreach (var item in resultado)
        {
            Console.WriteLine(item);
        }
    }

    public void AulaCount()
    {    
        var numeros = new int[] { 1, 222, 30, 41, 15, 26, 17, 8, 19, 10 };
        
        var resultado = numeros.Count(r => r > 10);

        Console.WriteLine(resultado);
    }

    public void AulaFirstEFirstOrDefault()
    {    
        var numeros = new int[] { 1, 222, 30, 41, 15, 26, 17, 8, 19, 10 };
        
        //var resultado = numeros.First();
        //var resultado = numeros.First(r => r > 15);
        var resultado = numeros.FirstOrDefault(p => p > 1000, -1);

        // single ou singleOrDefault -> retorna um unico item e gera uma execeção caso ocorra duplicidade

        Console.WriteLine(resultado);

        // Average => em uma lista de numeros, o método Average retorna o valor medio
        Console.WriteLine($"Average {numeros.Average()}");
    }
}