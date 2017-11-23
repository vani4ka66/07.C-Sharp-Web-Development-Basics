namespace PhotographyWorkshop.Utilities
{
    using System.Linq;

    public static class Check
    {
        public static bool ForNull(params object[] objects)
        {
            return objects.Any(o => o == null);
        }
        public static bool ForNotNull(params object[] objects)
        {
            return objects.Any(o => o != null);
        }
    }
}
