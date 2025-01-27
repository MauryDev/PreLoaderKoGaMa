namespace PreLoaderKoGaMa.Helpers
{
    internal class ConsoleHelper
    {
        public static void WriteMessage(string title, string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"[{title}]: ");
            Console.ResetColor();
            Console.WriteLine(message);
        }
    }
}
