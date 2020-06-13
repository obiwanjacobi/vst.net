using FluentAssertions;
using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Specialized;

namespace Jacobi.Vst.UnitTest.Framework
{
    /// <summary>
    ///This is a test class for VstEventCollectionTest and is intended
    ///to contain all VstEventCollectionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VstEventCollectionTest
    {
        [TestMethod()]
        public void Test_VstEventCollection_ReadOnlyConstructor()
        {
            var events = new VstEvent[2];
            events[0] = new VstMidiEvent(0, 100, 0, new byte[] { 100, 110, 120 }, 0, 0);
            events[1] = new VstMidiEvent(0, 100, 0, new byte[] { 100, 110, 120 }, 0, 0);

            var target = new VstEventCollection(events);

            target.Should().HaveCount(events.Length);
            target.IsReadOnly.Should().BeTrue();
            target[0].Should().Be(events[0]);
            target[1].Should().Be(events[1]);
        }

        [TestMethod()]
        public void Test_VstEventCollection_Constructor()
        {
            var target = new VstEventCollection();
            target.IsReadOnly.Should().BeFalse();

            target.Add(new VstMidiEvent(0, 100, 0, new byte[] { 100, 110, 120 }, 0, 0));
            target.Add(new VstMidiEvent(0, 100, 0, new byte[] { 100, 110, 120 }, 0, 0));
            target.Should().HaveCount(2);

            target.Clear();
            target.Should().BeEmpty();
        }

        [TestMethod()]
        public void Test_VstEventCollection_CollectionChanged_Add()
        {
            var target = new VstEventCollection();
            int callCount = 0;

            target.CollectionChanged += (sender, e) =>
                {
                    e.Action.Should().Be(NotifyCollectionChangedAction.Add);
                    e.NewItems.Should().NotBeNullOrEmpty();
                    e.NewItems[0].Should().NotBeNull();

                    callCount++;
                };

            target.Add(new VstMidiEvent(0, 100, 0, new byte[] { 100, 110, 120 }, 0, 0));
            target.Add(new VstMidiEvent(0, 100, 0, new byte[] { 100, 110, 120 }, 0, 0));

            callCount.Should().Be(2);
        }

        [TestMethod()]
        public void Test_VstEventCollection_CollectionChanged_RemoveAt()
        {
            var target = new VstEventCollection
            {
                new VstMidiEvent(0, 100, 0, new byte[] { 100, 110, 120 }, 0, 0),
                new VstMidiEvent(0, 100, 0, new byte[] { 100, 110, 120 }, 0, 0)
            };

            target.Should().HaveCount(2);

            int callCount = 0;

            target.CollectionChanged += (sender, e) =>
            {
                e.Action.Should().Be(NotifyCollectionChangedAction.Remove);
                e.OldItems.Should().NotBeNullOrEmpty();
                e.OldItems[0].Should().NotBeNull();

                callCount++;
            };

            target.RemoveAt(0);
            target.RemoveAt(0);

            callCount.Should().Be(2);
            target.Should().BeEmpty();
        }

        [TestMethod()]
        public void Test_VstEventCollection_CollectionChanged_Replace()
        {
            var target = new VstEventCollection
            {
                new VstMidiEvent(0, 100, 0, new byte[] { 100, 110, 120 }, 0, 0),
                new VstMidiEvent(0, 100, 0, new byte[] { 100, 110, 120 }, 0, 0)
            };

            target.Should().HaveCount(2);

            int callCount = 0;

            target.CollectionChanged += (sender, e) =>
            {
                e.Action.Should().Be(NotifyCollectionChangedAction.Replace);
                e.NewItems.Should().NotBeNullOrEmpty();
                e.NewItems[0].Should().NotBeNull();
                e.OldItems.Should().NotBeNullOrEmpty();
                e.OldItems[0].Should().NotBeNull();

                callCount++;
            };

            VstEvent @event = target[0];

            target[0] = target[1];
            target[1] = @event;

            callCount.Should().Be(2);
        }
    }
}
