using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace batalha_naval
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            int opcaoEscolhida = Entrada();
            if (opcaoEscolhida == 2) Jogar2();
        }
        public static int Entrada()
        {
            bool opcaoControle;
            int opcaoEscolhida;
            do
            {
                Console.WriteLine("Jogar contra o computador digite 1");
                Console.WriteLine("Jogar contra adversário real digite 2");
                var opcao = Console.ReadLine();
                opcaoControle = int.TryParse(opcao, out opcaoEscolhida);
                if(opcaoEscolhida != 1 && opcaoEscolhida !=2) opcaoControle = false;        
            }while(!opcaoControle);
            return opcaoEscolhida;
        }  
        public static void Jogar2()
        {
            Console.Clear();
            Console.WriteLine("Digite o nome do player 1");
            var player1 = Console.ReadLine();
            Console.WriteLine("Digite o nome do player 2");
            var player2 = Console.ReadLine();
            Console.Clear();
            var posicoesPlayer1 = PosicoesNoMapa(player1);
            var posicoesPlayer2 = PosicoesNoMapa(player2);
            ConstroiMapa(posicoesPlayer1);
            ConstroiMapa(posicoesPlayer1);
        }
        public static string[,] PosicoesNoMapa(string player)
        {
            var listaQuantidadeFrota = new List<int>{1,2,3,4};
            var posicoesPlayer = new string[10,10];
            var resultado = (listaQuantidadeFrota, posicoesPlayer);
            while(listaQuantidadeFrota.Sum() > 0)
            {
            Console.WriteLine($"soma da lista da frota = {listaQuantidadeFrota.Sum()}");
            resultado = TipoDaEmbarcacao(listaQuantidadeFrota, posicoesPlayer, player);
            listaQuantidadeFrota = resultado.Item1;
            posicoesPlayer = resultado.Item2;
            }
            return posicoesPlayer;
        }         

        public static (List<int>, string[,]) TipoDaEmbarcacao(List<int> listaQuantidadeFrota, string[,] posicoesPlayer, string player)
        {
            bool validador = false;
            string escolha;
            var posicoes = (listaQuantidadeFrota, posicoesPlayer);
            while(!validador)
            {
            Console.WriteLine($"Bem vindo {player}!!!");
            Console.WriteLine(@$"Você tem disponível {listaQuantidadeFrota[0]} Porta Aviões, {listaQuantidadeFrota[1]} Navio Tanques, {listaQuantidadeFrota[2]} Destroyers, {listaQuantidadeFrota[3]} Submarinos");
            Console.WriteLine("");
            Console.WriteLine("Qual o tipo de embarcação? Digite");
            Console.WriteLine("PS para Porta-Aviões"); //tem 1 e ocupa 5 espacos
            Console.WriteLine("NT para Navio-Tanque"); //tem 2 e ocupa 4 espacos
            Console.WriteLine("DS para Destroyers"); //tem 3 e ocupa 3 espacos
            Console.WriteLine("SB para Submarinos"); //tem 4 e ocupa 2 espacos
            escolha = Console.ReadLine();
            validador = RegexEscolhaDaFrota(escolha);
            Console.WriteLine("validador " + validador);

            if(validador == true)
            {
                Console.WriteLine("to aqui dentro");
                if(escolha == "PS")
                {
                    Console.WriteLine($" listaQua..frota {listaQuantidadeFrota[0]}");
                    if(listaQuantidadeFrota[0] == 0)
                    {
                        Console.WriteLine("***Você já colocou seu Porta-Aviões***");
                        validador = false;
                    }
                    else
                    {
                        Console.WriteLine("aqui no else");
                        posicoes = VerificaCoordenadas(escolha,posicoesPlayer,listaQuantidadeFrota);
                    }
                }
                else if(escolha == "NT")
                {
                    if(listaQuantidadeFrota[1] == 0)
                    {
                        Console.WriteLine("***Você já colocou seus Navios-Tanque***");
                        validador = false;
                    }
                    else
                    {
                        Console.WriteLine("aqui no else do NT");
                        posicoes = VerificaCoordenadas(escolha,posicoesPlayer,listaQuantidadeFrota);
                    }
                }
                else if(escolha == "DS")
                {
                    if(listaQuantidadeFrota[2] == 0)
                    {
                        Console.WriteLine("***Você já colocou seus Destroyers***");
                        validador = false;
                    }
                    else
                    {
                        posicoes = VerificaCoordenadas(escolha,posicoesPlayer,listaQuantidadeFrota);
                    }
                }
                else if(escolha == "SB")
                {
                    if(listaQuantidadeFrota[3] == 0)
                    {
                        Console.WriteLine("***Você já colocou seus Submarinos***");
                        validador = false;
                    }
                    else
                    {
                        posicoes = VerificaCoordenadas(escolha,posicoesPlayer,listaQuantidadeFrota);
                    }
                }          
            }
            }
            return (posicoes.Item1, posicoes.Item2);
        }
        public static (List<int>, string[,]) VerificaCoordenadas(string escolha, string[,] posicoesPlayer, List<int> listaQuantidadeFrota)
        { 
            (bool,string[,],List<int>) validaEntrada = (true, posicoesPlayer,listaQuantidadeFrota);
            string coordenadas;
            bool controleEntrada = false;
            do
            {
                Console.WriteLine("Qual a sua posição?");
                coordenadas = Console.ReadLine();
                controleEntrada = RegexEntrada(coordenadas);
                Console.WriteLine($"controle de Entrada {controleEntrada}");
                if(controleEntrada)
                {
                string letras = "ABCDEFGHIJ";
                string numeros = "1 2 3 4 5 6 7 8 9 10";
                string[] comecoEfim = new string[2];
                
                    if(coordenadas.Length == 4)
                    {
                        if(!letras.Contains(coordenadas[0]) || !letras.Contains(coordenadas[2])) validaEntrada.Item1 = false;
                        if(!numeros.Contains(coordenadas[1]) || !numeros.Contains(coordenadas[3])) validaEntrada.Item1 = false;

                        if(validaEntrada.Item1)
                        {
                            comecoEfim[0] = $"{coordenadas[0]}{coordenadas[1]}";
                            comecoEfim[1] = coordenadas.Substring(2); 
                            validaEntrada = VerificaTamanho(comecoEfim, escolha,posicoesPlayer,listaQuantidadeFrota);
                        }
                        else{
                            break;
                        }                 
                    }
                    if(coordenadas.Length == 6)
                    {
                        if(!letras.Contains(coordenadas[0]) || !letras.Contains(coordenadas[3])) validaEntrada.Item1 = false;
                        if(!numeros.Contains($"{coordenadas[1]}{coordenadas[2]}") || !numeros.Contains($"{coordenadas[4]}{coordenadas[5]}")) validaEntrada.Item1 = false;
                        if(validaEntrada.Item1)
                        {
                            Console.WriteLine("chhh");
                            comecoEfim[0] = coordenadas.Substring(0,3);
                            comecoEfim[1] = coordenadas.Substring(3);
                            validaEntrada = VerificaTamanho(comecoEfim, escolha,posicoesPlayer,listaQuantidadeFrota);
                        }
                    }
                    if(coordenadas.Length == 5)
                    {   
                        if(letras.Contains(coordenadas[2]))
                        {
                            if(!letras.Contains(coordenadas[0])) validaEntrada.Item1 = false;
                            if(!numeros.Contains(coordenadas[1]) || !numeros.Contains($"{coordenadas[3]}{coordenadas[4]}")) validaEntrada.Item1 = false;
                            if(validaEntrada.Item1)
                            {
                                comecoEfim[0] = coordenadas.Substring(0,2);
                                comecoEfim[1] = coordenadas.Substring(2);
                                Console.WriteLine($"comeco e fim 0 {comecoEfim[0]}");
                                Console.WriteLine($"comeco e fim 1 {comecoEfim[1]}");
                                validaEntrada = VerificaTamanho(comecoEfim, escolha,posicoesPlayer,listaQuantidadeFrota);
                                Console.WriteLine($"valida entrada {validaEntrada}");
                            }
                        }
                        else if(numeros.Contains(coordenadas[2]))
                        {
                            if(!letras.Contains(coordenadas[0]) || !letras.Contains(coordenadas[3])) validaEntrada.Item1 = false;
                            if(!numeros.Contains($"{coordenadas[1]}{coordenadas[2]}") || !numeros.Contains(coordenadas[4])) validaEntrada.Item1 = false;
                            if(validaEntrada.Item1)
                            {
                                comecoEfim[0] = coordenadas.Substring(0,3);
                                comecoEfim[1] = coordenadas.Substring(3);
                                validaEntrada = VerificaTamanho(comecoEfim, escolha,posicoesPlayer,listaQuantidadeFrota);
                            }
                        }
                        else
                        { 
                            validaEntrada.Item1 = false;
                        }
                    }
                }

                
            }while(!validaEntrada.Item1 || !controleEntrada);
            return (validaEntrada.Item3, posicoesPlayer);
        }

        public static (bool,string[,],List<int>) VerificaTamanho(string[] comecoEfim, string escolha, string[,] posicoesPlayer, List<int> listaQuantidadeFrota )
        {
            if(comecoEfim[0][0] != comecoEfim[1][0] && comecoEfim[0][1] != comecoEfim[1][1])
            {
                return (false, posicoesPlayer, listaQuantidadeFrota);
            }
            int tamanhoPermitido = 0;
            if(escolha == "PS") tamanhoPermitido = 5;
            if(escolha == "NT") tamanhoPermitido = 4;
            if(escolha == "DS") tamanhoPermitido = 3;
            if(escolha == "SB") tamanhoPermitido = 2;
            int tamanhoDaEntrada = comecoEfim[0].Length + comecoEfim[1].Length;
            if(tamanhoDaEntrada == 4)
            {
                if(comecoEfim[0][0] == comecoEfim[1][0])
                {   
                    if(tamanhoPermitido - (Math.Abs(comecoEfim[0][1] - comecoEfim[1][1]) + 1) != 0)
                    {
                        return (false, posicoesPlayer, listaQuantidadeFrota);
                    }
                    int indice0 = -1;
                    if(comecoEfim[0][0] == 'A')      indice0 = 0;
                    else if(comecoEfim[0][0] == 'B') indice0 = 1;
                    else if(comecoEfim[0][0] == 'C') indice0 = 2;
                    else if(comecoEfim[0][0] == 'D') indice0 = 3;
                    else if(comecoEfim[0][0] == 'E') indice0 = 4;
                    else if(comecoEfim[0][0] == 'F') indice0 = 5;
                    else if(comecoEfim[0][0] == 'G') indice0 = 6;
                    else if(comecoEfim[0][0] == 'H') indice0 = 7;
                    else if(comecoEfim[0][0] == 'I') indice0 = 8;
                    else                             indice0 = 9;

                    int indice1 = Convert.ToInt32(char.ToString(comecoEfim[0][1])) - 1;
                    int indice2 = Convert.ToInt32(char.ToString(comecoEfim[1][1])) - 1;
                    int maior = indice2;
                    int menor = indice1;
                    if(indice1 > maior)
                    {
                        maior = indice1;
                        menor = indice2;
                    }
                    bool validaPorNoMapa = true;
                    for(int i = menor; i <= maior; i++)
                    {
                        if(posicoesPlayer[indice0,i] == "A")
                        {
                            validaPorNoMapa = false;
                        }
                    }
                    if(validaPorNoMapa)
                    {
                        for(int i = menor; i <= maior; i++)
                        {
                            posicoesPlayer[indice0,i] = "A";
                        }

                        if(escolha == "PS")
                        {
                            listaQuantidadeFrota[0] -= 1;
                        }
                        else if(escolha == "NT")
                        {
                            listaQuantidadeFrota[1] -= 1;
                        }
                        else if(escolha == "DS")
                        {
                            listaQuantidadeFrota[2] -= 1;
                        }
                        else
                        {
                            listaQuantidadeFrota[3] -= 1;
                        }
                        return (true, posicoesPlayer, listaQuantidadeFrota);  
                    }
                }
                else if(comecoEfim[0][1] == comecoEfim[1][1])
                {
                    int inicial;
                    if(comecoEfim[0][0] == 'A') inicial = 0;
                    else if(comecoEfim[0][0] == 'B') inicial = 1;
                    else if(comecoEfim[0][0] == 'C') inicial = 2;
                    else if(comecoEfim[0][0] == 'D') inicial = 3;
                    else if(comecoEfim[0][0] == 'E') inicial = 4;
                    else if(comecoEfim[0][0] == 'F') inicial = 5;
                    else if(comecoEfim[0][0] == 'G') inicial = 6;
                    else if(comecoEfim[0][0] == 'H') inicial = 7;
                    else if(comecoEfim[0][0] == 'I') inicial = 8;
                    else inicial = 9;

                    int final;
                    if(comecoEfim[1][0] == 'A') final = 0;
                    else if(comecoEfim[1][0] == 'B') final = 1;
                    else if(comecoEfim[1][0] == 'C') final = 2;
                    else if(comecoEfim[1][0] == 'D') final = 3;
                    else if(comecoEfim[1][0] == 'E') final = 4;
                    else if(comecoEfim[1][0] == 'F') final = 5;
                    else if(comecoEfim[1][0] == 'G') final = 6;
                    else if(comecoEfim[1][0] == 'H') final = 7;
                    else if(comecoEfim[1][0] == 'I') final = 8;
                    else final = 9;

                    if(tamanhoPermitido - (Math.Abs(final - inicial) + 1) != 0)
                    {
                        return (false, posicoesPlayer, listaQuantidadeFrota);
                    } 
                    
                    int indice0 = Convert.ToInt32(char.ToString(comecoEfim[0][1]));
                    int indice1 = inicial;
                    int indice2 = final;
                    int maior = indice2;
                    int menor = indice1;

                    if(indice1 > maior)
                    {
                        maior = indice1;
                        menor = indice2;
                    }
                    bool validaPorNoMapa = true;
                    for(int i = menor; i <= maior; i++)
                    {
                        if(posicoesPlayer[i,indice0] == "A")
                        {
                            Console.WriteLine("conflito");
                            validaPorNoMapa = false;
                        }
                    }
                    if(validaPorNoMapa)
                    {
                        for(int i = menor; i <= maior; i++)
                        {
                            posicoesPlayer[indice0,i] = "A";
                        }
                        return (true, posicoesPlayer, listaQuantidadeFrota); 
                    }
                    
                }
            }
            if(tamanhoDaEntrada == 5)
            {
                Console.WriteLine($"tama {comecoEfim[0].Length}");
                if(comecoEfim[0].Length == 2)
                {
                    if(comecoEfim[0][0] == comecoEfim[1][0])
                    {   
                        int primeiro = Convert.ToInt32(char.ToString(comecoEfim[0][1]));
                        int segundo = Convert.ToInt32(char.ToString(comecoEfim[1][1]) + char.ToString(comecoEfim[1][2]));
                        
                        if(tamanhoPermitido - (Math.Abs(primeiro - segundo )+ 1) != 0)
                        {
                            return (false, posicoesPlayer, listaQuantidadeFrota);
                        }
                        int indice0 = -1;
                        if(comecoEfim[0][0] == 'A')      indice0 = 0;
                        else if(comecoEfim[0][0] == 'B') indice0 = 1;
                        else if(comecoEfim[0][0] == 'C') indice0 = 2;
                        else if(comecoEfim[0][0] == 'D') indice0 = 3;
                        else if(comecoEfim[0][0] == 'E') indice0 = 4;
                        else if(comecoEfim[0][0] == 'F') indice0 = 5;
                        else if(comecoEfim[0][0] == 'G') indice0 = 6;
                        else if(comecoEfim[0][0] == 'H') indice0 = 7;
                        else if(comecoEfim[0][0] == 'I') indice0 = 8;
                        else                             indice0 = 9;
                        int indice1 = primeiro - 1;
                        int indice2 = segundo - 1;
                        int maior = indice2;
                        int menor = indice1;
                        if(indice1 > maior)
                        {
                            maior = indice1;
                            menor = indice2;
                        }
                        bool validaPorNoMapa = true;
                        for(int i = menor; i <= maior; i++)
                        {
                            if(posicoesPlayer[indice0,i] == "A")
                            {
                                validaPorNoMapa = false;
                            }
                        }
                        if(validaPorNoMapa)
                        {
                            for(int i = menor; i <= maior; i++)
                            {
                                posicoesPlayer[indice0,i] = "A";
                            }
                            return (true, posicoesPlayer, listaQuantidadeFrota);  
                        }   
                    }
                    else
                    {
                        Console.WriteLine("é aqui essa coisa");
                        int inicial;
                        if(comecoEfim[0][0] == 'A') inicial = 0;
                        else if(comecoEfim[0][0] == 'B') inicial = 1;
                        else if(comecoEfim[0][0] == 'C') inicial = 2;
                        else if(comecoEfim[0][0] == 'D') inicial = 3;
                        else if(comecoEfim[0][0] == 'E') inicial = 4;
                        else if(comecoEfim[0][0] == 'F') inicial = 5;
                        else if(comecoEfim[0][0] == 'G') inicial = 6;
                        else if(comecoEfim[0][0] == 'H') inicial = 7;
                        else if(comecoEfim[0][0] == 'I') inicial = 8;
                        else inicial = 9;

                        int final;
                        if(comecoEfim[1][0] == 'A') final = 0;
                        else if(comecoEfim[1][0] == 'B') final = 1;
                        else if(comecoEfim[1][0] == 'C') final = 2;
                        else if(comecoEfim[1][0] == 'D') final = 3;
                        else if(comecoEfim[1][0] == 'E') final = 4;
                        else if(comecoEfim[1][0] == 'F') final = 5;
                        else if(comecoEfim[1][0] == 'G') final = 6;
                        else if(comecoEfim[1][0] == 'H') final = 7;
                        else if(comecoEfim[1][0] == 'I') final = 8;
                        else final = 9;

                        Console.WriteLine($"teste bool {tamanhoPermitido - (Math.Abs(final - inicial) + 1) != 0}");
                        Console.WriteLine($"teste tamanho {tamanhoPermitido}");
                        Console.WriteLine($"teste math abs +1 :{(Math.Abs(final - inicial) + 1)}");
                        if(tamanhoPermitido - (Math.Abs(final - inicial) + 1) != 0)
                        {
                            return (false, posicoesPlayer, listaQuantidadeFrota);
                        }
                        return (true, posicoesPlayer, listaQuantidadeFrota);
                    }
                }
                if(comecoEfim[0].Length == 3)
                {
                    if(comecoEfim[0][0] == comecoEfim[1][0])
                    {   
                        int primeiro = Convert.ToInt32(char.ToString(comecoEfim[0][1]) + char.ToString(comecoEfim[0][2]));
                        int segundo = Convert.ToInt32(char.ToString(comecoEfim[1][1]));
                        
                        Console.WriteLine($"primeiro {primeiro} sgudo {segundo}");
                        Console.WriteLine($"validacao {tamanhoPermitido - (Math.Abs(primeiro - segundo) + 1)}");

                        if(tamanhoPermitido - (Math.Abs(primeiro - segundo) + 1) != 0)
                        {
                            return (false, posicoesPlayer, listaQuantidadeFrota);
                        } 
                        

                        int indice0 = -1;
                        if(comecoEfim[0][0] == 'A')      indice0 = 0;
                        else if(comecoEfim[0][0] == 'B') indice0 = 1;
                        else if(comecoEfim[0][0] == 'C') indice0 = 2;
                        else if(comecoEfim[0][0] == 'D') indice0 = 3;
                        else if(comecoEfim[0][0] == 'E') indice0 = 4;
                        else if(comecoEfim[0][0] == 'F') indice0 = 5;
                        else if(comecoEfim[0][0] == 'G') indice0 = 6;
                        else if(comecoEfim[0][0] == 'H') indice0 = 7;
                        else if(comecoEfim[0][0] == 'I') indice0 = 8;
                        else                             indice0 = 9;
                        int indice1 = primeiro - 1;
                        int indice2 = segundo - 1;
                        int maior = indice2;
                        int menor = indice1;
                        if(indice1 > maior)
                        {
                            maior = indice1;
                            menor = indice2;
                        }
                        bool validaPorNoMapa = true;
                        for(int i = menor; i <= maior; i++)
                        {
                            if(posicoesPlayer[indice0,i] == "A")
                            {
                                validaPorNoMapa = false;
                            }
                        }
                        if(validaPorNoMapa)
                        {
                            for(int i = menor; i <= maior; i++)
                            {
                                posicoesPlayer[indice0,i] = "A";
                            }
                            Console.WriteLine("tchiriri thcarara");
                            return (true, posicoesPlayer, listaQuantidadeFrota);  
                        }    
                    }
                    else
                    {
                        int inicial;
                        if(comecoEfim[0][0] == 'A') inicial = 0;
                        else if(comecoEfim[0][0] == 'B') inicial = 1;
                        else if(comecoEfim[0][0] == 'C') inicial = 2;
                        else if(comecoEfim[0][0] == 'D') inicial = 3;
                        else if(comecoEfim[0][0] == 'E') inicial = 4;
                        else if(comecoEfim[0][0] == 'F') inicial = 5;
                        else if(comecoEfim[0][0] == 'G') inicial = 6;
                        else if(comecoEfim[0][0] == 'H') inicial = 7;
                        else if(comecoEfim[0][0] == 'I') inicial = 8;
                        else inicial = 9;

                        int final;
                        if(comecoEfim[1][0] == 'A') final = 0;
                        else if(comecoEfim[1][0] == 'B') final = 1;
                        else if(comecoEfim[1][0] == 'C') final = 2;
                        else if(comecoEfim[1][0] == 'D') final = 3;
                        else if(comecoEfim[1][0] == 'E') final = 4;
                        else if(comecoEfim[1][0] == 'F') final = 5;
                        else if(comecoEfim[1][0] == 'G') final = 6;
                        else if(comecoEfim[1][0] == 'H') final = 7;
                        else if(comecoEfim[1][0] == 'I') final = 8;
                        else final = 9;

                        Console.WriteLine($"teste bool {tamanhoPermitido - (Math.Abs(final - inicial) + 1) != 0}");
                        Console.WriteLine($"teste tamanho {tamanhoPermitido}");
                        Console.WriteLine($"teste math abs +1 :{(Math.Abs(final - inicial) + 1)}");
                        if(tamanhoPermitido - (Math.Abs(final - inicial) + 1) != 0)
                        {
                            return (false, posicoesPlayer, listaQuantidadeFrota);
                        }
                        return (true, posicoesPlayer, listaQuantidadeFrota);
                    }
                }
            }
            if(tamanhoDaEntrada == 6)
            {
                if(comecoEfim[0][0] != comecoEfim[1][0])
                {   
                    int inicial = 0;
                    int final = 0;
                    Console.WriteLine($"teste {comecoEfim[0][0]} e {comecoEfim[1][0]}");
                            
                    if(comecoEfim[0][0] == 'A')      inicial = 0;
                    else if(comecoEfim[0][0] == 'B') inicial = 1;
                    else if(comecoEfim[0][0] == 'C') inicial = 2;
                    else if(comecoEfim[0][0] == 'D') inicial = 3;
                    else if(comecoEfim[0][0] == 'E') inicial = 4;
                    else if(comecoEfim[0][0] == 'F') inicial = 5;
                    else if(comecoEfim[0][0] == 'G') inicial = 6;
                    else if(comecoEfim[0][0] == 'H') inicial = 7;
                    else if(comecoEfim[0][0] == 'I') inicial = 8;
                                                else inicial = 9;

                    if(comecoEfim[1][0] == 'A')      final = 0;
                    else if(comecoEfim[1][0] == 'B') final = 1;
                    else if(comecoEfim[1][0] == 'C') final = 2;
                    else if(comecoEfim[1][0] == 'D') final = 3;
                    else if(comecoEfim[1][0] == 'E') final = 4;
                    else if(comecoEfim[1][0] == 'F') final = 5;
                    else if(comecoEfim[1][0] == 'G') final = 6;
                    else if(comecoEfim[1][0] == 'H') final = 7;
                    else if(comecoEfim[1][0] == 'I') final = 8;
                                                else final = 9;

                    if(tamanhoPermitido - (Math.Abs(inicial - final) + 1) != 0)
                    {
                        return (false, posicoesPlayer, listaQuantidadeFrota);
                    }    

                    int indice0 = 9;
                    int indice1 = inicial;
                    int indice2 = final;
                    int maior = indice2;
                    int menor = indice1;

                    if(indice1 > maior)
                    {
                        maior = indice1;
                        menor = indice2;
                    }
                    bool validaPorNoMapa = true;
                    for(int i = menor; i <= maior; i++)
                    {
                        if(posicoesPlayer[i,indice0] == "A")
                        {
                            Console.WriteLine("conflito");
                            validaPorNoMapa = false;
                        }
                    }
                    if(validaPorNoMapa)
                    {
                        for(int i = menor; i <= maior; i++)
                        {
                            posicoesPlayer[indice0,i] = "A";
                        }
                        return (true, posicoesPlayer, listaQuantidadeFrota); 
                    }
                }
                else
                {
                    return (false, posicoesPlayer, listaQuantidadeFrota);
                }
            }  
            return (false, posicoesPlayer, listaQuantidadeFrota);     
        }

        public static string[,] ColocaNoMapa(string[,] posicoesPlayer, string[] comecoEfim )
        {
            Console.WriteLine(comecoEfim[0]);
            Console.WriteLine(comecoEfim[1]);
            return posicoesPlayer;
        }

        public static void ConstroiMapa(string[,] mapa){

            Console.WriteLine("   |" + " 1 " + " 2 " + " 3 " + " 4 " + " 5 " + " 6 " + " 7 " + " 8 " + " 9 " + " 10 ");
            Console.WriteLine("------------------------------------");
            Console.WriteLine($" A | {mapa[0, 0]}  {mapa[0, 1]}  {mapa[0, 2]}  {mapa[0, 3]}  {mapa[0, 4]}  {mapa[0, 5]}  {mapa[0, 6]}  {mapa[0, 7]}  {mapa[0, 8]}  {mapa[0, 9]}");
            Console.WriteLine("   |");
            Console.WriteLine($" B | {mapa[1, 0]}  {mapa[1, 1]}  {mapa[1, 2]}  {mapa[1, 3]}  {mapa[1, 4]}  {mapa[1, 5]}  {mapa[1, 6]}  {mapa[1, 7]}  {mapa[1, 8]}  {mapa[1, 9]}");
            Console.WriteLine("   |");
            Console.WriteLine($" C | {mapa[2, 0]}  {mapa[2, 1]}  {mapa[2, 2]}  {mapa[2, 3]}  {mapa[2, 4]}  {mapa[2, 5]}  {mapa[2, 6]}  {mapa[2, 7]}  {mapa[2, 8]}  {mapa[2, 9]}");
            Console.WriteLine("   |");
            Console.WriteLine($" D | {mapa[3, 0]}  {mapa[3, 1]}  {mapa[3, 2]}  {mapa[3, 3]}  {mapa[3, 4]}  {mapa[3, 5]}  {mapa[3, 6]}  {mapa[3, 7]}  {mapa[3, 8]}  {mapa[3, 9]}");
            Console.WriteLine("   |");
            Console.WriteLine($" E | {mapa[4, 0]}  {mapa[4, 1]}  {mapa[4, 2]}  {mapa[4, 3]}  {mapa[4, 4]}  {mapa[4, 5]}  {mapa[4, 6]}  {mapa[4, 7]}  {mapa[4, 8]}  {mapa[4, 9]}");
            Console.WriteLine("   |");
            Console.WriteLine($" F | {mapa[5, 0]}  {mapa[5, 1]}  {mapa[5, 2]}  {mapa[5, 3]}  {mapa[5, 4]}  {mapa[5, 5]}  {mapa[5, 6]}  {mapa[5, 7]}  {mapa[5, 8]}  {mapa[5, 9]}");
            Console.WriteLine("   |");
            Console.WriteLine($" G | {mapa[6, 0]}  {mapa[6, 1]}  {mapa[6, 2]}  {mapa[6, 3]}  {mapa[6, 4]}  {mapa[6, 5]}  {mapa[6, 6]}  {mapa[6, 7]}  {mapa[6, 8]}  {mapa[6, 9]}");
            Console.WriteLine("   |");
            Console.WriteLine($" H | {mapa[7, 0]}  {mapa[7, 1]}  {mapa[7, 2]}  {mapa[7, 3]}  {mapa[7, 4]}  {mapa[7, 5]}  {mapa[7, 6]}  {mapa[7, 7]}  {mapa[7, 8]}  {mapa[7, 9]}");
            Console.WriteLine("   |");
            Console.WriteLine($" I | {mapa[8, 0]}  {mapa[8, 1]}  {mapa[8, 2]}  {mapa[8, 3]}  {mapa[8, 4]}  {mapa[8, 5]}  {mapa[8, 6]}  {mapa[8, 7]}  {mapa[8, 8]}  {mapa[8, 9]}");
            Console.WriteLine("   |");
            Console.WriteLine($" J | {mapa[0, 0]}  {mapa[9, 1]}  {mapa[9, 2]}  {mapa[9, 3]}  {mapa[9, 4]}  {mapa[9, 5]}  {mapa[9, 6]}  {mapa[9, 7]}  {mapa[9, 8]}  {mapa[9, 9]}");

        }

        public static bool RegexEntrada(string coordenada)
        {   
            string padrao = @"^[A-J]{1}([0-9]{1}|[0-9]{2})[A-J]{1}([0-9]{1}|[0-9]{2}){1}$";
            Console.WriteLine($"teste input regex {Regex.IsMatch(coordenada, padrao)}");            
        
            return Regex.IsMatch(coordenada, padrao);
        }

        public static bool RegexEscolhaDaFrota(string escolha)
        {   
            string padrao = @"^([P][S]|[N][T]|[D][S]|[S][B])";
            Console.WriteLine($"input regex frota {Regex.IsMatch(escolha, padrao)}");            
        
            return Regex.IsMatch(escolha, padrao);
        }
    }
}