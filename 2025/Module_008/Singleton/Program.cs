namespace Singleton
{
    class Page
    {
        double width = 10;
        double height = 20;
        private Page()
        {
            Console.WriteLine("Calling Page() ctor");
        }

        private static Page? thePage = null;
        public static Page getThePage()
        {
            if (thePage == null)
            {
                thePage = new Page();
            }
            return thePage;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("In Main()");
            Page p1 = Page.getThePage();
            Page p2 = Page.getThePage();
        }
    }
}
