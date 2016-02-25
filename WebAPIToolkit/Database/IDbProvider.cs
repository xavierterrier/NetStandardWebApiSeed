namespace WebAPIToolkit.Database
{
    /// <summary>
    ///  The database provider interface
    /// </summary>
    public interface IDbProvider
    {
        /// <summary>
        /// Get ApplicationContext
        /// </summary>
        /// <returns></returns>
        ApplicationContext GetModelContext();
    }
}