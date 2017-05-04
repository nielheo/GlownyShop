import { combineReducers } from 'redux'
import homeReducer from './homeReducer'
import queryReducer from './queryReducer'


export default combineReducers({
  homeReducer,
  queryReducer,
});