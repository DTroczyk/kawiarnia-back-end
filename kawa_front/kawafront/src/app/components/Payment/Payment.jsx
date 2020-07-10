import React from "react";
import "./Payment.scss";
import { useDispatch } from "react-redux";
function Payment() {
  const dispatch = useDispatch();
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
    </div>
  );
}
export default Payment;
