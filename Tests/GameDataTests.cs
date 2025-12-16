using NUnit.Framework;

using uGameData;

namespace SharedTests {
    internal class GameDataTests {
        // Definitions
        internal class TestableGameData : GameData<uint>
        {
            public TestableGameData() : base(default) { }
        }

        // Methods
        // Public
        [Test]
        public void Instantiate() {
            TestableGameData entry = new();
            Assert.IsNotNull(entry, "Failed to instantiate object");

            entry.Index = 123;
            Assert.IsTrue(entry.Index == 123, "IndexedEntry index was not modified");
        }
    }
}
