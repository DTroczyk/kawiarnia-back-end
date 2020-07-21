import React, { useState } from "react";
import { toast } from "react-toastify";
import bucketActions from "../../../redux/bucket/actions";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { kURL } from "../../../helpers/consts";
import { loadStripe } from "@stripe/stripe-js";
import { getFetchHeader } from "../../../helpers";
import useToken from "../../../hooks/useToken";
const stripePromise = loadStripe("pk_test_JJ1eMdKN0Hp4UFJ6kWXWO4ix00jtXzq5XG");
const Block = ({ children, type, orderedProducts }) => {
  const history = useHistory();
  const dispatch = useDispatch();
  const token = useToken();
  const order = useSelector((state) => state.order);
  const [isClicked, setIsClicked] = useState(false);
  async function handleOrder(orderedProducts) {
    const stripeResponse = await fetch(
      `${kURL}/payment`,
      getFetchHeader("POST", token, orderedProducts)
    );
    const { id } = await stripeResponse.json();
    const stripe = await stripePromise;
    const { error } = await stripe.redirectToCheckout({ id });
  }

  function handleClick() {
    switch (type) {
      case "addToBucket":
        setIsClicked(true);
        if (!isClicked) {
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
        }
        return;
      case "payNow":
        setIsClicked(true);
        if (!isClicked) {
          toast.info("Przygotuj się do finalizacji zamówienia!", {
            position: "top-left",
            autoClose: 5000,
            hideProgressBar: true,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
          });
          setTimeout(() => {
            order.isSelectedToPay = true;
            dispatch(bucketActions.addItemToBucket(order));
            history.push("/panel/pay");
            window.scrollTo(0, 0);
          }, 2000);
        }
        return;
      case "przelewy24":
        handleOrder(orderedProducts);
        break;
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
