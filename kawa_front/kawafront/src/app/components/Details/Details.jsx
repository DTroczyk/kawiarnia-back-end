import React from "react";
import "./Details.scss";
import { useSelector } from "react-redux";
function Details({ idx }) {
  const item = useSelector((state) => state.history.historyItems[idx]);
  return (
    <div className="details">
      <div className="details__field">Metoda płatności:{item.paymentMethod}</div>
    </div>
  );
}
export default Details;
