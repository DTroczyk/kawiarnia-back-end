import React from "react";
import "./Payment.scss";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { useHistory } from "react-router-dom";
import bucketActions from "../../redux/bucket/actions";
import { useDispatch, useSelector } from "react-redux";
function Payment() {
  const history = useHistory();
  const order = useSelector((state) => state.order);
  const dispatch=useDispatch()
  function handleAddToBucket() {
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
      dispatch(bucketActions.addItemToBucket(order))
    },2000);
  }

  return (
    <div className="payment">
      <div className="payment__block">
        <span className="payment__title">BLIK</span>
        <p className="payment__value">11,50zł</p>
      </div>
      <div className="payment__block">
        <span className="payment__title">Płatność przy odbiorze</span>
        <p className="payment__value">10,50zł</p>
      </div>
      <div className="payment__block" onClick={handleAddToBucket}>
        <span className="payment__title">Albo</span>
        <p className="payment__value">Dodaj do koszyka</p>
      </div>
      <ToastContainer
        position="top-left"
        autoClose={5000}
        hideProgressBar
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
      />
    </div>
  );
}
export default Payment;
