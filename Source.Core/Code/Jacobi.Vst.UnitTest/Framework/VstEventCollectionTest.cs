using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jacobi.Vst.UnitTest.Framework
{
    /// <summary>
    ///This is a test class for VstEventCollectionTest and is intended
    ///to contain all VstEventCollectionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VstEventCollectionTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for VstEventCollection read-only Constructor
        ///</summary>
        [TestMethod()]
        public void Test_VstEventCollection_ReadOnlyConstructor()
        {
            VstEvent[] events = new VstEvent[2];
            events[0] = new VstMidiEvent(0, 100, 0, new byte[] { 100, 110, 120 }, 0, 0);
            events[1] = new VstMidiEvent(0, 100, 0, new byte[] { 100, 110, 120 }, 0, 0);
            
            VstEventCollection target = new VstEventCollection(events);

            Assert.AreEqual(events.Length, target.Count, "Count does not match.");
            Assert.AreEqual(2, target.Count, "Count is not as expected.");
            Assert.IsTrue(target.IsReadOnly, "Collection is not read-only.");
            Assert.AreEqual(events[0], target[0], "First item does not match.");
            Assert.AreEqual(events[1], target[1], "Second item does not match.");
        }

        /// <summary>
        ///A test for VstEventCollection Constructor
        ///</summary>
        [TestMethod()]
        public void Test_VstEventCollection_Constructor()
        {
            VstEventCollection target = new VstEventCollection();

            Assert.IsFalse(target.IsReadOnly, "Collection is read-only.");

            target.Add(new VstMidiEvent(0, 100, 0, new byte[] { 100, 110, 120 }, 0, 0));
            target.Add(new VstMidiEvent(0, 100, 0, new byte[] { 100, 110, 120 }, 0, 0));

            Assert.AreEqual(2, target.Count, "Count is not as expected.");

            target.Clear();
            Assert.AreEqual(0, target.Count, "Count is not zero.");
        }

        [TestMethod()]
        public void Test_VstEventCollection_CollectionChanged_Add()
        {
            VstEventCollection target = new VstEventCollection();
            int callCount = 0;

            target.CollectionChanged += (sender, e) =>
                {
                    Assert.AreEqual(NotifyCollectionChangedAction.Add, e.Action, "Unexpected collection changed action.");
                    Assert.IsNotNull(e.NewItems, "NewItems collection is null.");
                    Assert.AreEqual(1, e.NewItems.Count, "Not the expected number of items in the NewItems collection.");
                    Assert.IsNotNull(e.NewItems[0], "NewItems[0] is null.");
                    Assert.IsNotNull(e.OldItems, "OldItems collection is null.");
                    Assert.AreEqual(0, e.OldItems.Count, "Not the expected number of items in the OldItems collection.");

                    callCount++;
                };

            target.Add(new VstMidiEvent(0, 100, 0, new byte[] { 100, 110, 120 }, 0, 0));
            target.Add(new VstMidiEvent(0, 100, 0, new byte[] { 100, 110, 120 }, 0, 0));

            Assert.AreEqual(2, target.Count, "Collection Count is not as expected.");
            Assert.AreEqual(2, callCount, "Call count is not as expected.");
        }

        [TestMethod()]
        public void Test_VstEventCollection_CollectionChanged_RemoveAt()
        {
            VstEventCollection target = new VstEventCollection();
            target.Add(new VstMidiEvent(0, 100, 0, new byte[] { 100, 110, 120 }, 0, 0));
            target.Add(new VstMidiEvent(0, 100, 0, new byte[] { 100, 110, 120 }, 0, 0));

            Assert.AreEqual(2, target.Count, "Collection Count is not as expected.");

            int callCount = 0;

            target.CollectionChanged += (sender, e) =>
            {
                Assert.AreEqual(NotifyCollectionChangedAction.Remove, e.Action, "Unexpected collection changed action.");
                Assert.IsNotNull(e.NewItems, "NewItems collection is null.");
                Assert.AreEqual(0, e.NewItems.Count, "Not the expected number of items in the NewItems collection.");
                Assert.IsNotNull(e.OldItems, "OldItems collection is null.");
                Assert.AreEqual(1, e.OldItems.Count, "Not the expected number of items in the OldItems collection.");
                Assert.IsNotNull(e.OldItems[0], "OldItems[0] is null.");

                callCount++;
            };

            target.RemoveAt(0);
            target.RemoveAt(0);

            Assert.AreEqual(2, callCount, "Call count is not as expected.");
        }

        [TestMethod()]
        public void Test_VstEventCollection_CollectionChanged_Replace()
        {
            VstEventCollection target = new VstEventCollection();
            target.Add(new VstMidiEvent(0, 100, 0, new byte[] { 100, 110, 120 }, 0, 0));
            target.Add(new VstMidiEvent(0, 100, 0, new byte[] { 100, 110, 120 }, 0, 0));

            Assert.AreEqual(2, target.Count, "Collection Count is not as expected.");

            int callCount = 0;

            target.CollectionChanged += (sender, e) =>
            {
                Assert.AreEqual(NotifyCollectionChangedAction.Replace, e.Action, "Unexpected collection changed action.");
                Assert.IsNotNull(e.NewItems, "NewItems collection is null.");
                Assert.AreEqual(1, e.NewItems.Count, "Not the expected number of items in the NewItems collection.");
                Assert.IsNotNull(e.NewItems[0], "NewItems[0] is null.");
                Assert.IsNotNull(e.OldItems, "OldItems collection is null.");
                Assert.AreEqual(1, e.OldItems.Count, "Not the expected number of items in the OldItems collection.");
                Assert.IsNotNull(e.OldItems[0], "OldItems[0] is null.");

                callCount++;
            };

            VstEvent @event = target[0];

            target[0] = target[1];
            target[1] = @event;

            Assert.AreEqual(2, callCount, "Call count is not as expected.");
        }
    }
}
