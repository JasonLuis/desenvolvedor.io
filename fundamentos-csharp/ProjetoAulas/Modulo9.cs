namespace Conversores;

public class Conversor
{
    public void ConvertandParse()
    {
        //int numero = int.Parse("1");
        int numero = Convert.ToInt32("1");
        int nullParaZero = Convert.ToInt32(null);

        Console.WriteLine(numero);
        Console.WriteLine($"null para zero -> {nullParaZero}");

        bool verdadeiro = bool.Parse("true");
        bool falso = Convert.ToBoolean(1);
        Console.WriteLine($"verdadeiro -> {verdadeiro} | falso -> {falso}");
    }

    public void AulaTryParse()
    {
        var numero = "abc";

        if (int.TryParse(numero, out int numeroConvertido))
        {
            Console.Write($"Numero convertido");
        }

        Console.WriteLine($"Numero convertido para -> {numeroConvertido}");
    }
}