using Malefics.Models;
using Malefics.Models.Pieces;
using System;

namespace Malefics.Parsers.Ascii
{
    public class TileParser
    {
        private const char ROCK = ' ';
        private const char ROAD = '.';
        private const char BARRICADE = 'o';

        public Tile Parse(char tile)
            => tile switch
            {
                ROCK => new() { Terrain = Terrain.Rock },
                ROAD => new() { Terrain = Terrain.Road },
                BARRICADE => new() 
                {
                    Terrain = Terrain.Road, 
                    OccupyingPiece = new Barricade() 
                },
                _ => throw new ArgumentException($"Unkown tile encoding: {tile}")
            };
    }
}
