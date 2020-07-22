import React from "react";
import { useSelector,useDispatch } from "react-redux";
import orderActions from '../../../redux/order/actions'
function Input() {
  const orderProperties = useSelector((store) => store.order);
  const dispatch = useDispatch()
  return (
    <label className="coffeeCup__checkboxContainer">
    <input
      type="checkbox"
      className="coffeeCup__input"
      checked={orderProperties.isContainChocolate}
      onChange={() => dispatch(orderActions.toggleChocolate())}
    />
    <span className="coffeeCup__checkmark"></span>
    </label>
  );  
}
export default Input;
