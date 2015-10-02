namespace WebAPIToolkit.Database
{
    public interface IDbProvider
    {
        ApplicationContext GetModelContext();
    }
}