namespace IndexedGameData {
    public interface IIndexed<TIndex> {
        TIndex Index { get; set; }
    }
}