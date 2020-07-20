import React from "react";
import { useDispatch } from "react-redux";
import userAction from "../../../redux/user/actions";
const Block = ({ id, children, setSelectedView }) => {
  const dispatch = useDispatch();
  function handleClick(e) {
    const id =
      e.target.className !== "delivery__block"
        ? e.target.parentNode.id
        : e.target.id;
    if (id === "choosedNewAdress") {
      dispatch(userAction.changeInputValue({ name: "road", value: "" }));
      dispatch(userAction.changeInputValue({ name: "zipcode", value: "" }));
      dispatch(userAction.changeInputValue({ name: "place", value: "" }));
      dispatch(userAction.changeInputValue({ name: "houseNumber", value: "" }));
    }
    setSelectedView(id);
  }
  return (
    <div id={id} onClick={handleClick} className="delivery__block">
      {children}
    </div>
  );
};
export default Block;
