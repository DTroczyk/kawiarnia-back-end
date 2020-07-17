import React, { Fragment } from "react";

import "./NewOrder.scss";
import CoffeeCup from "../CoffeeCup/CoffeeCup";
import Map from "../Map/Map";

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
