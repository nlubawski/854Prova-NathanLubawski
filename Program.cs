using System;

namespace batalha_naval
{
    class Program
    {
        static void Main(string[] args)
        {
            int opcaoEscolhida = Entrada();
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
    }
}