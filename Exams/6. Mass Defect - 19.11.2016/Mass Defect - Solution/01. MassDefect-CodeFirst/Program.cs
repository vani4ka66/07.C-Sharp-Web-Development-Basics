namespace _01.MassDefect_CodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new MassDefectContext();
            using (context)
            {
                context.SaveChanges();
            }
        }
    }
}
