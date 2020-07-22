import React from "react";
import { useSelector, useDispatch } from "react-redux";
import userAction from "../../../redux/user/actions";

const Input = ({ name, type = "text" }) => {
  const user = useSelector((state) => state.user.data);
  const dispatch = useDispatch();
  function getValue() {
    const {
      username,
      password,
      email,
      telephone,
      firstName,
      lastName,
      houseNumber,
      dateOfBirth,
      road,
      zipcode,
      place,
    } = user;
    switch (name) {
      case "username":
        return username;
      case "password":
        return password;
      case "email":
        return email;
      case "telephone":
        return telephone;
      case "firstName":
        return firstName;
      case "lastName":
        return lastName;
      case "houseNumber":
        return houseNumber;
      case "dateOfBirth":
        return dateOfBirth;
      case "road":
        return road;
      case "zipcode":
        return zipcode;
      case "place":
        return place;
      default:
        break;
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
      className="login__input"
      onChange={handleChange}
      value={getValue()}
      required
    />
  );
};
export default Input;
