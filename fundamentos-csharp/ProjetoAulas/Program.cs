// estrutura de um projeto nas versões anteriores do .net (versão .net 5.0 ou inferio) sem o uso do Top Level Statement
// namemespace -> serve para organizar a estrutura do projeto

// Top-level statements, ou instruções de nível superior, são um recurso do C# 9 que permite definir instruções em um arquivo sem criar uma classe ou o método Main


using System;

namespace Application {
    class  Program {
        static void Main(string[] args) {
            //Console.WriteLine("Hello, World!");
            AulaClasses();
        }

        private static void AulaClasses() {
            var resultado = Cadastro.Calculos.SomarNumeros(10, 20);
            Console.WriteLine(resultado);

            // var produto = new Cadastro.Produto();
            // produto.SetId(1);
            // produto.Descricao = "Teclado mecanico";
            // produto.ImprimirDescricao();
        }
    }
}


// namespace Cadastro
// {
//     class Cliente
//     {

//     }

//     class Funcionario
//     {

//     }
// }

// namespace Financeiro
// {
//     class ContasReceber
//     {

//     }

//     class Funcionario
//     {

//     }
// }