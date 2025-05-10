namespace IndexedAssets {
    public interface IHasIndexProperty<IndexType> {
        IndexType Index { get; set; }
    }
}