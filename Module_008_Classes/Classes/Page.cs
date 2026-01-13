namespace Classes
{
    // Singleton Pattern
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
}
