using System;
using System.Linq;

namespace Redux.DevTools
{
    /// Todo : Refactor to use StoreEnhancer
    public class TimeMachineStore<TState> : Store<TimeMachineState>, IStore<TState>
    {
        public TimeMachineStore(Reducer<TState> reducer, TState initialState = default(TState), params Middleware<TState>[] middlewares)
            : base(new TimeMachineReducer((state, action) => reducer((TState)state, action)).Execute, new TimeMachineState(initialState), WrapMiddlewares(middlewares))
        {
        }

        private static Middleware<TimeMachineState>[] WrapMiddlewares(Middleware<TState>[] middlewares)
        {
            return middlewares.Select(WrapMiddleware).ToArray();
        }

        private static Middleware<TimeMachineState> WrapMiddleware(Middleware<TState> middleware)
        {
            return store => middleware.Invoke((IStore<TState>)store);
        }

        TState IStore<TState>.GetState()
        {
            return Unlift(GetState());
        }

        private TState Unlift(TimeMachineState state)
        {
            return (TState)state.States[state.Position];
        }
    }
}