namespace WriteModel.Domain.Tools.Resolvers
{
    public interface IResolver<TResolveResult>
    {
        TResolveResult Resolve();
    }

    public interface IResolver<in TResolverParameter, TResolveResult>
    {
        TResolveResult Resolve(TResolverParameter parameters);
    }
}
