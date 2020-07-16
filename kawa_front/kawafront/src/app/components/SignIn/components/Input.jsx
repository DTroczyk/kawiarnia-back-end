import React from "react";
import { useSelector, useDispatch } from "react-redux";
import userAction from "../../../redux/user/actions";

const Input = ({ name, type = "text" }) => {
  const user = useSelector((state) => state.user.data);
  const dispatch = useDispatch();
  function getValue() {
    const { username, password, email } = user;
    switch (name) {
      case "username":
        return username;

      case "password":
        return password;

      case "email":
        return email;
        
      default:
        return;
    }
  }
  function handleChange(event) {
    const { name, value } = event.target;
    dispatch(userAction.changeInputValue({ name, value }));
  }
  return (
    <input
      name={name}
      type={type}
      className="signIn__input"
      onChange={handleChange}
      value={getValue()}
    />
  );
};
export default Input;
