import React from "react";
import { useSelector,useDispatch } from "react-redux";
import orderActions from '../../../redux/order/actions'
function Input() {
  const orderProperties = useSelector((store) => store.order);
  const dispatch = useDispatch()
  return (
    <input
      type="checkbox"
      checked={orderProperties.isContainChocolate}
      onChange={() => dispatch(orderActions.toggleChocolate())}
    />
  );
}
export default Input;
