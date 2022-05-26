using System;

namespace batalha_naval
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Jogar contra o computador digite 1");
            Console.WriteLine("Jogar contra adversário real digite 2");
            var opcao = Console.ReadLine();

            //tratar caso contra outro player primeiro
            Console.WriteLine("Digite o nome do player 1");
            var player1 = Console.ReadLine();

            Console.WriteLine("Digite o nome do player 2");
            var player2 = Console.ReadLine();

            int portaAvioes = 1;
            int navioTanque = 2;
            int destroyers = 3;
            int submarinos = 4;

            do
            {
                Console.WriteLine("Qual o tipo de embarcação? Aperte");
                Console.WriteLine("PS para Porta-Aviões"); //tem 1 e ocupa 5 espacos
                Console.WriteLine("NT para Navio-Tanque"); //tem 2 e ocupa 4 espacos
                Console.WriteLine("DS para Destroyers"); //tem 3 e ocupa 3 espacos
                Console.WriteLine("SB para Submarinos"); //tem 4 e ocupa 2 espacos
                
                var escolha = Console.ReadLine();

                switch (escolha)
                {
                    case "PS":
                    Console.WriteLine("PS");
                    break;

                    case "NT":
                    Console.WriteLine("NT");
                    break;

                    case "DS":
                    Console.WriteLine("DS");
                    break;

                    case "SB":
                    Console.WriteLine("SB");
                    break;

                    default:
                    Console.WriteLine("DEU RUIM");
                    break;
                }

                //escolha = embarcacaoEscolhida.escolha;
                //Console.WriteLine(escolha);
                //bool tipoDaEmbarcacao = int.TryParse(escolha, out escolha);

                //Console.WriteLine(escolha);

            }while(false);
    
            // var embarcacao = Console.ReadLine();
            // // input: SB
            // Console.WriteLine("Qual a sua posição?");
            // // input: H1H2
            // Console.WriteLine("Qual o tipo de embarcação?");
            // var embarcacao = Console.ReadLine();
            // // input: SB
            // Console.WriteLine("Qual a sua posição?")
            // // input: F6G6
        
            // //INFO PLAY2
            // Console.WriteLine("Qual o tipo de embarcação?");
            // var embarcacao = Console.ReadLine();
            // // input: SB
            // Console.WriteLine("Qual a sua posição?");
            // // input: H1H2
            // Console.WriteLine("Qual o tipo de embarcação?");
            // var embarcacao = Console.ReadLine();
            // // input: SB
            // Console.WriteLine("Qual a sua posição?");
            // // input: F6G6
        }
    }
}