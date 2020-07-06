import { combineReducers } from "redux";
import userReducer from '../app/redux/user'
const rootReducer = combineReducers({
    user:userReducer
});
export default rootReducer;
