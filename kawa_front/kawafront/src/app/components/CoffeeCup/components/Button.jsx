import React from "react";
import { useDispatch } from "react-redux";
import orderActions from "../../../redux/order/actions";
import {elementScrollIntoView} from '../../../helpers'
function Button({ children, name }) {
  const dispatch = useDispatch();
  function handleClick() {
    switch (name) {
      case "addMilk":
        dispatch(orderActions.addMilk());
        break;
      case "addEspresso":
        dispatch(orderActions.addEspresso());
        break;
      case "addWater":
        dispatch(orderActions.addWater());
        break;
      case "deleteMilk":
        dispatch(orderActions.deleteMilk());
        break;
      case "deleteEspresso":
        dispatch(orderActions.deleteEspresso());
        break;
      case "deleteWater":
        dispatch(orderActions.deleteWater());
        break;
      case "goForeward":
        elementScrollIntoView("map");
        break;
      default:
        break;
    }
  }
  return (
    <button onClick={handleClick} name={name} className="coffeeCup__button">
      {children}
    </button>
  );
}
export default Button;
