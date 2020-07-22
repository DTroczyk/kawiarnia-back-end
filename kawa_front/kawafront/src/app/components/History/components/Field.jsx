import React from "react";
import { useDispatch } from "react-redux";
import historyActions from "../../../redux/history/actions";
const Field = ({ children, idx }) => {
  const dispatch = useDispatch();
  function handleClick() {
    dispatch(historyActions.toggleOrderDetailsVisible(idx));
  }
  return (
    <div onClick={handleClick} className="history__field">
      {children}
    </div>
  );
};

export default Field;
