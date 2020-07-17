import React from "react";
import { useDispatch } from "react-redux";
import { getCoffeeBackground } from "../../../helpers";
import bucketActions from "../../../redux/bucket/actions";
const Field = ({ className,idx, children, coffeeName }) => {
  const dispatch = useDispatch();
  function handleClick() {
    dispatch(bucketActions.toggleElementToPay(idx));
  }
  return (
    <div
      onClick={handleClick}
      style={getCoffeeBackground(coffeeName)}
      className={className}
    >
      {children}
    </div>
  );
};
export default Field;
