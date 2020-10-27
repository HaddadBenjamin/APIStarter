namespace WriteModel.Domain.Tools.Resolvers
{
    public interface IResolver<ResolveResult>
    {
        ResolveResult Resolve();
    }
}
