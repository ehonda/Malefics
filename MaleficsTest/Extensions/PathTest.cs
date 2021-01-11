using Malefics.Extensions;
using Malefics.Models;
using NUnit.Framework;
using System;

namespace MaleficsTest.Extensions
{
    public class PathTest
    {
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
        public void Joining_Paths_Throws_If_They_Are_Not_Connected()
        {
            var p = Path.AxisParallel(new(0, 0), new(2, 0));
            var q = Path.AxisParallel(new(1, 1), new(1, -1));

            Assert.Catch<ArgumentException>(() => p.JoinPathTo(q));
        }
    }
}
