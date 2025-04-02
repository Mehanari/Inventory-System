namespace InventorySystem.Core
{
    /// <summary>
    /// Demands a class to have an id.
    /// Does not demand it to guaranty the uniqueness of an object though.
    /// </summary>
    public interface IUnique
    {
        public string Id { get; }
    }
}