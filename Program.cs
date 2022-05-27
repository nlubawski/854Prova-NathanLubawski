using System;
using System.Collections.Generic;
using System.Linq;

namespace batalha_naval
{
    class Program
    {
        static void Main(string[] args)
        {
            // int opcaoEscolhida = Entrada();

            // if (opcaoEscolhida == 2) Jogar2();
            ConstroiMapa();
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

            //var posicoesPlayer1 = PosicoesNoMapa(player1);
            //var posicoesPlayer1 = PosicoesNoMapa(player2);

            //chama o jogo e tals
            // fazer a construcao dos mapas genericos



        }

        public static string TipoDaEmbarcacao(List<int> lista)
        {
            bool validador = false;
            string escolha;
            do
            {
            Console.WriteLine("Qual o tipo de embarcação? Digite");
            Console.WriteLine("PS para Porta-Aviões"); //tem 1 e ocupa 5 espacos
            Console.WriteLine("NT para Navio-Tanque"); //tem 2 e ocupa 4 espacos
            Console.WriteLine("DS para Destroyers"); //tem 3 e ocupa 3 espacos
            Console.WriteLine("SB para Submarinos"); //tem 4 e ocupa 2 espacos
            escolha = Console.ReadLine();

            validador = escolha == "PS" || escolha == "NT" || escolha == "DS" || escolha == "SB";
            
            //if(validador == true) //AdicionaPosicao(validador, ad);
            
            }while(!validador);
            return escolha;
        }

        public static void PosicoesNoMapa(string player)
        {
            var lista = new List<int>{1,2,3,4}; //portaAvioes,navioTanque,destroyers,submarinos = 4;
            var posicoes = new List<string>();
            while(lista.Sum() > 0)
            {
            Console.WriteLine($"Bem vindo {player}!!!");
            Console.WriteLine(@$"Você tem disponível {lista[0]} Porta Aviões, 
            {lista[1]} Navio Tanque, {lista[2]} Destroyers, {lista[3]} Submarinos");

            string embarcação = TipoDaEmbarcacao(lista);
            }

            //return posicoes;
            Console.WriteLine("TESTE");
        }

        // public static void AdicionaPosicao(string tipoEmbarcacao)
        // {
        //     {
        //         bool validador;
        //         switch (escolha)
        //         {
        //             case "PS":
        //             if(lista[0] > 0) lista[0] -= 1;
        //             else validador = false;
        //             break;

        //             case "NT":
        //             if(lista[0] > 0) lista[1] -= 1;
        //             else validador = false;
        //             break;

        //             case "DS":
        //             if(lista[0] > 0) lista[2] -= 1;
        //             else validador = false;
        //             break;

        //             case "SB":
        //             if(lista[0] > 0) lista[3] -= 1;
        //             else validador = false;
        //             break;

        //             default: break;
        //         }
        //     }
        //     Console.WriteLine("teste");
        // }
        
        public static void ConstroiMapa(){
            //string[,] mapa = new string[10, 10];
            int[,] mapa = new int[10, 10];

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

    }
}