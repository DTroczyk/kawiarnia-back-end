import React, { useState } from "react";
import "./SignIn.scss";
import { useSelector, useDispatch } from "react-redux";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import userAction from "../../redux/user/actions";
import { useHistory } from "react-router-dom";
function SignIn() {
  const user = useSelector((state) => state.user.data);
  const dispatch = useDispatch();
  const history = useHistory()
  const [selectedDate, setSelectedDate] = useState(new Date());
  function handleChange(event) {
    const { name, value } = event.target;
    dispatch(userAction.changeInputValue({ name, value }));
  }
  function handleClick(){
    dispatch(userAction.loginUser("123"))
    history.push('/panel')
  }
  return (
    <div className="signIn">
      <div className="signIn__formulee">
        <input
          name="username"
          type="text"
          className="signIn__input"
          onChange={handleChange}
          value={user.username}
        />
        <input
          name="password"
          type="text"
          className="signIn__input"
          onChange={handleChange}
          value={user.password}
        />
        <input
          name="email"
          type="text"
          className="signIn__input"
          onChange={handleChange}
          value={user.email}
        />
        <DatePicker
          selected={selectedDate}
          onChange={(date) => setSelectedDate(date)}
        />
      </div>
      <button onClick={handleClick}>+</button>
    </div>
  );
}
export default SignIn;
