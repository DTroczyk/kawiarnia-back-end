import React, { Fragment } from "react";
import { useSelector } from "react-redux";
import "./NewOrder.scss";
import CoffeeCup from "../CoffeeCup/CoffeeCup";
import Map from "../Map/Map";

import Payment from "../Payment/Payment";
import { Coffee, Wrapper, Section } from "./components";

function NewOrder() {
  const price = useSelector((state) => state.order.price);
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
      {price > 0 ? (
        <Section>
          <Payment />
        </Section>
      ) : null}
    </Fragment>
  );
}
export default NewOrder;
