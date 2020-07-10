import React from "react";
import "./CoffeeCup.scss";
import { useSelector, useDispatch } from "react-redux";
import orderActions from "../../redux/order/actions";
import FadeIn from "react-fade-in";
function CoffeeCup() {
  const orderProperties = useSelector((store) => store.order);
  const dispatch = useDispatch();
  function handleClick(e) {
    const { name } = e.target;
    switch (name) {
      case "addMilk":
        dispatch(orderActions.addMilk());
        break;
      case "addEspresso":
        dispatch(orderActions.addEspresso());
        break;
      case "deleteMilk":
        dispatch(orderActions.deleteMilk());
        break;
      case "deleteEspresso":
        dispatch(orderActions.deleteEspresso());
        break;
      case "goForeward":
        const map = document.querySelector(".newOrder__map");
        map.scrollIntoView({
          block: "start",
          behavior: "smooth",
          inline: "nearest",
        });
        break;
      default:
        break;
    }
  }
  return (
    <div className="wrapper">
      <div className="coffeeCup">
        {Array.apply(null, {
          length: orderProperties.milkCount,
        }).map((e, i) => (
          <FadeIn delay="100">
            <div key={i} className="coffeeCup__fill--milk"></div>
          </FadeIn>
        ))}
        {Array.apply(null, {
          length: orderProperties.espressoCount,
        }).map((e, i) => (
          <FadeIn delay="100">
            <div key={i} className="coffeeCup__fill--coffee"></div>
          </FadeIn>
        ))}
      </div>
      <div className="coffeeCup__ear"></div>
      <div className="coffeeCup__properties">
        <div className="coffeeCup__field">
          <p>Mleko</p>
          <button onClick={handleClick} name="addMilk">
            +
          </button>
          {orderProperties.milkCount ? (
            <button onClick={handleClick} name="deleteMilk">
              -
            </button>
          ) : null}
        </div>
        <div className="coffeeCup__field">
          <p>Espresso</p>
          <button onClick={handleClick} name="addEspresso">
            +
          </button>
          {orderProperties.espressoCount ? (
            <button onClick={handleClick} name="deleteEspresso">
              -
            </button>
          ) : null}
        </div>
        <div className="coffeeCup__field">
          <p>Czekolada</p>
          <input
            type="checkbox"
            checked={orderProperties.isContainChocolate}
            onChange={() => dispatch(orderActions.toggleChocolate())}
          />
        </div>
        <button name="goForeward" onClick={handleClick}>
          &#129047;
        </button>
      </div>
    </div>
  );
}
export default CoffeeCup;
