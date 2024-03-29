using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {

        Chakimon chakimon = new Chakimon("cha-pito", type.VIF, 1);
        JsonFileManager fileManager = new JsonFileManager();
        ChakimonCatched chakimonCatched = new ChakimonCatched();

        [TestMethod]
        [DataRow(2, 18)]
        [DataRow(25, 0)]
        [DataRow(-2, 20)]
        public void TakeDamage(float damage, float expected)
        {
            chakimon.TakeDamage(damage);
            int result = (int)chakimon.pv;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow("data.json", false)]
        public void FoundFile(string path, bool expected)
        {
            bool isFound = fileManager.FoundFile(path);
            Assert.AreEqual(isFound, expected);
        }

        [TestMethod]
        [DataRow(3, 3)]
        [DataRow(7, 6)]
        public void AddChakimon(int push, int expexted)
        {
            for (int i = 0; i < push; i++)
            {
                chakimonCatched.Add(chakimon);
            }
            Assert.AreEqual(chakimonCatched.team.Count, expexted);
        }
    }
}