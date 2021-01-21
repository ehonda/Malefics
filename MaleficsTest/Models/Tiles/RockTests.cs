using Malefics.Exceptions;
using Malefics.Models.Pieces;
using Malefics.Models.Tiles;
using MaleficsTests.Models.Pieces.TestCases;
using NUnit.Framework;

namespace MaleficsTests.Models.Tiles
{
    [TestFixture]
    public class RockTests
    {
        private Rock _rock = new();

        [SetUp]
        public void SetUp() => _rock = new();

        [Test]
        public void No_Piece_Can_Be_Put_On_A_Rock_Tile()
            => Assert.Catch<InvalidTileOperationException>(
                () => _rock.Put(new Barricade()));

        [Test]
        public void A_Rock_Can_Not_Be_Taken_From()
            => Assert.Catch<InvalidTileOperationException>(
                () => _rock.Take());

        [Test]
        public void A_Rock_Tile_Is_Not_Traversable()
            => Assert.That(_rock.IsTraversable, Is.False);

        [Test]
        public void A_Rock_Is_Never_Occupied()
            => Assert.That(_rock.IsOccupied, Is.False);

        [Test]
        [TestCaseSource(typeof(PieceCases), nameof(PieceCases.AllPieces))]
        public void A_Rock_Is_Not_A_Valid_Capture_Target_For_Any_Piece(Piece piece)
            => Assert.That(_rock.IsValidCaptureTargetFor(piece), Is.False);

        [Test]
        [TestCaseSource(typeof(PieceCases), nameof(PieceCases.AllPieces))]
        public void A_Rock_Contains_No_Piece(Piece piece)
            => Assert.That(_rock.Contains(piece), Is.False);

        [Test]
        public void Peeking_At_A_Rock_Returns_Null()
            => Assert.That(_rock.Peek(), Is.Null);

        [Test]
        public void A_Rock_Is_Not_Geometrically_Traversable()
            => Assert.That(_rock.IsGeometricallyTraversable(), Is.False);
    }
}
