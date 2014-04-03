namespace SeleniumExtension.SauceLabs
{
    /// <summary>contains constants used by the tests such as the user name and password for the sauce labs</summary>
    internal static class Constants
    {
        /// <summary>name of the sauce labs account that will be used</summary>
        internal const string SAUCE_LABS_ACCOUNT_NAME = "richardcasady";
        /// <summary>account key for the sauce labs account that will be used</summary>
        internal const string SAUCE_LABS_ACCOUNT_KEY = "a6618920-425d-4f34-b9c2-1576bef9686e";

        // NOTE:
        // To change the maximum number of parallel tests edit DegreeOfParallelism in AssemblyInfo.cs

    }
}
