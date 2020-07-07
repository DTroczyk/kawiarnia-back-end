import React, { Fragment, useState } from "react";
import "./NewOrder.scss";
import CoffeeCup from "../CoffeeCup/CoffeeCup";
import Map from "../Map/Map";
import { PDFViewer } from "@react-pdf/renderer";
import PDF from "../PDFDocument/PDFDocument";
const moccaBg = {
  background:
    "url(https://img.wallpapersafari.com/desktop/1920/1080/45/90/dCS7mf.jpg)",
};

function NewOrder() {
  const [text, setText] = useState("");

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
        setText(nameOfElement);
        break;
      case "flatWhite":
        setText(nameOfElement);
        break;
      case "latte":
        setText(nameOfElement);
        break;
      case "americana":
        setText(nameOfElement);
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
          style={moccaBg}
          className="newOrder__coffee"
        >
          Latte
        </div>
        <div
          name="mocca"
          onClick={handleClick}
          style={moccaBg}
          className="newOrder__coffee"
        >
          Mocca
        </div>
        <div
          name="americana"
          onClick={handleClick}
          style={moccaBg}
          className="newOrder__coffee"
        >
          Americana
        </div>
        <div
          name="flatWhite"
          onClick={handleClick}
          style={moccaBg}
          className="newOrder__coffee"
        >
          Flat White
        </div>
      </div>
      <div className="newOrder__preferences">
        <CoffeeCup />
      </div>
      <div className="newOrder__preferences">
        <Map />
      </div>
      <div className="newOrder__preferences">
        <PDFViewer width="1000" height="600">
          <PDF />
        </PDFViewer>
      </div>
    </Fragment>
  );
}
export default NewOrder;
