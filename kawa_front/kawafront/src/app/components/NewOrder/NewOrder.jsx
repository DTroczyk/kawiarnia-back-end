import React, { Fragment, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import "./NewOrder.scss";
import CoffeeCup from "../CoffeeCup/CoffeeCup";
import Map from "../Map/Map";
import { PDFViewer } from "@react-pdf/renderer";
import PDF from "../PDFDocument/PDFDocument";
import orderActions from "../../redux/order/actions";
import Payment from "../Payment/Payment";
const moccaBg = {
  background:
    "url(https://img.wallpapersafari.com/desktop/1920/1080/45/90/dCS7mf.jpg) center",
};
const latteBg = {
  background:
    "url(https://images.unsplash.com/photo-1563090308-5a7889e40542?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=2134&q=80) center",
};
const americanoBg = {
  background:
    "url(https://images.unsplash.com/photo-1521302080334-4bebac2763a6?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=2000&q=80) center",
};
const flatwhiteBg = {
  background:
    "url(https://images.unsplash.com/photo-1459755486867-b55449bb39ff?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1949&q=80      ) center",
};

function NewOrder() {
  const dispatch = useDispatch();
  const user = useSelector((store) => store.user);

  function handleClick(event) {
    Array.from(document.getElementsByClassName("newOrder__preferences"))
      .shift()
      .scrollIntoView({
        block: "start",
        behavior: "smooth",
        inline: "nearest",
      });
    let nameOfElement = event.target.attributes[0].value;
    switch (nameOfElement) {
      case "mocca":
        dispatch(
          orderActions.setPresetOfCoffee({
            espressoCount: 4,
            milkCount: 6,
            isContainChocolate: true,
          })
        );
        break;
      case "flatWhite":
        dispatch(
          orderActions.setPresetOfCoffee({
            espressoCount: 5,
            milkCount: 5,
            isContainChocolate: false,
          })
        );
        break;
      case "latte":
        dispatch(
          orderActions.setPresetOfCoffee({
            espressoCount: 2,
            milkCount: 6,
            isContainChocolate: false,
          })
        );
        break;
      case "americana":
        dispatch(
          orderActions.setPresetOfCoffee({
            espressoCount: 5,
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
    <Fragment>
      <div className="newOrder">
        <div
          name="latte"
          onClick={handleClick}
          style={latteBg}
          className="newOrder__coffee"
        >
          <div className="newOrder__coffeeName">Latte</div>
        </div>
        <div
          name="mocca"
          onClick={handleClick}
          style={moccaBg}
          className="newOrder__coffee"
        >
          <div className="newOrder__coffeeName">Mocca</div>
        </div>
        <div
          name="americana"
          onClick={handleClick}
          style={americanoBg}
          className="newOrder__coffee"
        >
          <div className="newOrder__coffeeName">Americana</div>
        </div>
        <div
          name="flatWhite"
          onClick={handleClick}
          style={flatwhiteBg}
          className="newOrder__coffee"
        >
          <div className="newOrder__coffeeName">Flat white</div>
        </div>
      </div>
      <div className="newOrder__preferences">
        <CoffeeCup />
      </div>
      <div className="newOrder__map">
        <Map />
      </div>
      <div className="newOrder__payment"></div>
      <div className="newOrder__pdf">
        <PDFViewer width="100%" height="50%">
          <PDF fullName={user.data.fullName} />
        </PDFViewer>
      </div>
      <Payment />
    </Fragment>
  );
}
export default NewOrder;
