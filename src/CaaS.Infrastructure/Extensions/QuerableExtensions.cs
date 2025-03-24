namespace CaaS.Infrastructure.Extensions;

public static class QuerableExtensions
{
    public static IQueryable<T> ApplyTracking<T>(this IQueryable<T> query, bool trackChanges)
        where T : class =>
        trackChanges ? query : query.AsNoTracking();

}
