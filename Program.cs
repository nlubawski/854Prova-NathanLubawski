using System;

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



        }

        public static string TipoDaEmbarcacao(List lista)
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
            
            if(validador == true)
            {
                switch (escolha)
                {
                    case "PS":
                    if(lista[0] > 0) lista[0] -= 1;
                    else validador = false;
                    break;

                    case "NT":
                    if(lista[0] > 0) lista[1] -= 1;
                    else validador = false;
                    break;

                    case "DS":
                    if(lista[0] > 0) lista[2] -= 1;
                    else validador = false;
                    break;

                    case "SB":
                    if(lista[0] > 0) lista[3] -= 1;
                    else validador = false;
                    break;

                    default: break;
                }
            }
            
            }while(!validador);
            return escolha;
        }

        public static int PosicoesNoMapa(string player)
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

            return posicoes;
        }

    }
}