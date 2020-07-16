import React from "react";
import { toast } from "react-toastify";
import bucketActions from "../../../redux/bucket/actions";
import { useDispatch,useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
const Block = ({ children, type }) => {
  const history = useHistory();
  const dispatch = useDispatch();
  const order = useSelector((state) => state.order);
  function handleClick() {
    switch (type) {
      case "addToBucket":
        toast.info("Dodano do koszyka!", {
          position: "top-left",
          autoClose: 5000,
          hideProgressBar: true,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
        });
        setTimeout(() => {
          history.push("/panel");
          dispatch(bucketActions.addItemToBucket(order));
        }, 2000);
        return;
      default:
        return;
    }
  }
  return (
    <div onClick={handleClick} className="payment__block">
      {children}
    </div>
  );
};
export default Block;
