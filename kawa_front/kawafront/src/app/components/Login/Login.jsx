import React from "react";
import "./Login.scss";
import { useDispatch } from "react-redux";
import userActions from "../../redux/user/actions";
import { useHistory } from "react-router-dom";
export default function Login() {
  const dispatch = useDispatch();
  const history = useHistory();
  function handleClick(event) {
    const { name } = event.target;
    switch (name) {
      case "login":
        dispatch(userActions.loginUser("123"));
        history.push("/panel");
        break;
      case "signin":
        history.push("/signin");
        break;
      default:
        break;
    }
  }
  return (
    <div className="login">
      <div className="login__container">
        <div className="login__formulee">
          <div className="login__field">
            <label className="login__label">LOGIN</label>
            <input className="login__input" type="text" />
          </div>
          <div className="login__field">
            <label className="login__label">HASŁO</label>
            <input className="login__input" type="password" />
          </div>

          <div className="login__field">
            <button name="login" className="login__button" onClick={handleClick}>
              ZALOGUJ
            </button>
            <button name="signin" className="login__button" onClick={handleClick}>
              DOŁĄCZ
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
