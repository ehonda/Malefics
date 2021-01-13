using Malefics.Models;
using Sprache;
using System;

namespace Malefics.Parsers.Ascii
{
    public class TileParser
    {
        private const char ROCK = ' ';
        private const char ROAD = '.';
        private const char BARRICADE = 'o';
        private const char PLAYER_RED = 'r';

        public Tile Parse(char tile)
            => tile switch
            {
                ROCK => Grammar.Rock.Parse(tile.ToString()),
                ROAD => Grammar.Road.Parse(tile.ToString()),
                BARRICADE => Grammar.Barricade.Parse(tile.ToString()),
                PLAYER_RED => Grammar.RedPawn.Parse(tile.ToString()),
                _ => throw new ArgumentException($"Unkown tile encoding: {tile}")
            };
    }
}
