namespace WebAPIToolkit.Model.Database
{
    /// <summary>
    /// EntityFrameowrk database provider
    /// </summary>
    public class DbProvider : IDbProvider
    {
        /// <summary>
        /// Get ApplicationContext
        /// </summary>
        /// <returns></returns>
        public ApplicationContext GetModelContext()
        {
            return new ApplicationContext();
        }
    }
}