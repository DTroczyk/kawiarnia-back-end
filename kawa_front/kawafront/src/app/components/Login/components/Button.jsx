import React from "react";
import userActions from "../../../redux/user/actions";
import { useDispatch } from "react-redux";
import { useHistory } from "react-router-dom";
const Button = ({ name, children }) => {
  const dispatch = useDispatch();
  const history = useHistory();
  function handleClick(event) {
    switch (name) {
      case "login":
        dispatch(userActions.loginUser("123"));
        history.push("/panel");
        break;
      case "signIn":
        history.push("/signin");
        break;
      default:
        break;
    }
  }
  return (
    <button onClick={handleClick} className="login__button">
      {children}
    </button>
  );
};
export default Button;
