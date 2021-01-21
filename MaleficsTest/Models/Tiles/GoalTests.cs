using Malefics.Models.Tiles;
using NUnit.Framework;

namespace MaleficsTests.Models.Tiles
{
    [TestFixture]
    public class GoalTests
    {
        [Test]
        public void A_Goal_Is_Traversable()
            => Assert.That(new Goal().IsTraversable(), Is.True);

        [Test]
        public void A_Goal_Is_Geometrically_Traversable()
            => Assert.That(new Goal().IsGeometricallyTraversable(), Is.True);
    }
}
