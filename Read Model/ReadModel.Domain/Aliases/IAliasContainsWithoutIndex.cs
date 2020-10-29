namespace ReadModel.Domain.Aliases
{
    public interface IAliasContainsWithoutIndex
    {
        bool Contains(IndexType indexType);
    }
}