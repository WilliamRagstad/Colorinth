using System;

namespace Colorinth
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Colorinth())
                game.Run();
        }
    }
}
