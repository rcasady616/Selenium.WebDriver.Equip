namespace SeleniumExtention
{
    public interface IPage
    {
        /// <summary>
        /// Gets the value indicating whether or not this page has loaded
        /// </summary>
        /// <returns>A <see cref="bool"/> if it is loaded or not</returns>
        bool IsPageLoaded();
    }
}
