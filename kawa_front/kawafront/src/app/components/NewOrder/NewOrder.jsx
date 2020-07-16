import React, { Fragment, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import "./NewOrder.scss";
import CoffeeCup from "../CoffeeCup/CoffeeCup";
import Map from "../Map/Map";
import { PDFViewer } from "@react-pdf/renderer";
import PDF from "../PDFDocument/PDFDocument";
import orderActions from "../../redux/order/actions";
import Payment from "../Payment/Payment";
import { Coffee, Wrapper, Section } from "./components";

function NewOrder() {
  

  return (
    <Fragment>
      <Wrapper>
        <Coffee name="latte" />
        <Coffee name="mocca" />
        <Coffee name="americana" />
        <Coffee name="flatWhite" />
        <Coffee name="espresso" />
        <Coffee name="flatWhite" />
        <Coffee name="flatWhite" />
        <Coffee name="flatWhite" />
      </Wrapper>
      <Section>
        <CoffeeCup />
      </Section>
      <Section>
        <Map />
      </Section>
      <Section>
        <Payment />
      </Section>
    </Fragment>
  );
}
export default NewOrder;
