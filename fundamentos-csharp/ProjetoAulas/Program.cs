// estrutura de um projeto nas versões anteriores do .net (versão .net 5.0 ou inferio) sem o uso do Top Level Statement
// namemespace -> serve para organizar a estrutura do projeto

// Top-level statements, ou instruções de nível superior, são um recurso do C# 9 que permite definir instruções em um arquivo sem criar uma classe ou o método Main


using System;
using Cadastro;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            //AulaClasses();
            //AulaPropriedadeSomenteLeitura();
            //AulaHeranca();
            //AulaClasseSelada();
            //AulaClasseAbstrata();
            //AulaRecord();
            //AulaInterface();
            //Conversores();
            //TrabalhandoComStrings();
            //TrabalhandoComDatas();
            TrabalhandoComExcecoes();
        }


        private static void TrabalhandoComExcecoes() {
            var trabalhandoComExcecoes = new Modulo12.TrabalhandoComExcecoes();
            //trabalhandoComExcecoes.AulaGerandoException();
            trabalhandoComExcecoes.AulaTratandoException();
        }

        private static void TrabalhandoComDatas() {
            var trabalhandoComDatas = new Modulo11.TrabalhandoComDatas();
            //trabalhandoComDatas.AulaDateTime();
            //trabalhandoComDatas.AulaSubtraindoDatas();
            //trabalhandoComDatas.AulaAdicionandoDiasMesAno();
            //trabalhandoComDatas.AulaAdicionandoHoraMinutoSegundo();
            //trabalhandoComDatas.AulaDiaDaSemana();
            //trabalhandoComDatas.AulaDateOnly();
            trabalhandoComDatas.AulaTimeOnly();
        }

        public static void TrabalhandoComStrings()
        {
            var trabalhandoComStrings = new Modulo10.TrabalhandoComStrings();
            //trabalhandoComStrings.ConverterParaLetrasMinusculas();
            //trabalhandoComStrings.ConverterParaLetrasMaisculas();
            //trabalhandoComStrings.AulaSubString();
            //trabalhandoComStrings.AulaRange();
            //trabalhandoComStrings.AulaContains();
            //trabalhandoComStrings.AulaTrim();
            //trabalhandoComStrings.AulaStartWithEndWith();
            //trabalhandoComStrings.AulaReplace();
            trabalhandoComStrings.AulaLength();
        }

        private static void Conversores() {
            var conversor = new Conversores.Conversor();
            //conversor.ConvertandParse();
            conversor.AulaTryParse();
        }

        private static void AulaInterface(){
            var notificacaoCliente = new Cadastro.NotificacaoCliente();
            notificacaoCliente.Notificar();
            notificacaoCliente.NotificarOutros();

            Cadastro.INotificacao notificacao = new Cadastro.NotificacaoFuncionario();
            notificacao.Notificar();
        }

        private static void AulaRecord()
        {

            // record 1
            // var curso1 = new Cadastro.Curso { Id = 1, Descricao = "Curso 1"};
            // var curso2 = new Cadastro.Curso { Id = 1, Descricao = "Curso 1"};

            

            //Console.WriteLine(curso1.Equals(curso2)); // -> recebe true
            //Console.WriteLine(curso1 == curso2); // -> recebe false

            // var curso1 = new Cadastro.CursoTeste
            // {
            //     Id = 1,
            //     Descricao = "Curso"
            // };

            // quando passo o valor de um objeto de classe comum para outro objeto de classe comum, ambos utilizam o mesmo endereço de memória. Dessa maneira, quando alterar a propriedade Descrição de curso2, a propriedade Descrição de curso1 também será alterada.
            // var curso2 = curso1;
            // curso2.Descricao = "Curso 2";

            // record 2
            // Diferente de uma class comum, a class record cria um novo endereço de memória para o objeto, assim, quando alterar a propriedade Descrição de curso2, a propriedade Descrição de curso1 não será alterada.
            var curso1 = new Cadastro.Curso(1, "Curso 1");
            var curso2 = curso1 with {Descricao = "Teste Novo"};


            //var curso2 = new Cadastro.CursoTeste();
            //curso2.Id = curso1.Id;
            //curso2.Descricao = "Nova descricao";

            Console.WriteLine(curso1.Descricao);
            Console.WriteLine(curso2.Descricao);
        }

        
        
        private static void AulaClasseAbstrata()
        {
            var cachorro = new Cadastro.Cachorro();
            cachorro.Nome = "Dog";
            cachorro.ImprimirDados();
        }

        private static void AulaClasseSelada()
        {
            // var configuracao = new Cadastro.Configuracao();
            // configuracao.Host = "localhost";

            var configuracao = new Cadastro.Configuracao
            {
                Host = "localhost"
            };

            Console.WriteLine(configuracao.Host);
        }

        private static void AulaHeranca()
        {
            // var pessoaFisica = new Cadastro.PessoaFisica();
            // pessoaFisica.Id = 1;
            // pessoaFisica.Endereco = "Endereço Fisico";
            // pessoaFisica.Cidade = "Cidade Fisica";
            // pessoaFisica.Cep = "05055-555";
            // pessoaFisica.CPF = "000.000.000-00";

            // pessoaFisica.ImprimirDados();
            // pessoaFisica.ImprimirCpf();

            var funcionario = new Cadastro.Funcionario();
            funcionario.Id = 10;
            funcionario.Endereco = "Endereço Fisico";
            funcionario.Cidade = "Cidade Fisica";
            funcionario.Cep = "05055-555";
            funcionario.CPF = "000.000.000-00";

            funcionario.ImprimirDados();
            funcionario.ImprimirCpf();
        }

        private static void AulaClasses()
        {
            var resultado = Cadastro.Calculos.SomarNumeros(10, 20);
            Console.WriteLine(resultado);

            // var produto = new Cadastro.Produto();
            // produto.SetId(1);
            // produto.Descricao = "Teclado mecanico";
            // produto.ImprimirDescricao();
        }

        private static void AulaPropriedadeSomenteLeitura()
        {
            var produto = new Cadastro.Produto();
            produto.Descricao = "Teclado mecanico";
            //produto.Estoque = 1;
            Console.WriteLine(produto.Estoque);
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