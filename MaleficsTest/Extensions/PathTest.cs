using Malefics.Extensions;
using Malefics.Models;
using NUnit.Framework;

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
    }
}
