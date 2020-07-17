import React from "react";
import { useDispatch } from "react-redux";
import orderActions from "../../../redux/order/actions";
import { getCoffeeBackground } from "../../../helpers";
const Coffee = ({ name }) => {
  const dispatch = useDispatch();
  function handleClick() {
    Array.from(document.getElementsByClassName("newOrder__section"))
      .shift()
      .scrollIntoView({
        block: "start",
        behavior: "smooth",
        inline: "nearest",
      });
    switch (name) {
      case "mocca":
        dispatch(
          orderActions.setPresetOfCoffee({
            coffeeName: name,
            espressoCount: 3,
            milkCount: 5,
            isContainChocolate: true,
            price: 9.5,
          })
        );
        break;
      case "flatWhite":
        dispatch(
          orderActions.setPresetOfCoffee({
            coffeeName: name,
            price: 7.5,
            espressoCount: 3,
            milkCount: 3,
            isContainChocolate: false,
          })
        );
        break;
      case "latte":
        dispatch(
          orderActions.setPresetOfCoffee({
            coffeeName: name,
            price: 7,
            espressoCount: 2,
            milkCount: 4,
            isContainChocolate: false,
          })
        );
        break;
      case "americana":
        dispatch(
          orderActions.setPresetOfCoffee({
            coffeeName: name,
            espressoCount: 5,
            price: 7.5,
            milkCount: 0,
            isContainChocolate: false,
          })
        );
        break;
      case "espresso":
        dispatch(
          orderActions.setPresetOfCoffee({
            coffeeName: name,
            espressoCount: 2,
            price: 3,
            milkCount: 0,
            isContainChocolate: false,
          })
        );
        break;
      default:
        break;
    }
  }
  return (
    <div
      style={getCoffeeBackground(name)}
      onClick={handleClick}
      className="newOrder__coffee"
    >
      <div className="newOrder__coffeeName">{name.toUpperCase()}</div>
    </div>
  );
};
export default Coffee;
