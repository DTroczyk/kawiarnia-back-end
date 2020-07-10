import { combineReducers } from "redux";
import userReducer from '../app/redux/user'
import orderReducer from '../app/redux/order'
const rootReducer = combineReducers({
    user:userReducer,
    order:orderReducer
});
export default rootReducer;
