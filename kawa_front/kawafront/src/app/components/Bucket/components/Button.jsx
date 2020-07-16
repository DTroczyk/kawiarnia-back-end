import React from "react";
import { useDispatch } from "react-redux";
import bucketActions from '../../../redux/bucket/actions'
import { ToastContainer, toast } from "react-toastify";
const Button = ({idx, children, name }) => {
  const dispatch= useDispatch()
  
  function handleClick() {
    switch (name) {
      case "pay":
        break;
      case "delete":
        toast.warn("Usunięto z koszyka", {
          position: "top-left",
          autoClose: 5000,
          hideProgressBar: true,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
        });
        dispatch(bucketActions.deleteItemFromBucket(idx));
        break;
      default:
        break;
    }
  }
  return (
    <button onClick={handleClick} name={name} className="bucket__button">
      {children}
    </button>
  );
};
export default Button;
