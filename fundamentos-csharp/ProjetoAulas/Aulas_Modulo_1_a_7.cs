// // See https://aka.ms/new-console-template for more information

// // para usar arrays 
// using System.Collections;
// using System.Collections.Generic;

// // variáveis

// Console.WriteLine("\n\n Variáveis \n\n");

// int idade = 35;
// Console.WriteLine(idade);

// string nome = "Pessoas";
// Console.WriteLine(nome);

// decimal valor = 200.9m;
// double valorDouble = 200.9;
// float valorFloat = 209.9f;

// Console.WriteLine(valor);
// Console.WriteLine(valorDouble);
// Console.WriteLine(valorFloat);

// var idadeNova = 34;
// Console.WriteLine(idadeNova);

// char flag = 'C';
// Console.WriteLine(flag);

// bool ativo = true;
// Console.WriteLine(ativo);

// // constantes
// const string descricao = "CSharp";
// Console.WriteLine(descricao);


// /Operadores aritiméticos/

// Console.WriteLine("\n\n Operadores Aritiméticos \n\n");

// int numero1 = 20;
// int numero2 = 10;

// var soma = numero1 + numero2;
// var subtracao = numero1 - numero2;
// var multiplicacao = numero1 * numero2;
// var divisao = numero1 / numero2;

// Console.WriteLine(soma);
// Console.WriteLine(subtracao);
// Console.WriteLine(multiplicacao);
// Console.WriteLine(divisao);

// /Operadores Relacionais/

// Console.WriteLine("\n\n Operadores Relacionais \n\n");

// int numeroOR1 = 20;
// int numeroOR2 = 10;

// bool igual = numeroOR1 == numeroOR2;
// Console.WriteLine(igual);

// bool maior = numeroOR1 > numeroOR2;
// Console.WriteLine(maior);

// bool menor = numeroOR1 < numeroOR2;
// Console.WriteLine(menor);

// bool maiorOuIgual = numeroOR1 >= numeroOR2;
// Console.WriteLine(maiorOuIgual);

// bool menorOuIgual = numeroOR1 <= numeroOR2;
// Console.WriteLine(menorOuIgual);


// bool diferente = numeroOR1 != numeroOR2;
// Console.WriteLine(diferente);


// /Operadores lógicos/

// Console.WriteLine("\n\n Operadores lógicos \n\n");

// int numeroOL1 = 20;
// int numeroOL2 = 10;

// bool valido = numeroOL1 > numeroOL2 && 8 > 7;
// Console.WriteLine(valido);

// bool valido2 = numeroOL1 > numeroOL2 || 6 > 7;
// Console.WriteLine(valido2);

// bool valido3 = !(numeroOL1 < numeroOL2) || 6 > 7;
// Console.WriteLine(valido3);


// /Operador ternário/

// Console.WriteLine("\n\n Operador ternário \n\n");

// bool cadAtivo = true;
// string status = cadAtivo ? "Cadastro ativo" : "Cadastro inativo";
// Console.WriteLine(status);

// // funções

// Console.WriteLine("\n\n Funções \n\n");

// var nomeCompleto = NomeCompleto();

// Console.WriteLine(nomeCompleto);

// EscreveNome();

// void EscreveNome()
// {
//     var nome = NomeCompleto();
//     var soma = SomaValares();

//     Console.WriteLine(nome);
//     Console.WriteLine(soma);
// }

// string NomeCompleto()
// {

//     string nome = "Michael";
//     string segundoNome = "Jackson";

//     return $"{nome} {segundoNome}";
// }

// int SomaValares()
// {
//     return 20 + 10;
// }

// /* Funções que recebem valores */

// Console.WriteLine("\n\n Funções que recebem valores \n\n");

// var somaValoresComParametros = SomaValoresComParametros(20, 10);
// Console.WriteLine(somaValoresComParametros);

// var nomeEIdade = NomeEIdade("Michael", 35);
// Console.WriteLine(nomeEIdade);

// int SomaValoresComParametros(int num1, int num2)
// {
//     return num1 + num2;
// }

// string NomeEIdade(string nome, int idade)
// {
//     return $"Meu nome é {nome} e tenho a seguinte idade: {idade}";
// }


// //Estrutura de Dados

// // Array List

// Console.WriteLine("\n\n Array List \n\n");

// var arrayList = new ArrayList();

// arrayList.Add(1);
// arrayList.Add("Rafael");
// arrayList.Add(true);

// Console.WriteLine(arrayList[1]);

// arrayList.RemoveAt(1); // remove um item do array pelo index
// arrayList.Clear(); // limpa o array

// foreach (var item in arrayList)
// {
//     Console.WriteLine(item);
// }

// // Array tipados

// Console.WriteLine("\n\n Array tipados \n\n");

// var arrayTipadoNumero = new int[3] { 1, 2, 3 };

// arrayTipadoNumero[0] = 10;
// arrayTipadoNumero[1] = 11;
// arrayTipadoNumero[2] = 12;

// // Array.Resize(ref arrayTipadoNumero, 10); //aumenta a capacidade 
// // arrayTipadoNumero[3] = 13;

// foreach (var item in arrayTipadoNumero)
// {
//     Console.WriteLine(item);
// }

// var arrayString = new string[3] { "Rafael", "Jackson", "Silva" };

// foreach (var item in arrayString)
// {
//     Console.WriteLine(item);
// }

// // Lista Genéricas

// Console.WriteLine("\n\n Listas Genéricas\n\n");

// var lista = new List<string>(10)
// {
//     "Fulano"
// };

// lista.Add("Ciclano");
// lista.Add("Beltrano");

// var nomeLista = lista[0];
// Console.WriteLine(nomeLista);

