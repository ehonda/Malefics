using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Malefics.Enums;
using Malefics.Models;

namespace Malefics.Players
{
    public class Delayed : IPlayer
    {
        public int Delay
        {
            get => _delay;
            set
            {
                if (value < 0)
                    throw new ArgumentException($"Can't set negative delay: {value}");
                _delay = value;
            }
        }

        private int _delay;
        private readonly IPlayer _base;

        public Delayed(IPlayer @base) => _base = @base;

        #region Implementation of IPlayer

        /// <inheritdoc />
        public PlayerColor PlayerColor { get; init; }

        /// <inheritdoc />
        public IEnumerable<Position> RequestPawnMove(Board board, uint pips)
            => DelayExecution(() => _base.RequestPawnMove(board, pips));

        /// <inheritdoc />
        public Position RequestBarricadePlacement(Board board)
            => DelayExecution(() => _base.RequestBarricadePlacement(board));

        #endregion

        private T DelayExecution<T>(Func<T> f)
        {
            Task.Delay(Delay).Wait();
            return f();
        }
    }
}