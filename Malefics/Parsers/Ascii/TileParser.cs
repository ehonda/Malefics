using Malefics.Enums;
using Malefics.Models;
using Malefics.Models.Pieces;
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
                ROAD => new() { Terrain = Terrain.Road },
                BARRICADE => new() 
                {
                    Terrain = Terrain.Road, 
                    OccupyingPiece = new Barricade() 
                },
                PLAYER_RED => new()
                {
                    Terrain = Terrain.Road,
                    OccupyingPiece = new Pawn { Player = Player.Red }
                },
                _ => throw new ArgumentException($"Unkown tile encoding: {tile}")
            };
    }
}
