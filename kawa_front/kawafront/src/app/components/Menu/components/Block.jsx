import React from "react";
import userActions from "../../../redux/user/actions";
import { useHistory } from "react-router-dom";
import { useDispatch } from "react-redux";
const Block = ({ children, name }) => {
  const history = useHistory();
  const dispatch = useDispatch();

  function handleClick() {
    switch (name) {
      case "history":
        history.push("/panel/history");
        break;
      case "bucket":
        history.push("/panel/bucket");
        break;
      case "account":
        history.push("/panel/account");
        break;
      case "newOrder":
        history.push("/panel/newOrder");
        break;
      case "logout":
        dispatch(userActions.logoutUser());
        history.push("/");
        break;
      default:
        break;
    }
  }
  return (
    <div onClick={handleClick} className="menu__block">
      {children}
    </div>
  );
};
export default Block;
