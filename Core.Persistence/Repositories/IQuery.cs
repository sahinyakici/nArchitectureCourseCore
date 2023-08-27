namespace Core.Packages.Repositories;

public interface IQuery<T>
{
    IQueryable<T> Query();
}