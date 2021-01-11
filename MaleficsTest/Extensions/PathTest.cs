using Malefics.Extensions;
using Malefics.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace MaleficsTest.Extensions
{
    public class PathTest
    {
        [Test]
        public void Empty_Is_A_Path()
            => Assert.IsTrue(Enumerable.Empty<Position>().IsPath());

        [Test]
        public void Single_Position_Is_A_Path()
            => Assert.IsTrue(new[] { new Position(0, 0) }.IsPath());

        [Test]
        public void Non_Neighboring_Positions_Are_Not_A_Path()
            => Assert.IsFalse(new[] { new Position(0, 0), new(1, 1) }.IsPath());

        [Test]
        public void Neighboring_Positions_Are_A_Path()
            => Assert.IsTrue(new[] { new Position(0, 0), new(0, 1), new(1, 1) }.IsPath());

        [Test]
        public void Axis_Parallel_Paths_In_All_Directions()
        {
            var path = Path.AxisParallel(new(0, 0), new(2, 0));
            Assert.That(path, Is.EquivalentTo(new[] {
                new Position(0, 0), new(1, 0), new(2, 0) }));

            path = Path.AxisParallel(new(0, 0), new(0, -2));
            Assert.That(path, Is.EquivalentTo(new[] {
                new Position(0, 0), new(0, -1), new(0, -2) }));

            path = Path.AxisParallel(new(-1, -1), new(-3, -1));
            Assert.That(path, Is.EquivalentTo(new[] {
                new Position(-1, -1), new(-2, -1), new(-3, -1) }));

            path = Path.AxisParallel(new(-1, -1), new(-1, 1));
            Assert.That(path, Is.EquivalentTo(new[] {
                new Position(-1, -1), new(-1, 0), new(-1, 1) }));
        }

        [Test]
        public void Paths_That_Connect_Can_Be_Joined()
        {
            var p = Path.AxisParallel(new(0, 0), new(2, 0));
            var q = Path.AxisParallel(new(2, 0), new(2, 1));

            var expectedJoinedPath = new Position[]
            {
                new(0, 0),
                new(1, 0),
                new(2, 0),
                new(2, 1)
            };

            Assert.That(p.JoinPathTo(q), Is.EquivalentTo(expectedJoinedPath));
        }

        [Test]
        public void Empty_Path_Is_Neutral_Element_In_Joining()
        {
            var p = Path.AxisParallel(new(0, 0), new(0, 1));
            var e = Enumerable.Empty<Position>();

            Assert.That(p.JoinPathTo(e), Is.EqualTo(p));
            Assert.That(e.JoinPathTo(p), Is.EqualTo(p));
        }

        [Test]
        public void Joining_Paths_Throws_If_They_Are_Not_Connected()
        {
            var p = Path.AxisParallel(new(0, 0), new(2, 0));
            var q = Path.AxisParallel(new(1, 1), new(1, -1));

            Assert.Catch<ArgumentException>(() => p.JoinPathTo(q));
        }

        [Test]
        public void Create_Path_From_Segments_Through_Endpoints()
        {
            var p = Path.AxisParallelSegments(new(0, 0), new(1, 0), new(1, 3));

            var expectedPath = new Position[]
            {
                new(0, 0),
                new(1, 0),
                new(1, 1),
                new(1, 2),
                new(1, 3)
            };

            Assert.That(p, Is.EquivalentTo(expectedPath));
        }

        [Test]
        public void Segments_From_Less_Than_Two_Endpoints_Throws()
        {
            Assert.Catch<ArgumentException>(() => Path.AxisParallelSegments());
            Assert.Catch<ArgumentException>(
                () => Path.AxisParallelSegments(new Position(0, 0)));
        }
    }
}
