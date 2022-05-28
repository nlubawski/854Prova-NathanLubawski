using System;
using System.Collections.Generic;
using System.Linq;

namespace batalha_naval
{
    class Program
    {
        static void Main(string[] args)
        {
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
            Console.WriteLine("Digite o nome do player 1");
            var player1 = Console.ReadLine();

            Console.WriteLine("Digite o nome do player 2");
            var player2 = Console.ReadLine();

            var posicoesPlayer1 = PosicoesNoMapa(player1);
            var posicoesPlayer2 = PosicoesNoMapa(player2);
        }

        public static string[,] PosicoesNoMapa(string player)
        {
            var lista = new List<int>{1,2,3,4}; //portaAvioes,navioTanque,destroyers,submarinos = 4;
            var posicoesPlayer = new string[10,10];
            var resultado = new Tuple<List<int>, string[,]>(lista, posicoesPlayer);

            while(lista.Sum() > 0)
            {
            Console.WriteLine($"Bem vindo {player}!!!");
            Console.WriteLine(@$"Você tem disponível {lista[0]} Porta Aviões, 
            {lista[1]} Navio Tanque, {lista[2]} Destroyers, {lista[3]} Submarinos");

            resultado = TipoDaEmbarcacao(lista, posicoesPlayer);
            lista = resultado.Item1;
            posicoesPlayer = resultado.Item2;
            //o verifica no mapa vai ter que vir antes pra pedir pra reescrever a entrada
            //o caso abaixo ja e do sucesso
            }

            return posicoesPlayer;
        }         

        public static Tuple<List<int>, string[,]> TipoDaEmbarcacao(List<int> lista, string[,] posicoesPlayer)
        {
            bool validador = false;
            var teste = new string[2];
            bool validaQuantidade = false;
            string escolha;
            string[,] posicoes = posicoesPlayer;
            do
            {
            Console.WriteLine("Qual o tipo de embarcação? Digite");
            Console.WriteLine("PS para Porta-Aviões"); //tem 1 e ocupa 5 espacos
            Console.WriteLine("NT para Navio-Tanque"); //tem 2 e ocupa 4 espacos
            Console.WriteLine("DS para Destroyers"); //tem 3 e ocupa 3 espacos
            Console.WriteLine("SB para Submarinos"); //tem 4 e ocupa 2 espacos
            escolha = Console.ReadLine();
            validador = escolha == "PS" || escolha == "NT" || escolha == "DS" || escolha == "SB";
           
            if(validador == true)
            {
                if(escolha == "PS")
                {
                    if(lista[0] > 0)
                    {
                        lista[0] -=1;
                        validaQuantidade = true;
                    }
                    else
                    {
                        Console.WriteLine("Você já colocou seu Porta-Aviões");
                        validador = false;
                    }
                }
                else if(escolha == "NT")
                {
                    if(lista[1] > 0)
                    {
                        lista[1] -=1;
                        validaQuantidade = true;
                    }
                    else
                    {
                        Console.WriteLine("Você já colocou seus Navios-Tanque");
                        validador = false;
                    }
                }
                else if(escolha == "DS")
                {
                    if(lista[2] > 0)
                    {
                        lista[2] -=1;
                        validaQuantidade = true;
                    }
                    else
                    {
                        Console.WriteLine("Você já colocou seus Destroyers");
                        validador = false;
                    }
                }
                else
                {
                    if(lista[3] > 0)
                    {
                        lista[3] -=1;
                        validaQuantidade = true;
                    }
                    else
                    {
                        Console.WriteLine("Você já colocou seus Submarinos");
                        validador = false;
                    }
                }
            }
            if(validaQuantidade)
            {
                posicoes = VerificaCoordenadas(escolha,posicoesPlayer);
            }
            
            }while(!validador && validaQuantidade);

            var retornar = new Tuple<List<int>, string[,]>(lista, posicoes);
            return retornar;
        }

        public static string[,] VerificaCoordenadas(string escolha, string[,] posicoesPlayer) //void temp
        { 
            bool validaTamanho = true;
            do
            {
                Console.WriteLine("Qual a sua posição?");
                string coordenadas = Console.ReadLine();
                string letras = "ABCDEFGHIJ";
                string numeros = "1 2 3 4 5 6 7 8 9 10";
                string[] comecoEfim = new string[2];
                int tipoEntrada = 0; //1 pra length 4, 2 pra length 5(), 3 pra 5() e 4 pra 6
                
                if(coordenadas.Length == 4)
                {
                    if(!letras.Contains(coordenadas[0]) || !letras.Contains(coordenadas[2])) validaTamanho = false;

                    if(!numeros.Contains(coordenadas[1]) || !numeros.Contains(coordenadas[3])) validaTamanho = false;
                    
                    if(validaTamanho)
                    {
                        comecoEfim[0] = $"{coordenadas[0]}{coordenadas[1]}";
                        comecoEfim[1] = coordenadas.Substring(2);                    
                        validaTamanho = VerificaTamanho(comecoEfim, escolha);
                        tipoEntrada = 1;
                    }                    
                }

                if(coordenadas.Length == 6)
                {
                    if(!letras.Contains(coordenadas[0]) || !letras.Contains(coordenadas[3])) validaTamanho = false;
                    if(!numeros.Contains($"{coordenadas[1]}{coordenadas[2]}") || !numeros.Contains($"{coordenadas[4]}{coordenadas[5]}")) validaTamanho = false;
                    if(validaTamanho)
                    {
                        comecoEfim[0] = coordenadas.Substring(0,3);
                        comecoEfim[1] = coordenadas.Substring(3,6);
                        validaTamanho = VerificaTamanho(comecoEfim, escolha);
                        tipoEntrada = 4;
                    }
                }

                if(coordenadas.Length == 5)
                {   
                    if(letras.Contains(coordenadas[2]))
                    {
                        if(!letras.Contains(coordenadas[0])) validaTamanho = false;
                        if(!numeros.Contains(coordenadas[1]) || !numeros.Contains($"{coordenadas[3]}{coordenadas[4]}")) validaTamanho = false;
                        if(validaTamanho)
                        {
                            comecoEfim[0] = coordenadas.Substring(0,2);
                            comecoEfim[1] = coordenadas.Substring(2,5);
                            validaTamanho = VerificaTamanho(comecoEfim, escolha);
                            tipoEntrada = 2;
                        }
                    }
                    else if(numeros.Contains(coordenadas[2]))
                    {
                        if(!letras.Contains(coordenadas[0]) || !letras.Contains(coordenadas[3])) validaTamanho = false;
                        if(!numeros.Contains($"{coordenadas[1]}{coordenadas[2]}") || !numeros.Contains(coordenadas[4])) validaTamanho = false;
                        if(validaTamanho)
                        {
                            comecoEfim[0] = coordenadas.Substring(0,3);
                            comecoEfim[1] = coordenadas.Substring(3,5);
                            validaTamanho = VerificaTamanho(comecoEfim, escolha);
                            tipoEntrada = 3;
                        }
                    }
                }

                if(validaTamanho)
                    {   
                        int inicial;
                        int final;
                        if(tipoEntrada == 1)
                        {   
                            Console.WriteLine($"tete {comecoEfim[0][0]}");
                            
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

                            // if(inicial == final)
                            // {
                            //     int maior = 0;
                            //     int menor = 0;
                                //var teste1 = int.Parse(comecoEfim[0][1]);
                                //var teste2 = int.Parse(comecoEfim[1][1]);
                                // if(teste1 > teste2)
                                // {
                                //     maior = teste1;
                                //     menor = teste2;
                                // }
                                // else
                                // {
                                    //maior = int.Parse(comecoEfim[1][1]);
                                   // menor = int.Parse(comecoEfim[0][1]);
                                //}

                                // for(var i = menor; i <= maior; i++ )
                                // {
                                //     posicoesPlayer[inicial,i] = "A";
                                // }
                            // }
                            // else
                            // {
                            //     //fixa segunda e faz conta na primeira
                            // }
                        }
                        // if(coordenadas[0][0] == coordenadas[1][0])
                        // {
                        //     //identifica qual a letra pra poder
                        // }

                        //caso A1A5
                        for(var i = 1; i <=5; i++)
                        {
                            posicoesPlayer[0,i] = "X";
                        }
                        break;
                    }

            }while(!validaTamanho);
            return posicoesPlayer;
        }

        public static bool VerificaTamanho(string[] comecoEfim, string escolha)
        {
            if(comecoEfim[0][0] != comecoEfim[1][0] && comecoEfim[0][1] != comecoEfim[1][1])
            {
                return false;
            }
            
            int tamanhoPermitido = 0;
            if(escolha == "PS") tamanhoPermitido = 5;
            if(escolha == "NT") tamanhoPermitido = 4;
            if(escolha == "DS") tamanhoPermitido = 3;
            if(escolha == "SB") tamanhoPermitido = 2;
            
            if(comecoEfim[0][0] == comecoEfim[1][0])
            {   
                if(tamanhoPermitido - Math.Abs(comecoEfim[0][1] - comecoEfim[1][1]) != 0)
                {
                    return false;
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

                if(tamanhoPermitido - Math.Abs(final - inicial) != 0)
                {
                    return false;
                } 
            }

            return true;


        }

        public static string[,] ColocaNoMapa(string[,] posicoesPlayer, string[] comecoEfim )
        {
            Console.WriteLine(comecoEfim[0]);
            Console.WriteLine(comecoEfim[1]);

            return posicoesPlayer;
        }

        // public static void ConstroiMapa(){
        //     //string[,] mapa = new string[10, 10];
        //     int[,] mapa = new int[10, 10];

        //     Console.WriteLine("   |" + " 1 " + " 2 " + " 3 " + " 4 " + " 5 " + " 6 " + " 7 " + " 8 " + " 9 " + " 10 ");
        //     Console.WriteLine("------------------------------------");
        //     Console.WriteLine($" A | {mapa[0, 0]}  {mapa[0, 1]}  {mapa[0, 2]}  {mapa[0, 3]}  {mapa[0, 4]}  {mapa[0, 5]}  {mapa[0, 6]}  {mapa[0, 7]}  {mapa[0, 8]}  {mapa[0, 9]}");
        //     Console.WriteLine("   |");
        //     Console.WriteLine($" B | {mapa[1, 0]}  {mapa[1, 1]}  {mapa[1, 2]}  {mapa[1, 3]}  {mapa[1, 4]}  {mapa[1, 5]}  {mapa[1, 6]}  {mapa[1, 7]}  {mapa[1, 8]}  {mapa[1, 9]}");
        //     Console.WriteLine("   |");
        //     Console.WriteLine($" C | {mapa[2, 0]}  {mapa[2, 1]}  {mapa[2, 2]}  {mapa[2, 3]}  {mapa[2, 4]}  {mapa[2, 5]}  {mapa[2, 6]}  {mapa[2, 7]}  {mapa[2, 8]}  {mapa[2, 9]}");
        //     Console.WriteLine("   |");
        //     Console.WriteLine($" D | {mapa[3, 0]}  {mapa[3, 1]}  {mapa[3, 2]}  {mapa[3, 3]}  {mapa[3, 4]}  {mapa[3, 5]}  {mapa[3, 6]}  {mapa[3, 7]}  {mapa[3, 8]}  {mapa[3, 9]}");
        //     Console.WriteLine("   |");
        //     Console.WriteLine($" E | {mapa[4, 0]}  {mapa[4, 1]}  {mapa[4, 2]}  {mapa[4, 3]}  {mapa[4, 4]}  {mapa[4, 5]}  {mapa[4, 6]}  {mapa[4, 7]}  {mapa[4, 8]}  {mapa[4, 9]}");
        //     Console.WriteLine("   |");
        //     Console.WriteLine($" F | {mapa[5, 0]}  {mapa[5, 1]}  {mapa[5, 2]}  {mapa[5, 3]}  {mapa[5, 4]}  {mapa[5, 5]}  {mapa[5, 6]}  {mapa[5, 7]}  {mapa[5, 8]}  {mapa[5, 9]}");
        //     Console.WriteLine("   |");
        //     Console.WriteLine($" G | {mapa[6, 0]}  {mapa[6, 1]}  {mapa[6, 2]}  {mapa[6, 3]}  {mapa[6, 4]}  {mapa[6, 5]}  {mapa[6, 6]}  {mapa[6, 7]}  {mapa[6, 8]}  {mapa[6, 9]}");
        //     Console.WriteLine("   |");
        //     Console.WriteLine($" H | {mapa[7, 0]}  {mapa[7, 1]}  {mapa[7, 2]}  {mapa[7, 3]}  {mapa[7, 4]}  {mapa[7, 5]}  {mapa[7, 6]}  {mapa[7, 7]}  {mapa[7, 8]}  {mapa[7, 9]}");
        //     Console.WriteLine("   |");
        //     Console.WriteLine($" I | {mapa[8, 0]}  {mapa[8, 1]}  {mapa[8, 2]}  {mapa[8, 3]}  {mapa[8, 4]}  {mapa[8, 5]}  {mapa[8, 6]}  {mapa[8, 7]}  {mapa[8, 8]}  {mapa[8, 9]}");
        //     Console.WriteLine("   |");
        //     Console.WriteLine($" J | {mapa[0, 0]}  {mapa[9, 1]}  {mapa[9, 2]}  {mapa[9, 3]}  {mapa[9, 4]}  {mapa[9, 5]}  {mapa[9, 6]}  {mapa[9, 7]}  {mapa[9, 8]}  {mapa[9, 9]}");

        // }

        // public static bool VerificaDisponibilidadeNoMapa(string palavra, int[,] mapa)
        // {
        //     string letras = "ABCDEFGHIJ";
        //     string numeros = "1 2 3 4 5 6 7 8 9 10";
            
        //     if(palavra.Length == 4)
        //     {
        //         string comeco = palavra.Substring(0,2);
        //         string fim = palavra.Substring(2,4);

        //         if(comeco[0] == fim[0] )
        //         {
                    
        //         }

        //         if(comeco[1] == fim[1] )
        //         {

        //         }

        //         return false;
        //     }

        //     if(palavra.Length == 6)
        //     {
        //         //corta em duas e compara
        //     }

        //     if(palavra.Length == 5)
        //     {   

        //         //verifica o caso em dois e corta de acordo

        //         if(letras.Contains(palavra[2]))
        //         {
        //             if(!letras.Contains(palavra[0])) return false;
        //             if(!numeros.Contains(palavra[1]) || !numeros.Contains($"{palavra[3]}{palavra[4]}")) return false;
        //         }
        //         else(numeros.Contains(palavra[2]))
        //         {
        //             if(!letras.Contains(palavra[0]) || !letras.Contains(palavra[3])) return false;
        //             if(!numeros.Contains($"{palavra[1]}{palavra[2]}") || !numeros.Contains(palavra[4])) return false;
        //         }
        //     }
        // }
    }
}