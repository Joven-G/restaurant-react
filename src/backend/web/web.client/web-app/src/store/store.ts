import { LocationChangeAction, RouterAction } from 'react-router-redux';
import createLogger from "redux-logger";
import thunk from "redux-thunk";
import { AllActions } from './actions/actions';
import { RootState, rootReducer } from './reducers/redusers';
import { Middleware, createStore, applyMiddleware } from 'redux';

type ReactRouterAction = RouterAction | LocationChangeAction;

export type RootAction = ReactRouterAction | AllActions;

export type getStateType = () => RootState;

const configureStore = () => {
	const middlewares: Middleware[] = [thunk];

	if (process.env.NODE_ENV !== 'production') {
		middlewares.push(createLogger);
	}

	return createStore(
		rootReducer,
		// { auth: auth },
		applyMiddleware(...middlewares)
	);
};


export const store = configureStore();