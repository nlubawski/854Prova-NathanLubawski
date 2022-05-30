using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace batalha_naval
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            int opcaoEscolhida = Entrada();
            if (opcaoEscolhida == 2) Jogar2();
            if (opcaoEscolhida == 1) Jogar1();
        }
        public static int Entrada()
        {
            bool opcaoControle;
            int opcaoEscolhida;
            
            do
            {
            Console.Clear();
            Console.WriteLine(@$"         
                                         o oo oo 
                                            o ooo           _________
                                              o oo         |        #
                                               o o         |   _____#
                                                oo        _|_ _|_
                                                 o       |       |
                __                    ___________________|       |_________________
                | ---_______----------                                              \
                &  ;|    _____                    TURMA 854                          |
                |__ ---------------________________________________________________ /
            ");
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Digite o nome do player 1");
            Console.ResetColor();
            var player1 = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Digite o nome do player 2");
            Console.ResetColor();
            var player2 = Console.ReadLine();
            Console.Clear();
            var posicoesPlayer1 = PosicoesNoMapa(player1);
            Console.Clear();
            var posicoesPlayer2 = PosicoesNoMapa(player2);
            Iniciar(player1, posicoesPlayer1, player2, posicoesPlayer2);
        }
        public static string[,] PosicoesNoMapa(string player)
        {
            var listaQuantidadeFrota = new List<int>{1,2,3,4};
            var posicoesPlayer = new string[10,10];
            for(var i = 0; i < 10; i++)
            {
                for(var j = 0; j < 10; j++)
                {
                    posicoesPlayer[i,j] = " ";
                }
            }
            var resultado = (listaQuantidadeFrota, posicoesPlayer);
            while(listaQuantidadeFrota.Sum() > 0)
            {
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
            Console.Write($"Bem vindo");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($" {player}!!!");
            Console.ResetColor();
            Console.WriteLine(@$"Você tem disponível {listaQuantidadeFrota[0]} Porta Aviões, {listaQuantidadeFrota[1]} Navio Tanques, {listaQuantidadeFrota[2]} Destroyers, {listaQuantidadeFrota[3]} Submarinos");
            Console.WriteLine("");
            Console.WriteLine("Qual o tipo de embarcação? Digite");
            Console.WriteLine("PS para Porta-Aviões (ocupa 5 espacos)"); 
            Console.WriteLine("NT para Navio-Tanque (ocupa 4 espacos)");
            Console.WriteLine("DS para Destroyers (ocupa 3 espacos)");
            Console.WriteLine("SB para Submarinos (ocupa ocupa 2 espacos)");
            escolha = Console.ReadLine();
            validador = RegexEscolhaDaFrota(escolha);

            if(validador == true)
            {
                if(escolha == "PS")
                {
                    if(listaQuantidadeFrota[0] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("***Você já colocou seu Porta-Aviões***");
                        Console.ResetColor();
                        validador = false;
                    }
                    else
                    {
                        posicoes = VerificaCoordenadas(escolha,posicoesPlayer,listaQuantidadeFrota);
                    }
                }
                else if(escolha == "NT")
                {
                    if(listaQuantidadeFrota[1] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("***Você já colocou seus Navios-Tanque***");
                        Console.ResetColor();
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
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("***Você já colocou seus Destroyers***");
                        Console.ResetColor();
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
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("***Você já colocou seus Submarinos***");
                        Console.ResetColor();
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
            string[,] tabuleiroAuxilar = new string[10,10];
            for(var i = 0; i < 10; i++)
            {
                for(var j = 0; j < 10; j++)
                {
                    tabuleiroAuxilar[i,j] = " ";
                }
            }

            do
            {
                ConstroiMapa(tabuleiroAuxilar);
                Console.WriteLine("Qual a sua posição?");
                coordenadas = Console.ReadLine();
                controleEntrada = RegexEntrada(coordenadas);
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
                                validaEntrada = VerificaTamanho(comecoEfim, escolha,posicoesPlayer,listaQuantidadeFrota);
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
                        if(posicoesPlayer[indice0,i] == "X")
                        {
                            validaPorNoMapa = false;
                        }
                    }
                    if(validaPorNoMapa)
                    {
                        for(int i = menor; i <= maior; i++)
                        {
                            posicoesPlayer[indice0,i] = "X";
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
                        if(posicoesPlayer[i,indice0] == "X")
                        {
                            validaPorNoMapa = false;
                        }
                    }
                    if(validaPorNoMapa)
                    {
                        for(int i = menor; i <= maior; i++)
                        {
                            posicoesPlayer[i,indice0] = "X";
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
            }
            if(tamanhoDaEntrada == 5)
            {
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
                            if(posicoesPlayer[indice0,i] == "X")
                            {
                                validaPorNoMapa = false;
                            }
                        }
                        if(validaPorNoMapa)
                        {
                            for(int i = menor; i <= maior; i++)
                            {
                                posicoesPlayer[indice0,i] = "X";
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
                            ConstroiMapa(posicoesPlayer);
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
                        
                        if(tamanhoPermitido - (Math.Abs(final - inicial) + 1) != 0)
                        {
                            return (false, posicoesPlayer, listaQuantidadeFrota);
                        }
                        //TOFIX POR NO MAPS
                        return (true, posicoesPlayer, listaQuantidadeFrota);
                    }
                }
                if(comecoEfim[0].Length == 3)
                {
                    if(comecoEfim[0][0] == comecoEfim[1][0])
                    {   
                        int primeiro = Convert.ToInt32(char.ToString(comecoEfim[0][1]) + char.ToString(comecoEfim[0][2]));
                        int segundo = Convert.ToInt32(char.ToString(comecoEfim[1][1]));
                        
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
                            if(posicoesPlayer[indice0,i] == "X")
                            {
                                validaPorNoMapa = false;
                            }
                        }
                        if(validaPorNoMapa)
                        {
                            for(int i = menor; i <= maior; i++)
                            {
                                posicoesPlayer[indice0,i] = "X";
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
                        if(tamanhoPermitido - (Math.Abs(final - inicial) + 1) != 0)
                        {
                            return (false, posicoesPlayer, listaQuantidadeFrota);
                        }
                        //TO FIX POR NO MAPS
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
                        if(posicoesPlayer[i,indice0] == "X")
                        {
                            validaPorNoMapa = false;
                        }
                    }
                    if(validaPorNoMapa)
                    {
                        for(int i = menor; i <= maior; i++)
                        {
                            posicoesPlayer[i,indice0] = "X";
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
                else
                {
                    return (false, posicoesPlayer, listaQuantidadeFrota);
                }
            }  
            return (false, posicoesPlayer, listaQuantidadeFrota);     
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
            string padrao = @"^[A-J]{1}([1-9]{1}|[1-9]{1}0)[A-J]{1}([1-9]{1}|10)$";
            return Regex.IsMatch(coordenada, padrao);
        }
        public static bool RegexEscolhaDaFrota(string escolha)
        {   
            string padrao = @"^([P][S]|[N][T]|[D][S]|[S][B])";
            return Regex.IsMatch(escolha, padrao);
        }
        public static bool RegexValidaJogada(string jogada)
        {   
            string padrao = @"^[A-J]{1}([1-9]|10)$";
            return Regex.IsMatch(jogada, padrao);
        }
        public static void Iniciar(string player1, string[,] posicoesPlayer1, string player2, string[,] posicoesPlayer2)
        {
            Console.WriteLine("Vamos ao jogo! Boa Sorte!");
            bool vencedor = false;
            string[,] tabuleiroParaPlayer1 = new string[10,10];
            string[,] tabuleiroParaPlayer2 = new string[10,10];
            int contagemPlayer1 = 2; //teste
            int contagemPlayer2 = 2; //Tofix
            var resultado = (vencedor, contagemPlayer1, tabuleiroParaPlayer1);
            
            for(var i = 0; i < 10; i++)
            {
                for(var j = 0; j < 10; j++)
                {
                    tabuleiroParaPlayer1[i,j] = " ";
                    tabuleiroParaPlayer2[i,j] = " ";
                }
            }
            
            while(!vencedor)
            {
                resultado = Jogada(player1, tabuleiroParaPlayer1, posicoesPlayer2, contagemPlayer1, vencedor);
                vencedor = resultado.Item1;
                contagemPlayer1 = resultado.Item2;
                tabuleiroParaPlayer1 = resultado.Item3;
                

                if(vencedor == false)
                {
                    resultado = Jogada(player2, tabuleiroParaPlayer2, posicoesPlayer1, contagemPlayer2, vencedor);
                    vencedor = resultado.Item1;
                    contagemPlayer2 = resultado.Item2;
                    tabuleiroParaPlayer2 = resultado.Item3;
                }
            }
        }

        public static (bool, int, string[,]) Jogada(string player, string[,] tabuleiroParaPlayer, string[,] posicoesPlayerOposto, int contagemPlayer, bool vencedor)
        {
                ConstroiMapa(tabuleiroParaPlayer);
                Console.Write($"Sua vez de jogar ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{player}:");
                
                string jogadaPlayer;
                Console.ResetColor();
                jogadaPlayer = Console.ReadLine();

                while(!RegexValidaJogada(jogadaPlayer))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Tente novamente ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(player);
                    Console.ResetColor();
                    jogadaPlayer = Console.ReadLine();
                }
                Console.Clear();
                int indice1 = -1;
                int indice2 = -1;
                if(jogadaPlayer.Length == 2)
                {
                    if(jogadaPlayer[1] == '1') indice2 = 0;
                    else if(jogadaPlayer[1] == '2') indice2 = 1;
                    else if(jogadaPlayer[1] == '3') indice2 = 2;
                    else if(jogadaPlayer[1] == '4') indice2 = 3;
                    else if(jogadaPlayer[1] == '5') indice2 = 4;
                    else if(jogadaPlayer[1] == '6') indice2 = 5;
                    else if(jogadaPlayer[1] == '7') indice2 = 6;
                    else if(jogadaPlayer[1] == '8') indice2 = 7;
                    else if(jogadaPlayer[1] == '9') indice2 = 8;
                    else indice2 = 9;

                    if(jogadaPlayer[0] == 'A') indice1 = 0;
                    else if(jogadaPlayer[0] == 'B') indice1 = 1;
                    else if(jogadaPlayer[0] == 'C') indice1 = 2;
                    else if(jogadaPlayer[0] == 'D') indice1 = 3;
                    else if(jogadaPlayer[0] == 'E') indice1 = 4;
                    else if(jogadaPlayer[0] == 'F') indice1 = 5;
                    else if(jogadaPlayer[0] == 'G') indice1 = 6;
                    else if(jogadaPlayer[0] == 'H') indice1 = 7;
                    else if(jogadaPlayer[0] == 'I') indice1 = 8;
                    else indice1 = 9;
                }
                else if(jogadaPlayer.Length == 3)
                {
                    indice2 = 9;

                    if(jogadaPlayer[0] == 'A') indice1 = 0;
                    else if(jogadaPlayer[0] == 'B') indice1 = 1;
                    else if(jogadaPlayer[0] == 'C') indice1 = 2;
                    else if(jogadaPlayer[0] == 'D') indice1 = 3;
                    else if(jogadaPlayer[0] == 'E') indice1 = 4;
                    else if(jogadaPlayer[0] == 'F') indice1 = 5;
                    else if(jogadaPlayer[0] == 'G') indice1 = 6;
                    else if(jogadaPlayer[0] == 'H') indice1 = 7;
                    else if(jogadaPlayer[0] == 'I') indice1 = 8;
                    else indice1 = 9;
                }
                bool acerto = false;
                if(posicoesPlayerOposto[indice1,indice2] == "X")
                {
                    tabuleiroParaPlayer[indice1,indice2] = "X";
                    contagemPlayer -=1;
                    acerto = true;
                }
                else
                {
                    tabuleiroParaPlayer[indice1,indice2] = "A";
                }
                if(contagemPlayer == 0)
                {
                    vencedor = true;
                    
                }
                ConstroiMapa(tabuleiroParaPlayer);
                if(acerto)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{player}!");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(" Você Acertou!!!");
                    Thread.Sleep(1000);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{player}, ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" Você Errou...");
                    Thread.Sleep(1000);
                    Console.ResetColor();
                }
                if(vencedor)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"Parabéns ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{player}!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" Você Ganhou!!!");
                    Thread.Sleep(3000);
                    Console.ResetColor();
                }  
                Console.WriteLine("trocando turno ...");
                Thread.Sleep(3000);
                if(!vencedor)
                {
                    Console.Clear();
                }

                return (vencedor, contagemPlayer, tabuleiroParaPlayer);
        }

        public static void Jogar1()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Digite o seu nome");
            Console.ResetColor();
            var player1 = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Seu adversário é o Hannibal!!! Prepare-se ... ");
            Thread.Sleep(2000);
            Console.ResetColor();
            var posicoesPlayer1 = PosicoesNoMapa(player1);
            Console.Clear();

            //gerar posicoes do player 2
            var posicoesPlayer2 = PosicoesNoMapa();
            Iniciar(player1, posicoesPlayer1, posicoesPlayer2);
        }

        //minha primeira sobrecarga
        public static string[,] PosicoesNoMapa()
        {
            var listaQuantidadeFrota = new List<int>{0,0,0,1};
            var posicoesPlayer = new string[10,10];
            for(var i = 0; i < 10; i++)
            {
                for(var j = 0; j < 10; j++)
                {
                    posicoesPlayer[i,j] = " ";
                }
            }
            var resultado = (listaQuantidadeFrota, posicoesPlayer);
            while(listaQuantidadeFrota.Sum() > 0)
            {
            resultado = TipoDaEmbarcacao(listaQuantidadeFrota, posicoesPlayer);
            listaQuantidadeFrota = resultado.Item1;
            posicoesPlayer = resultado.Item2;
            }
            return posicoesPlayer;
        }  

        //outra sobrecarga
        public static (List<int>, string[,]) TipoDaEmbarcacao(List<int> listaQuantidadeFrota, string[,] posicoesPlayer)
        {
            bool validador = false;
            string escolha;
            string Boss = "";
            var posicoes = (listaQuantidadeFrota, posicoesPlayer);
            while(!validador)
            {
            
            //gerar o aleatorios
            Random frota = new Random();
            int numeroAleatorio = frota.Next(0,4);
            string[] entradasFrota = {"PS","NT","DS","SB"};
            escolha = entradasFrota[numeroAleatorio];
            validador = RegexEscolhaDaFrota(escolha);

            if(validador == true)
            {
                if(escolha == "PS")
                {
                    if(listaQuantidadeFrota[0] == 0)
                    {
                        validador = false;
                    }
                    else
                    {
                        posicoes = VerificaCoordenadas(escolha,posicoesPlayer,listaQuantidadeFrota,Boss);
                    }
                }
                else if(escolha == "NT")
                {
                    if(listaQuantidadeFrota[1] == 0)
                    {
                        validador = false;
                    }
                    else
                    {
                        posicoes = VerificaCoordenadas(escolha,posicoesPlayer,listaQuantidadeFrota,Boss);
                    }
                }
                else if(escolha == "DS")
                {
                    if(listaQuantidadeFrota[2] == 0)
                    {
                        validador = false;
                    }
                    else
                    {
                        posicoes = VerificaCoordenadas(escolha,posicoesPlayer,listaQuantidadeFrota,Boss);
                    }
                }
                else if(escolha == "SB")
                {
                    if(listaQuantidadeFrota[3] == 0)
                    {
                        validador = false;
                    }
                    else
                    {
                        posicoes = VerificaCoordenadas(escolha,posicoesPlayer,listaQuantidadeFrota,Boss);
                    }
                }          
            }
            }
            return (posicoes.Item1, posicoes.Item2);
        }

        //mais uma sobrecarga
        public static (List<int>, string[,]) VerificaCoordenadas(string escolha, string[,] posicoesPlayer, List<int> listaQuantidadeFrota, string Boss)
        { 
            (bool,string[,],List<int>) validaEntrada = (true, posicoesPlayer,listaQuantidadeFrota);
            string coordenadas;
            bool controleEntrada = false;
            string[,] tabuleiroAuxilar = new string[10,10];
            for(var i = 0; i < 10; i++)
            {
                for(var j = 0; j < 10; j++)
                {
                    tabuleiroAuxilar[i,j] = " ";
                }
            }
            do
            {
                //gerar o aleatorios
                Random posicao = new Random();
                int numeroAleatorioPosicao = posicao.Next(0,9);
                string[] entradasLetras = {"A","B","C","D","E","F","G","H","I","J"};
                string[] entradasNumericas = {"1","2","3","4","5","6","7","8","9","10"};
                coordenadas = entradasLetras[numeroAleatorioPosicao];
                numeroAleatorioPosicao = posicao.Next(0,9);
                coordenadas += entradasNumericas[numeroAleatorioPosicao];
                numeroAleatorioPosicao = posicao.Next(0,9);
                coordenadas += entradasLetras[numeroAleatorioPosicao];
                numeroAleatorioPosicao = posicao.Next(0,9);
                coordenadas += entradasNumericas[numeroAleatorioPosicao];
                controleEntrada = RegexEntrada(coordenadas);
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
                                validaEntrada = VerificaTamanho(comecoEfim, escolha,posicoesPlayer,listaQuantidadeFrota);
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

        public static void Iniciar(string player1, string[,] posicoesPlayer1, string[,] posicoesPlayer2)
        {
            Console.WriteLine($"Vamos ao jogo! Boa Sorte {player1}!");
            bool vencedor = false;
            string[,] tabuleiroParaPlayer1 = new string[10,10];
            string[,] tabuleiroParaPlayer2 = new string[10,10];
            int contagemPlayer1 = 14; 
            int contagemPlayer2 = 14; 
            var resultado = (vencedor, contagemPlayer1, tabuleiroParaPlayer1);
            
            for(var i = 0; i < 10; i++)
            {
                for(var j = 0; j < 10; j++)
                {
                    tabuleiroParaPlayer1[i,j] = " ";
                    tabuleiroParaPlayer2[i,j] = " ";
                }
            }
            
            while(!vencedor)
            {
                resultado = Jogada(player1, tabuleiroParaPlayer1, posicoesPlayer2, contagemPlayer1, vencedor);
                vencedor = resultado.Item1;
                contagemPlayer1 = resultado.Item2;
                tabuleiroParaPlayer1 = resultado.Item3;
                

                if(vencedor == false)
                {
                    resultado = Jogada(tabuleiroParaPlayer2, posicoesPlayer1, contagemPlayer2, vencedor);
                    vencedor = resultado.Item1;
                    contagemPlayer2 = resultado.Item2;
                    tabuleiroParaPlayer2 = resultado.Item3;
                }
            }
        }

        public static (bool, int, string[,]) Jogada(string[,] tabuleiroParaPlayer, string[,] posicoesPlayerOposto, int contagemPlayer, bool vencedor)
        {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Hannibal jogou");
                
                string jogadaPlayer;
                Console.ResetColor();
                //gerar o aleatorios
                Random posicao = new Random();
                int numeroAleatorioPosicao = posicao.Next(0,9);
                string[] entradasLetras = {"A","B","C","D","E","F","G","H","I","J"};
                string[] entradasNumericas = {"1","2","3","4","5","6","7","8","9","10"};
                jogadaPlayer = entradasLetras[numeroAleatorioPosicao];
                numeroAleatorioPosicao = posicao.Next(0,9);
                jogadaPlayer += entradasNumericas[numeroAleatorioPosicao];

                while(!RegexValidaJogada(jogadaPlayer))
                {
                    jogadaPlayer = entradasLetras[numeroAleatorioPosicao];
                    numeroAleatorioPosicao = posicao.Next(0,9);
                    jogadaPlayer += entradasNumericas[numeroAleatorioPosicao];
                }
                Console.Clear();
                int indice1 = -1;
                int indice2 = -1;
                if(jogadaPlayer.Length == 2)
                {
                    if(jogadaPlayer[1] == '1') indice2 = 0;
                    else if(jogadaPlayer[1] == '2') indice2 = 1;
                    else if(jogadaPlayer[1] == '3') indice2 = 2;
                    else if(jogadaPlayer[1] == '4') indice2 = 3;
                    else if(jogadaPlayer[1] == '5') indice2 = 4;
                    else if(jogadaPlayer[1] == '6') indice2 = 5;
                    else if(jogadaPlayer[1] == '7') indice2 = 6;
                    else if(jogadaPlayer[1] == '8') indice2 = 7;
                    else if(jogadaPlayer[1] == '9') indice2 = 8;
                    else indice2 = 9;

                    if(jogadaPlayer[0] == 'A') indice1 = 0;
                    else if(jogadaPlayer[0] == 'B') indice1 = 1;
                    else if(jogadaPlayer[0] == 'C') indice1 = 2;
                    else if(jogadaPlayer[0] == 'D') indice1 = 3;
                    else if(jogadaPlayer[0] == 'E') indice1 = 4;
                    else if(jogadaPlayer[0] == 'F') indice1 = 5;
                    else if(jogadaPlayer[0] == 'G') indice1 = 6;
                    else if(jogadaPlayer[0] == 'H') indice1 = 7;
                    else if(jogadaPlayer[0] == 'I') indice1 = 8;
                    else indice1 = 9;
                }
                else if(jogadaPlayer.Length == 3)
                {
                    indice2 = 9;

                    if(jogadaPlayer[0] == 'A') indice1 = 0;
                    else if(jogadaPlayer[0] == 'B') indice1 = 1;
                    else if(jogadaPlayer[0] == 'C') indice1 = 2;
                    else if(jogadaPlayer[0] == 'D') indice1 = 3;
                    else if(jogadaPlayer[0] == 'E') indice1 = 4;
                    else if(jogadaPlayer[0] == 'F') indice1 = 5;
                    else if(jogadaPlayer[0] == 'G') indice1 = 6;
                    else if(jogadaPlayer[0] == 'H') indice1 = 7;
                    else if(jogadaPlayer[0] == 'I') indice1 = 8;
                    else indice1 = 9;
                }
                bool acerto = false;
                if(posicoesPlayerOposto[indice1,indice2] == "X")
                {
                    tabuleiroParaPlayer[indice1,indice2] = "X";
                    contagemPlayer -=1;
                    acerto = true;
                }
                else
                {
                    tabuleiroParaPlayer[indice1,indice2] = "A";
                }
                if(contagemPlayer == 0)
                {
                    vencedor = true;
                    
                }
                ConstroiMapa(tabuleiroParaPlayer);
                if(acerto)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"Hannibal jogou{jogadaPlayer} e Acertou!");
                    Thread.Sleep(2000);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"Hannibal jogou {jogadaPlayer} e Errou...");
                    Thread.Sleep(1000);
                    Console.ResetColor();
                }
                if(vencedor)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" Você Perdeu para o Hannibal!!!");
                    Thread.Sleep(3000);
                    Console.ResetColor();
                }  
                Console.WriteLine("trocando turno ...");
                Thread.Sleep(2000);
                if(!vencedor)
                {
                    Console.Clear();
                }

                return (vencedor, contagemPlayer, tabuleiroParaPlayer);
        }
    }
}