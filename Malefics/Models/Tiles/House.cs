namespace Malefics.Models.Tiles
{
    public class House : Tile, ITile
    {
        #region Overrides of Tile

        /// <inheritdoc />
        public override bool IsTraversable() => false;

        #endregion
    }
}