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
        <form className="login__formulee">
          <div className="login__field">
            <input className="login__input" type="text" required />
            <span className="login__input-highlight"></span>
            <span className="login__input-bar"></span>
            <label className="login__label">LOGIN</label>
          </div>
          <div className="login__field">
            <input className="login__input" type="password" required />
            <span className="login__input-highlight"></span>
            <span className="login__input-bar"></span>
            <label>HASŁO</label>
          </div>

          <div className="login__field">
            <button name="login" className="login__button" onClick={handleClick}>
              ZALOGUJ
            </button>
          </div>
          <div className="login__field">
            <button name="signin" className="login__button" onClick={handleClick}>
              DOŁĄCZ
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