// lista.RemoveAt(1); // remove um item da lista pelo index

// foreach (var item in lista)
// {
//     Console.WriteLine(item);
// }

// // Dicionários

// Console.WriteLine("\n\nDICIONÁRIOS\n\n");

// var dicionario = new Dictionary<int, string>()
// {
//     {1, "Tertuliano"},
//     {2, "Quartuliano"}
// };

// dicionario.Add(10, "Ciclano");

// dicionario[50] = "Beltrano"; // adiciona um item ao dicionario com a chave 50 e valor 'Beltrano'

// var nomeDicionario = dicionario[10]; // pega o item do dicionario pela chave
// Console.WriteLine(nomeDicionario);

// foreach (var item in dicionario)
// {
//     Console.WriteLine($"{item.Key} - {item.Value}");
// }

// // Queue

// Console.WriteLine("\n\nQUEUE\n\n");

// // Queue - Fila - FIFO - Primeiro a entrar, Primeiro a sair

// var queue = new Queue<string>();

// queue.Enqueue("Fulano");
// queue.Enqueue("Beltrano");

// var itemSeraRemovidoFila = queue.Peek(); // retorna o primeiro valor que saira da fila

// Console.WriteLine("Peek - " + itemSeraRemovidoFila);

// var itemRemovidoFila = queue.Dequeue(); // remove o primeiro item da fila
// var itemRemovidoFila1 = queue.Dequeue();
// Console.WriteLine("Dequeue - " + itemRemovidoFila);
// Console.WriteLine("Dequeue - " + itemRemovidoFila1);


// foreach (var item in queue)
// {
//     Console.WriteLine(item);
// }

// // Stack

// Console.WriteLine("\n\nSTACK\n\n");

// // Stack - Pilha - Primeiro a entrar, Primeiro a sair

// var stack = new Stack<string>();

// stack.Push("Fulano"); // empilha um item na pilha
// stack.Push("Ciclano");

// var itemSeraRemovidoPilha = stack.Peek(); // retorna o primeiro item que saira da pilha
// Console.WriteLine("Peek - " + itemSeraRemovidoPilha);

// var itemRemovidoPilha = stack.Pop(); // remove o primeiro item da pilha e retorna o valor
// Console.WriteLine("Pop - " + itemRemovidoPilha);

// foreach (var item in stack)
// {
//     Console.WriteLine(item);
// }

// // Estrutura de controle

// // IF/ELSE 

// var idadeValor = 18;

// Console.WriteLine("\n\n IF/ELSE  \n\n");

// if (idadeValor >= 18)
// {
//     Console.WriteLine("Maior de idade");
// }
// else if (idadeValor > 10 && idadeValor < 18)
// {
//     Console.WriteLine("Adolescente");
// }
// else
// {
//     Console.WriteLine("Criança");
// }

// // Switch

// var diaSemana = 0;

// Console.WriteLine("\n\n SWITCH  \n\n");

// switch (diaSemana)
// {
//     case 0:
//         Console.WriteLine("Hoje é domingo");
//         break;
//     case 1:
//         Console.WriteLine("Hoje é segunda-feira");
//         break;
//     case 2:
//         Console.WriteLine("Hoje é terça-feira");
//         break;
//     case 3:
//         Console.WriteLine("Hoje é quarta-feira");
//         break;
//     case 4:
//         Console.WriteLine("Hoje é quinta-feira");
//         break;
//     case 5:
//         Console.WriteLine("Hoje é sexta-feira");
//         break;
//     case 6:
//         Console.WriteLine("Hoje é sabado");
//         break;
//     default:
//         Console.WriteLine("Dia inválido");
//         break;
// }

// switch (diaSemana < 1 && diaSemana == 0)
// {
//     case true:
//         Console.WriteLine("Hoje é domingo");
//         break;
//     case false:
//         Console.WriteLine("Dia inválido");
//         break;
// }

// // FOR

// Console.WriteLine("\n\n FOR  \n\n");

// var listaGenerica = new List<int> {
//     1, 2, 3, 4, 5, 6, 7, 8, 9, 10
// };

// for (int i = 0; i < listaGenerica.Count; i++)
// {
//     Console.WriteLine($"Indice: {i} - Valor: {listaGenerica[i]}");
// }

// // FOR EACH

// Console.WriteLine("\n\n FOR EACH  \n\n");

// var listaGenerica2 = new List<string> {
//     "Fulano", "Ciclano", "Beltrano"
// };

// foreach (var item in listaGenerica2)
// {
//     Console.WriteLine(item);
// }

// foreach (var item in "Bora Programar")
// { // iterar sobre uma string
//     Console.WriteLine("item");
// }

// // WHILE e DO WHILE

// Console.WriteLine("\n\n WHILE e DO WHILE  \n\n");

// var contador = 0;

// while (contador < 10)
// {
//     Console.WriteLine($"contador = {contador}");
//     contador++;
// }

// var contador2 = 0;
// do
// {
//     Console.WriteLine($"contador2 = {contador2}");
//     contador2++;
// } while (contador2 < 10);


// // Break e Continue

// Console.WriteLine("\n\n BREAK e CONTINUE  \n\n");

// var contador3 = 0;
// while (contador3 < 10)
// {
//     if (contador3 < 2) {
//         Console.WriteLine("Continuando...");
//         contador3++;
//         continue; // pula para o proximo loop
//     }

//     Console.WriteLine($"contador3 = {contador3}");
//     contador3++;
//     if (contador3 == 2)
//     {
//         Console.WriteLine("Valor de contador3 é igual a 2 (dois)");
//         break; // sai do loop
//     }
// }
