import { combineReducers } from "redux";
import userReducer from "../app/redux/user";
import orderReducer from "../app/redux/order";
import historyReducer from "../app/redux/history";
import bucketReducer from "../app/redux/bucket";
const rootReducer = combineReducers({
  user: userReducer,
  order: orderReducer,
  history: historyReducer,
  bucket: bucketReducer,
});
export default rootReducer;
