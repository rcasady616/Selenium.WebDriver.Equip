using System.Linq;
using static NUnit.Framework.TestContext;

namespace NUnit.Framework
{
    public static class TestAdapterExtension
    {
        public static string GetCleanName(this TestAdapter testAdapter)
        {
            var fullName = testAdapter.FullName;
            if (fullName.Contains("("))
                fullName = fullName.Substring(0, fullName.LastIndexOf("("));
            var justName = fullName.Split('.').Last();
            return justName;
        }
    }
}
