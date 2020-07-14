import { combineReducers } from "redux";
import userReducer from '../app/redux/user'
import orderReducer from '../app/redux/order'
import historyReducer from '../app/redux/history'
const rootReducer = combineReducers({
    user:userReducer,
    order:orderReducer,
    history:historyReducer
});
export default rootReducer;
