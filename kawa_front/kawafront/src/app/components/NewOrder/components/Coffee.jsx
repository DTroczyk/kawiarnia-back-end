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
            espressoCount: 4,
            milkCount: 6,
            isContainChocolate: true,
          })
        );
        break;
      case "flatWhite":
        dispatch(
          orderActions.setPresetOfCoffee({
            coffeeName: name,

            espressoCount: 5,
            milkCount: 5,
            isContainChocolate: false,
          })
        );
        break;
      case "latte":
        dispatch(
          orderActions.setPresetOfCoffee({
            coffeeName: name,
            espressoCount: 2,
            milkCount: 6,
            isContainChocolate: false,
          })
        );
        break;
      case "americana":
        dispatch(
          orderActions.setPresetOfCoffee({
            coffeeName: name,
            espressoCount: 5,
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
            milkCount: 0,
            waterCount: 4,
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
