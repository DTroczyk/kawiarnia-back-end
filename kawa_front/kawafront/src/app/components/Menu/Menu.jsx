import React from "react";
import "./Menu.scss";
import { useHistory } from "react-router-dom";
import { useDispatch } from "react-redux";
import userActions from "../../redux/user/actions";
function Menu() {
  const dispatch = useDispatch();
  const history = useHistory();
  function handleClick(event) {
    switch (event.target.attributes[0].value) {
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
    <div className="menu">
      <div onClick={handleClick} name="history" className="menu__block">
        Historia
      </div>
      <div onClick={handleClick} name="bucket" className="menu__block">
        Koszyk
      </div>
      <div onClick={handleClick} name="account" className="menu__block">
        Konto
      </div>
      <div onClick={handleClick} name="newOrder" className="menu__block">
        Nowe zamówienie
      </div>
      <div onClick={handleClick} name="logout" className="menu__block">
        Wyloguj
      </div>
    </div>
  );
}
export default Menu;
