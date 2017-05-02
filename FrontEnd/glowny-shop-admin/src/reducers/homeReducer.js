import * as types from '../actions/actionTypes.js';

const defaultState = {
  onProgress: false,
};

export default function(state = defaultState, action) {
  switch (action.type) {

    case types.UPDATE_HOME_ON_PROGRESS:
      return {
        ...state,
        onProgress: action.onProgress,
      }

    default: return state;
  }
}
